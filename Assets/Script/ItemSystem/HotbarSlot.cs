using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//�N��I�]�����@��slot �t�d���myBag itemlist���������D��
//����RefreshUI�ɡA�C��slot�|��sUI
public class HotbarSlot : MonoBehaviour
{
    public Item slotItem;
    public Image slotImage;
    public TextMeshProUGUI slotNumtText;

    public void SetItem(Item newItem)
    {
        slotItem = newItem;
        slotImage.sprite = newItem != null ? newItem.itemIcon : null;
        slotNumtText.text = (newItem != null && newItem.isStack) ? newItem.itemNum.ToString() : "";
    }
}
