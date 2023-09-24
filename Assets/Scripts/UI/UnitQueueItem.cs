using Game.GridObjects;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class UnitQueueItem : MonoBehaviour, IDropHandler, IPointerClickHandler
    {
        [SerializeField]
        private Image unitImage;

        private MovingGridItem prefabInQue;
        private Sprite originalSprite;

        public MovingGridItem PrefabInQue => prefabInQue;

        private void Awake()
        {
            originalSprite = unitImage.sprite;
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null)
            {
                var item = eventData.pointerDrag.GetComponent<UnitSelectionItem>();
                prefabInQue = item.PrefabToSpawn;
                unitImage.sprite = item.UnitSprite;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            unitImage.sprite = originalSprite;
            prefabInQue = null;
        }
    }
}
