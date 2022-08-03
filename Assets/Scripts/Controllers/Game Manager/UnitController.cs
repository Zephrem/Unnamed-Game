using UnityEngine;

public class UnitController : MonoBehaviour
{
    private int column;
    private int row;

    private float speed;

    private GridController gridManager;
    private Tile myTile;

    private void Start()
    {
        gridManager = GetComponentInParent<GridController>();
        speed = gridManager.unitSpeed;

        //Handshake between this unit and the tile it is on for reference.
        gridManager.tileArray[column, row].SetUnit(this);
        SetTile(gridManager.tileArray[column, row]);
    }

    private void Update()
    {
        //Check if this unit is positioned where it should be.
        if (Mathf.Abs(row - transform.position.y) > .01)
        {
            //Move unit toward the tile position it should be at.
            transform.position = Vector2.Lerp(transform.position, gridManager.tileArray[column, row].transform.position, speed * Time.deltaTime);

            if (gridManager.unitArray[column, row] != this || gridManager.tileArray[column, row].GetUnit() != this || myTile != gridManager.tileArray[column, row])
            {
                gridManager.unitArray[column, row] = this;

                //Handshake between this unit and the tile it is on for reference.
                gridManager.tileArray[column, row].SetUnit(this);
                SetTile(gridManager.tileArray[column, row]);
            }
        }
        else
        {
            //Set the position manually when close enough to stop movement.
            transform.position = gridManager.tileArray[column, row].transform.position;
        }
    }

    #region __ACCESSORS__
    public Tile GetTile()
    {
        return (myTile);
    }

    public void SetTile(Tile newTile)
    {
        myTile = newTile;
    }

    public int GetColumn()
    {
        return (column);
    }

    public void SetColumn(int newColumn)
    {
        column = newColumn;
    }
    public int GetRow()
    {
        return (row);
    }

    public void SetRow(int newRow)
    {
        row = newRow;
    }
    #endregion
}
