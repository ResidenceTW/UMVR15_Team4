using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class HotbarManager : MonoBehaviour
{
    public static HotbarManager instance;

    public List<HotbarSlot> hotbarSlots;
    public Inventory myBag;
    
    [SerializeField] private TextMeshProUGUI[] _itemTextArray;
    [SerializeField] private TextMeshProUGUI[] _skillTextArray;

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
    
    private void Start()
    {
        GameInput gameInput = GameInput.Instance;
        
        if(_itemTextArray != null)
        {
            _itemTextArray[0].text = gameInput.GetBindText(GameInput.Bind.UseItem1);
            _itemTextArray[1].text = gameInput.GetBindText(GameInput.Bind.UseItem2);
            _itemTextArray[2].text = gameInput.GetBindText(GameInput.Bind.UseItem3);
            _itemTextArray[3].text = gameInput.GetBindText(GameInput.Bind.UseItem4);
            _itemTextArray[4].text = gameInput.GetBindText(GameInput.Bind.UseItem5);
            _itemTextArray[5].text = gameInput.GetBindText(GameInput.Bind.UseItem6);
        }
        
        if(_skillTextArray != null)
        {
            _skillTextArray[0].text = gameInput.GetBindText(GameInput.Bind.Attack);
            _skillTextArray[1].text = gameInput.GetBindText(GameInput.Bind.Skill1);
            _skillTextArray[2].text = gameInput.GetBindText(GameInput.Bind.Skill2);
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
