using Assets.Scripts.Game.GridObjects;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitQueue : MonoBehaviour
{
    [SerializeField]
    private List<UnitQueueItem> unitQueueItems;

    public GridItem[] GetQueue()
    {
        return unitQueueItems.Select(x => x.PrefabInQue).ToArray();
    }
}
