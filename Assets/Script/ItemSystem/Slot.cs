using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//�N��I�]�����@��slot �t�d���myBag itemlist���������D��
//����RefreshUI�ɡA�C��slot�|��sUI
public class Slot : MonoBehaviour
{   
    //slot�b myBag item list ����������
    public int slotIndex;
    private Item slotItem;
    public Image itemImage;
    public TextMeshProUGUI itemNumText;
    public TextMeshProUGUI itemName;

    public void SetSlotIndex(int index)
    {
        //�]�w��slot����myBag item List�����@��
        slotIndex = index;
    }

    //��sslot���D��W�١B�ƶq��
    public void UpdateSlot()
    {
        InventoryManager inventoryManager = InventoryManager.instance;

        //�T�O myBag �����o�ӯ���
        if (inventoryManager.myBag.itemList.Count > slotIndex)
        {
            //��� mybag item list ���ƭ�
            slotItem = inventoryManager.myBag.itemList[slotIndex];

            itemImage.sprite = slotItem.itemIcon;
            itemImage.enabled = true;
            itemName.text = slotItem.itemName;

            //�P�_�D��O�_���i���|�A�]�w���i���|����~��ܼƶq
            if (slotItem.isStack && slotItem.itemNum >= 0)
            {
                itemNumText.text = slotItem.itemNum.ToString();
                itemNumText.enabled = true;
            }
            else
            {
                itemNumText.enabled = false;
            }
        }
        else
        {
            //�M��Slot��UI
            itemImage.sprite = null;
            itemImage.enabled = false;
            itemNumText.text = "";
            itemNumText.enabled = false;
            itemName.text = "-";
        }
    }
}
