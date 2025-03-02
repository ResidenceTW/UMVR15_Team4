using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//�N��I�]�����@��slot �t�d���myBag itemlist���������D��
//����RefreshUI�ɡA�C��slot�|��sUI
public class HotbarSlot : MonoBehaviour
{
    public ItemData slotItem;
    public Image slotImage;
    public TextMeshProUGUI slotNumtText;

    public void SetItem(ItemData newItem)
    {
        slotItem = newItem;
        slotImage.sprite = newItem != null ? newItem.itemIcon : null;
        slotNumtText.text = (newItem != null && newItem.isStack) ? newItem.itemNum.ToString() : "";
    }

    public void UpdateSlot()
    {
        if (slotItem != null)
        {
            slotImage.sprite = slotItem.itemIcon;
            slotImage.enabled = true;
            slotNumtText.text = slotItem.isStack ? slotItem.itemNum.ToString() : "";
        }
        else
        {
            slotImage.sprite = null;
            slotImage.enabled = false;
            slotNumtText.text = "";
        }
    }
    public void ClearSlot()
    {
        slotItem = null;
        slotImage.sprite = null;
        slotNumtText.text = "";
    }
}
