using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

//�t�d�N�D��[�iitemlist
//�å�RefreshUI�t�d�Nitemlist�����D����ܦbslot�W
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public Inventory myBag;
    public Slot[] slots;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;
    }

    private void Start()
    {
        //��l�Ʈɨ�sUI
        RefreshUI(); 
    }

    //�D��Q�ߨ���A�qItemGet���I�s����k
    public void AddItem(Item newItem)
    {
        //�ˬdmyBag itemlist�ݬݳo�ӹD��O�_�w�s�b
        for (int i = 0; i < myBag.itemList.Count; i++)
        {
            //�P�_Slot�����D��ID�O�_�P�ߨ��D�㪺ID�@�P
            //�P�_�D��O�_�i�H���|
            if (myBag.itemList[i].itemID == newItem.itemID && newItem.isStack)
            {
                myBag.itemList[i].itemNum += 1;
                RefreshUI();
                return;
            }
        }

        //�p�GmyBag itemList�̨S���o�ӹD��Ϊ̤��i���|
        //�B�I�]�S�����N�s�W�D��
        if (myBag.itemList.Count < slots.Length)
        {
            myBag.itemList.Add(newItem);
            newItem.itemNum = 1;
            RefreshUI();
        }
        else
        {
            Debug.Log("�I�]�w��");
        }
    }

    public void RefreshUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            //���C��slot�I�s��kSetSlotIndex������ި�Ū����Ƨ�sUI
            slots[i].SetSlotIndex(i);
            slots[i].UpdateSlot();
        }
    }

    public void SwapItems(Slot slotA, Slot slotB)
    {
        if (slotA == null || slotB == null) return;

        int indexA = slotA.slotIndex;
        int indexB = slotB.slotIndex;

        if (indexA >= myBag.itemList.Count || indexB >= myBag.itemList.Count) return;
       
        // �洫 mybag itemlist ���� itemData
        Item temp = myBag.itemList[indexA];
        myBag.itemList[indexA] = myBag.itemList[indexB];
        myBag.itemList[indexB] = temp;

        RefreshUI();
    }
}

