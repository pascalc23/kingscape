using Assets.Scripts.Game.GridObjects;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitSelection : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private Transform container;
    [Space]
    [SerializeField]
    private Image unitImage;
    [SerializeField]
    private GridItem prefabToSpawn;

    private GameObject duplicate;

    public GridItem PrefabToSpawn => prefabToSpawn;
    public Sprite UnitSprite => unitImage.sprite;

    public void OnBeginDrag(PointerEventData eventData)
    {
        duplicate = Instantiate(gameObject, container);
        (duplicate.transform as RectTransform).sizeDelta = (transform as RectTransform).sizeDelta;

        foreach (var item in duplicate.GetComponentsInChildren<Graphic>())
        {
            item.raycastTarget = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        duplicate.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(duplicate);
    }


}
