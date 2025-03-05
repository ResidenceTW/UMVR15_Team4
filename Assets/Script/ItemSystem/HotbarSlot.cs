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
    public Image ItemImage;
    public Image slotImage;
    public Sprite nullImage;
    public TextMeshProUGUI slotNumtText;

    private void Awake()
    {
        nullImage = GetComponent<Image>().sprite;
    }

    public void SetItem(ItemData newItem)
    {
        slotItem = newItem;
        ItemImage.sprite = newItem != null ? newItem.itemIcon : null;
        slotNumtText.text = (newItem != null && newItem.isStack) ? newItem.itemNum.ToString() : "";
    }

    public void UpdateSlot()
    {
        if (slotItem != null)
        {
            slotImage.sprite = slotItem.itemIcon;
            ItemImage.sprite = slotItem.itemIcon;
            ItemImage.enabled = true;
            slotNumtText.text = slotItem.isStack ? slotItem.itemNum.ToString() : "";
        }
        else
        {
            slotImage.sprite = nullImage;
            ItemImage.sprite = null;
            ItemImage.enabled = false;
            slotNumtText.text = "";
        }
    }
    public void ClearSlot()
    {
        slotItem = null;
        ItemImage.sprite = null;
        slotNumtText.text = "";
    }
}
