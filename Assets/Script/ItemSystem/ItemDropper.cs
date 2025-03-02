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

        //�P�_�즲�ؼЬO�_���ֱ���B����O�_��Item
        if (transform.CompareTag("HotbarSlots") && dragger.gameObject.CompareTag("Item"))
        {
            int slotIndex = transform.GetSiblingIndex();
            if(transform.parent.name == "slot_groupR")
            {
                slotIndex += 3;
            }
            HotbarManager.instance.AssignItemToHotbar(dragger.GetItem(), slotIndex);
        }
        else
        {
            Slot targetSlot = GetComponent<Slot>();
            InventoryManager.instance.SwapItems(dragger.GetOriginSlot(), targetSlot);
        }
    }
}
