using System.Collections.Generic;
using UnityEngine;

public class UnitSelection : MonoBehaviour
{
    [SerializeField]
    private List<UnitSelectionItem> unitSelectionItems;

    public void SetUnitSelectionItemsInteractable(bool interactable)
    {
        foreach (var item in unitSelectionItems)
        {
            item.Interactable = interactable;
        }
    }
}
