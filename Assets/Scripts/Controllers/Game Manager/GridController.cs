using System.Collections;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public delegate void OnGridChangeCallback();
    public OnGridChangeCallback onGridChangeCallback;

    [SerializeField] private int columns, rows;

    [SerializeField] private Tile tilePrefab;
    [SerializeField] private SpawnTable spawner;

    [SerializeField] private float padWidth;
    [SerializeField] private float padHeight;

    public Tile[,] tileArray;
    public UnitController[,] unitArray;

    public float unitSpeed;

    private void Awake()
    {
        tileArray = new Tile[columns, rows];
        unitArray = new UnitController[columns, rows];

        GenerateGrid();

        //Populate with Coroutine in order to preserve unit entry effect during load time.
        StartCoroutine(PopulateGridCo());
    }

    private void GenerateGrid()
    {
        float xPadding = 0;
        float yPadding = 0;

        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                var spawnedTile = Instantiate(tilePrefab, new Vector2(this.transform.position.x + i + xPadding,
                    this.transform.position.y + j + yPadding), Quaternion.identity);

                //Organize tiles as children of the GridManager in the hierarchy and name them based on coordinates.
                spawnedTile.transform.parent = this.transform;
                spawnedTile.name = $"Tile ({i}, {j})";

                spawnedTile.column = i;
                spawnedTile.row = j;

                tileArray[i, j] = spawnedTile;

                yPadding += padHeight;
            }

            xPadding += padWidth;
            yPadding = 0;
        }
    }

    private IEnumerator PopulateGridCo()
    {
        yield return new WaitForSeconds(.5f);

        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                SpawnUnit(new Vector2(tileArray[i, j].transform.position.x, rows + 1), i, j);
            }
        }

        GridChangeCallback();
    }

    public void GridCleanup()
    {
        bool wasKilled = false;

        foreach(UnitController unit in unitArray)
        {
            EnemyStats unitStats = unit.GetComponent<EnemyStats>();

            if (unitStats.GetHealth() <= 0)
            {
                unitStats.LootDrop();

                unitStats.Kill();

                wasKilled = true;
            }
        }

        if (wasKilled)
        {
            CollapseColumns();
        }
    }

    private void CollapseColumns()
    {
        StartCoroutine(CollapseColumnsCo());
    }

    private IEnumerator CollapseColumnsCo()
    {
        int nullCount = 0;

        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                if (unitArray[i, j] == null)
                {
                    //Sum of null rows.
                    nullCount++;
                }
                else if (nullCount > 0)
                {
                    var currentRow = unitArray[i, j].GetRow();
                    unitArray[i, j].SetRow(currentRow - nullCount);
                    unitArray[i, j] = null;
                }
            }
            nullCount = 0;
        }

        yield return new WaitForSeconds(.1f);

        RefillColumns();

        GridChangeCallback();
    }

    private void SpawnUnit(Vector2 destination, int x, int y)
    {
        unitArray[x, y] = Instantiate(spawner.RollTable(), destination, Quaternion.identity).GetComponent<UnitController>();

        unitArray[x, y].SetColumn(x);
        unitArray[x, y].SetRow(y);

        unitArray[x, y].transform.parent = this.transform;
    }

    //Check the top row of each column and add a unit if it is null.
    private void RefillColumns()
    {
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                if (unitArray[i, j] == null)
                {
                    SpawnUnit(new Vector2(tileArray[i, j].transform.position.x, rows + 1), i, j);
                }
            }
        }
    }

    private void GridChangeCallback()
    {
        if(onGridChangeCallback != null)
        {
            onGridChangeCallback.Invoke();
        }
    }

    #region __ACCESSORS__
    public int GetColumns()
    {
        return (columns);
    }

    public int GetRows()
    {
        return (rows);
    }
    #endregion

}
