using Assets.Scripts.Game.GridObjects;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitQueueItem : MonoBehaviour, IDropHandler
{
    [SerializeField]
    private Image unitImage;

    private GridItem prefabInQue;

    public GridItem PrefabInQue => prefabInQue;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            var item = eventData.pointerDrag.GetComponent<UnitSelection>();
            prefabInQue = item.PrefabToSpawn;
            unitImage.sprite = item.UnitSprite;
        }
    }
}
