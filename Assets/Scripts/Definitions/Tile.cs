using UnityEngine;

public class Tile : MonoBehaviour
{
    public delegate void OnClickCallback(Tile tileController);
    public OnClickCallback onClickCallback;

    public int column;
    public int row;

    [SerializeField] private Color baseColor;
    [SerializeField] private Color hightlightColor;

    private UnitController myUnit;

    public void OnMouseEnter()
    {
        GetComponent<SpriteRenderer>().color = hightlightColor;
    }

    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = baseColor;
    }

    private void OnMouseDown()
    {
        if (onClickCallback != null)
        {
            onClickCallback.Invoke(this);
        }
    }


    #region __ACCESSORS__
    public UnitController GetUnit()
    {
        return (myUnit);
    }

    public void SetUnit(UnitController newUnit)
    {
        myUnit = newUnit;
    }
    #endregion
}
