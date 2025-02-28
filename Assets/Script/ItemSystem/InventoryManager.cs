using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<Slot> slots = new List<Slot>();
    public Inventory myBag;

    private void Start()
    {
        //��l�Ʈɨ�sUI
        RefreshUI(); 
    }

    //�D��Q�ߨ���A�qItemTake���I�s����k
    public void AddItem(Item newItem)
    {
        //�ˬd�I�]���O�_�w�g���o�ӹD��
        foreach (Slot slot in slots)
        {
            //�P�_Slot���O�Ū��A�ӬO���D�㪺
            //�P�_Slot�����D��ID�O�_�P�ߨ��D�㪺ID�@�P
            //�P�_�D��O�_�i�H���|
            if (slot.slotItem != null && slot.slotItem.itemID == newItem.itemID && newItem.isStack)
            {
                slot.slotItem.itemNum += 1;
                slot.UpdateSlot();
                return;
            }
        }

        //��Ĥ@�ӪŪ�Slot�A�N�D���i�h
        foreach (Slot slot in slots)
        {
            //�T�OSlot�O�Ū�
            if (slot.slotItem == null) 
            {
                slot.SetItem(newItem);
                myBag.itemList.Add(newItem);
                slot.slotItem.itemNum += 1;
                slot.UpdateSlot();
                return;
            }
        }
        Debug.Log("�I�]���F");
    }

    public void RefreshUI()
    {
        foreach (Slot slot in slots)
        {
            // ���Ҧ�Slot��sUI
            slot.UpdateSlot(); 
        }
    }
}

