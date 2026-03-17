using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string itemName;
    public int tileid;
    public string description;

    public void OnPointerEnter(PointerEventData _)
    {
        int price = (MoneySystem.price != null && tileid >= 0 && tileid < MoneySystem.price.Length)
            ? MoneySystem.price[tileid]
            : 0;

        TooltipSystem.Show(itemName, price, description);
    }

    public void OnPointerExit(PointerEventData _)
    {
        TooltipSystem.Hide();
    }
}