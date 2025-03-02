using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDropper : MonoBehaviour, IDropHandler
{
    private Slot targetSlot;

    void Awake()
    {
        //���o��Dropper��slot
        targetSlot = GetComponent<Slot>(); 
    }

    public void OnDrop(PointerEventData eventData)
    {
        //�T�O�즲���~�s�b
        ItemDragger dragger = eventData.pointerDrag.GetComponent<ItemDragger>();
        if (dragger == null) return;

        if (transform.CompareTag("HotbarSlots"))
        {
            int slotIndex = transform.GetSiblingIndex();
            HotbarManager.instance.AssignItemToHotbar(dragger.GetItem(), slotIndex);
        }
        else
        {
            Slot targetSlot = GetComponent<Slot>();
            InventoryManager.instance.SwapItems(dragger.GetOriginSlot(), targetSlot);
        }

        ////���o�즲���~��slot�ýT�Oslot�s�b
        //Slot originSlot = dragger.GetOriginSlot(); 
        //if (originSlot == null || targetSlot == null) return;

        ////�洫 mybag itemlist �����ƾ�
        //InventoryManager inventoryManager = InventoryManager.instance;
        //if (inventoryManager == null) return;

        //int originIndex = originSlot.slotIndex;
        //int targetIndex = targetSlot.slotIndex;

        //if (inventoryManager.myBag.itemList.Count > originIndex && inventoryManager.myBag.itemList.Count > targetIndex)
        //{
        //    // �洫 mybag itemlist ���� itemData
        //    Item tempItem = inventoryManager.myBag.itemList[originIndex];
        //    inventoryManager.myBag.itemList[originIndex] = inventoryManager.myBag.itemList[targetIndex];
        //    inventoryManager.myBag.itemList[targetIndex] = tempItem;

        //    //��sUI
        //    inventoryManager.RefreshUI();
        //}
    }
}
