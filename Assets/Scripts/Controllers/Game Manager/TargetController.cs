using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public delegate void OnTargetCallback();
    public OnTargetCallback onTargetCallback;

    public Tile primaryTarget;

    private List<Tile> targetList;

    private GridController gridManager;

    private Coroutine lastRoutine = null;


    // Start is called before the first frame update
    void Start()
    {
        targetList = new List<Tile>();
        gridManager = GetComponent<GridController>();

        //When a tile registers a click, it will request to be the primary target.
        foreach (Tile t in gridManager.tileArray)
        {
            t.onClickCallback += SetTarget;
        }
    }

    //Clear current targets and make a new primary target. Add to target list for multi-targeting.
    private void SetTarget(Tile newTarget)
    {
        ClearTargets();

        primaryTarget = newTarget;

        targetList.Add(primaryTarget);

        if (onTargetCallback != null)
        {
            onTargetCallback.Invoke();
        }
    }

    //Add all valid targets to target list based on targeting style and return the list.
    public List<Tile> GetTargetList(Ability.TargetingType targetingType, int targetNumber)
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

        return (targetList);
    }

    public void ClearTargets()
    {
        primaryTarget = null;
        targetList.Clear();
    }

    #region __TARGETING ROUTINE__
    //Begin coroutine to wait for target selection.
    public void StartTargeting()
    {
        lastRoutine = StartCoroutine(WaitForTargetCo());
    }

    //End coroutine and set lastRoutine to null.
    public void CancelTargeting()
    {
        if (lastRoutine != null)
        {
            StopCoroutine(lastRoutine);
            lastRoutine = null;
        }
    }

    //Clear existing targets and invoke target callback when a new target is set.
    private IEnumerator WaitForTargetCo()
    {
        ClearTargets();

        yield return new WaitUntil(() => primaryTarget != null);

        if (onTargetCallback != null)
        {
            onTargetCallback.Invoke();
        }

        lastRoutine = null;
    }
    #endregion

    #region __TARGETING PROTOCOLS__
    private void HorizontalTarget(int targetNumber)
    {
        if (targetList.Count > 0)
        {
            for (int i = 0; i < targetNumber; i++)
            {
                //Variables to find targets on the sides of the primary target.
                var over = Mathf.Clamp(targetList[0].column + i, 0, gridManager.GetColumns() - 1);
                var under = Mathf.Clamp(targetList[0].column - i, 0, gridManager.GetColumns() - 1);

                if (!targetList.Contains(gridManager.tileArray[over, targetList[0].row]))
                {
                    targetList.Add(gridManager.tileArray[over, targetList[0].row]);
                }
                if (!targetList.Contains(gridManager.tileArray[under, targetList[0].row]))
                {
                    targetList.Add(gridManager.tileArray[under, targetList[0].row]);
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
                var under = Mathf.Clamp(targetList[0].row - i, 0, gridManager.GetRows() - 1);

                if (!targetList.Contains(gridManager.tileArray[targetList[0].column, over]))
                {
                    targetList.Add(gridManager.tileArray[targetList[0].column, over]);
                }
                if (!targetList.Contains(gridManager.tileArray[targetList[0].column, under]))
                {
                    targetList.Add(gridManager.tileArray[targetList[0].column, under]);
                }
            }
        }
    }
    #endregion
}
