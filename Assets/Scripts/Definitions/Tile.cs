using UnityEngine;

public class Tile : MonoBehaviour
{
    public delegate void OnClickCallback(Tile tileController);
    public OnClickCallback onClickCallback;

    public delegate void OnHoverStartCallback(Tile tileController);
    public OnHoverStartCallback onHoverStartCallback;

    public delegate void OnHoverStopCallback();
    public OnHoverStopCallback onHoverStopCallback;

    public int column;
    public int row;

    [SerializeField] private Color baseColor;
    [SerializeField] private Color highlightColor;

    private EnemyController myUnit;

    public void OnMouseEnter()
    {
        if(onHoverStartCallback != null)
        {
            onHoverStartCallback.Invoke(this);
        }
    }

    private void OnMouseExit()
    {
        if(onHoverStopCallback != null)
        {
            onHoverStopCallback.Invoke();
        }
    }

    private void OnMouseDown()
    {
        if (onClickCallback != null)
        {
            onClickCallback.Invoke(this);
        }
    }

    public void Highlight(bool toggle)
    {
        if(toggle == true)
        {
            gameObject.GetComponent<SpriteRenderer>().color = highlightColor;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = baseColor;
        }
    }


    #region __ACCESSORS__
    public EnemyController GetUnit()
    {
        if(myUnit != null && myUnit.GetTile() != this)
        {
            myUnit = null;
        }

        return (myUnit);
    }

    public void SetUnit(EnemyController newUnit)
    {
        myUnit = newUnit;
    }
    #endregion
}
