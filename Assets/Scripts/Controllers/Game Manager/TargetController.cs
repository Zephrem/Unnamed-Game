using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public delegate void OnTargetChosenCallback(List<Tile> list);
    public OnTargetChosenCallback onTargetChosenCallback;

    private Ability.TargetingType targetingType;
    private int targetingRadius;
    private int targetingRange;

    private bool isTargeting = false;

    private List<Tile> targetList;

    private GridController gridManager;

    void Start()
    {
        targetList = new List<Tile>();
        gridManager = GetComponent<GridController>();
    }

    public void StartTargeting(Ability.TargetingType type, int radius, int range)
    {
        isTargeting = true;
        targetingType = type;
        targetingRadius = radius;
        targetingRange = range;

        foreach (Tile t in gridManager.tileArray)
        {
            t.onClickCallback += ConfirmTarget;
            t.onHoverStartCallback += PreviewTarget;
            t.onHoverStopCallback += ClearTargetList;
        }
    }

    public void StopTargeting()
    {
        isTargeting = false;
        ClearTargetList();

        foreach (Tile t in gridManager.tileArray)
        {
            t.onClickCallback -= ConfirmTarget;
            t.onHoverStartCallback -= PreviewTarget;
            t.onHoverStopCallback -= ClearTargetList;
        }
    }

    private void ConfirmTarget(Tile newTarget)
    {
        if (onTargetChosenCallback != null && newTarget.row + 1 <= targetingRange)
        {
            onTargetChosenCallback.Invoke(targetList);
        }
    }

    private void PreviewTarget(Tile tile)
    {
        if (isTargeting && tile.row + 1 <= targetingRange)
        {
            ClearTargetList();

            targetList.Add(tile);

            GenerateTargetList(targetingType, targetingRadius);

            foreach(Tile t in targetList)
            {
                t.Highlight(true);
            }
        }
    }

    public void GenerateTargetList(Ability.TargetingType targetingType, int targetNumber)
    {
        switch (targetingType)
        {
            case Ability.TargetingType.horizontal:
                HorizontalTarget(targetNumber);
                break;

            case Ability.TargetingType.vertical:
                VerticalTarget(targetNumber);
                break;

            default:
                break;
        }
    }

    public void ClearTargetList()
    {
        foreach(Tile t in targetList)
        {
            t.Highlight(false);
        }

        targetList.Clear();
    }

    #region __TARGETING PROTOCOLS__
    private void HorizontalTarget(int targetNumber)
    {
        if (targetList.Count > 0)
        {
            for (int i = 0; i < targetNumber; i++)
            {
                var over = Mathf.Clamp(targetList[0].column + i, 0, gridManager.GetColumns() - 1);

                if (!targetList.Contains(gridManager.tileArray[over, targetList[0].row]))
                {
                    targetList.Add(gridManager.tileArray[over, targetList[0].row]);
                }
            }
        }
    }

    private void VerticalTarget(int targetNumber)
    {
        if (targetList.Count > 0)
        {
            for (int i = 0; i < targetNumber; i++)
            {
                var over = Mathf.Clamp(targetList[0].row + i, 0, gridManager.GetRows() - 1);

                if (!targetList.Contains(gridManager.tileArray[targetList[0].column, over]))
                {
                    targetList.Add(gridManager.tileArray[targetList[0].column, over]);
                }
            }
        }
    }
    #endregion
}
