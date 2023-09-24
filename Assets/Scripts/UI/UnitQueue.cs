using System.Collections.Generic;
using System.Linq;
using Game.GridObjects;
using UnityEngine;

namespace UI
{
    public class UnitQueue : MonoBehaviour
    {
        [SerializeField]
        private List<UnitQueueItem> unitQueueItems;

        public GridItem[] GetQueue()
        {
            return unitQueueItems.Select(x => x.PrefabInQue).ToArray();
        }
    }
}
