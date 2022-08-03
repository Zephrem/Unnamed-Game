using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Table/Spawn Table", fileName = "New Spawn Table")]
public class SpawnTable : ScriptableObject
{
    [SerializeField] private List<GameObject> spawnTable = new List<GameObject>();

    public GameObject RollTable()
    {
        int index = Random.Range(0, spawnTable.Count);

        return (spawnTable[index]);
    }
}
