using System;
using System.Collections.Generic;
using System.Linq;
using Game.GridObjects;
using Game.Levels;
using UnityEngine;

namespace UI
{
    public class UnitQueue : MonoBehaviour
    {
        [SerializeField]
        private Transform container;
        [SerializeField]
        private UnitQueueItem unitQueueItemPrefab;

        private List<UnitQueueItem> unitQueueItems;

        public MovingGridItem[] GetQueue()
        {
            return unitQueueItems.Select(x => x.PrefabInQue).ToArray();
        }

        public void OnLevelLoaded(Level level)
        {
            foreach (Transform item in container)
            {
                Destroy(item.gameObject);
            }

            unitQueueItems.Clear();

            for (int i = 0; i < level.pawnSlots; i++)
            {
                var go = Instantiate(unitQueueItemPrefab, container);
                unitQueueItems.Add(go);
            }
        }
    }
}
