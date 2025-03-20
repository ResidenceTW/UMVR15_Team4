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
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        // �T�O HotbarSlots �O�q UI ���T�j�w��
        if (hotbarSlots.Count == 0)
        {
            Debug.LogWarning("HotbarManager: ���զ۰���� HotbarSlot...");
            hotbarSlots.AddRange(GetComponentsInChildren<HotbarSlot>());
        }

        foreach (var slot in hotbarSlots)
        {
            if (slot == null)
            {
                Debug.LogError("HotbarManager: �� HotbarSlot �� NULL�A���ˬd UI �]�m�I");
            }
        }
    }

    public void AssignItemToHotbar(ItemData item, int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= hotbarSlots.Count)
        {
            Debug.LogError($"HotbarManager: ���w�� HotbarSlot �W�X�d�� ({slotIndex})�I");
            return;
        }

        if (hotbarSlots[slotIndex] == null)
        {
            Debug.LogError($"HotbarManager: HotbarSlot at index {slotIndex} is missing!");
            return;
        }
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
            //�M��
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
