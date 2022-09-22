using System.Collections;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public delegate void OnGridChangeCallback();
    public OnGridChangeCallback onGridChangeCallback;

    [SerializeField] private BattleController battleController;

    [SerializeField] private int columns, rows;

    [SerializeField] private Tile tilePrefab;
    [SerializeField] private SpawnTable spawner;

    [SerializeField] private float padWidth;
    [SerializeField] private float padHeight;

    public Tile[,] tileArray;
    public EnemyController[,] unitArray;

    public float unitSpeed;

    private void Awake()
    {
        tileArray = new Tile[columns, rows];
        unitArray = new EnemyController[columns, rows];

        GenerateGrid();

        //Populate with Coroutine in order to preserve unit entry effect during load time.
        StartCoroutine(PopulateGridCo());

        onGridChangeCallback += CheckMatches;
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

        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                if (unitArray[i, j] != null)
                {
                    EnemyStats unitStats = unitArray[i, j].GetComponent<EnemyStats>();

                    if (unitStats.GetHealth() <= 0)
                    {
                        unitStats.LootDrop();
                        battleController.AddProgress(unitStats.GetCredit());

                        unitStats.Kill();

                        wasKilled = true;
                    }
                }
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

        if (battleController.GetStageProgress() < battleController.GetMaxStageProgress())
        {
            RefillColumns();
        }
        else
        {
            var totalEnemies = 0;

            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    if (unitArray[i, j] != null)
                    {
                        totalEnemies++;
                    }
                }
            }

            if(totalEnemies <= 0)
            {
                battleController.Victory();
            }
        }

        GridChangeCallback();
    }

    private void CheckMatches()
    {
        for (int i = 1; i < columns - 1; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                if (unitArray[i, j] != null && unitArray[i - 1, j] != null && unitArray[i + 1, j] != null)
                {
                    var middleUnit = unitArray[i, j].GetComponent<EnemyStats>().GetUnitName();
                    var leftUnit = unitArray[i - 1, j].GetComponent<EnemyStats>().GetUnitName();
                    var rightUnit = unitArray[i + 1, j].GetComponent<EnemyStats>().GetUnitName();

                    if (middleUnit == leftUnit && middleUnit == rightUnit)
                    {
                        PromoteUnit(i, j);
                    }
                }
            }
        }

        for (int i = 0; i < columns; i++)
        {
            for (int j = 1; j < rows - 1; j++)
            {
                if (unitArray[i, j] != null && unitArray[i, j - 1] != null && unitArray[i, j + 1] != null)
                {
                    var middleUnit = unitArray[i, j].GetComponent<EnemyStats>().GetUnitName();
                    var leftUnit = unitArray[i, j - 1].GetComponent<EnemyStats>().GetUnitName();
                    var rightUnit = unitArray[i, j + 1].GetComponent<EnemyStats>().GetUnitName();

                    if (middleUnit == leftUnit && middleUnit == rightUnit)
                    {
                        PromoteUnit(i, j);
                    }
                }
            }
        }
    }

    private void PromoteUnit(int x, int y)
    {
        Transform promoUnit = unitArray[x, y].GetComponent<EnemyStats>().GetPromo();
        Vector2 destination = unitArray[x, y].transform.position;

        if (promoUnit != null)
        {
            Destroy(unitArray[x, y].gameObject);

            unitArray[x, y] = Instantiate(promoUnit, destination, Quaternion.identity).GetComponent<EnemyController>();

            unitArray[x, y].SetColumn(x);
            unitArray[x, y].SetRow(y);

            unitArray[x, y].transform.parent = this.transform;
        }
    }

    private void SpawnUnit(Vector2 destination, int x, int y)
    {
        unitArray[x, y] = Instantiate(spawner.RollTable(), destination, Quaternion.identity).GetComponent<EnemyController>();

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
