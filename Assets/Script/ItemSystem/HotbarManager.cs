using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class HotbarManager : MonoBehaviour
{
    public static HotbarManager instance;

    public List<HotbarSlot> hotbarSlots;
    public Inventory myBag;

    private void Awake()
    {
        if (instance != null) Destroy(this);
        instance = this;
    }
    public void AssignItemToHotbar(ItemData item, int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= hotbarSlots.Count) return;

        //�j�M��ܧֱ���̪��D��O�_�w������
        HotbarSlot previousSlot = null;
        for (int i = 0; i < hotbarSlots.Count; i++)
        {
            if (hotbarSlots[i].slotItem == item) 
            {
                //�O���o��HotbarSlot
                previousSlot = hotbarSlots[i]; 
                break;
            }
        }

        if (previousSlot != null)
        {
            //�M���쥻�ֱ��椺���D��
            previousSlot.ClearSlot();
        }

        hotbarSlots[slotIndex].SetItem(item);

        RefreshHotbarUI();
    }
    public void RefreshHotbarUI()
    {
        for (int i = 0; i < hotbarSlots.Count; i++)
        {
            hotbarSlots[i].UpdateSlot();
        }
    }
}
