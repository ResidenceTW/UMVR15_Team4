using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class SkillHotbarManager : MonoBehaviour
{
    public static SkillHotbarManager instance;

    public List<SkillSlot> hotbarSlots;
    public SkillListManager skillListManager;

    private void Awake()
    {
        if (instance != null) Destroy(this);
        instance = this;
    }

    public void AssignSkillToHotbar(SkillDataSO skill, int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= hotbarSlots.Count) return;

        //�ˬd�ֱ���O�_�w�g���o�ӧޯ�
        SkillSlot previousSlot = null;
        for (int i = 0; i < hotbarSlots.Count; i++)
        {
            if (hotbarSlots[i].skillData == skill) 
            {
                //�O���o��HotbarSlot
                previousSlot = hotbarSlots[i]; 
                break;
            }
        }

        if (previousSlot != null)
        {
            //�M���쥻�ֱ��椺���D��
            previousSlot.skillData=null;
            previousSlot.UpdateSlot();
        }

        hotbarSlots[slotIndex].skillData = skill;
        hotbarSlots[slotIndex].UpdateSlot();
    }
    public void RefreshHotbarUI()
    {
        foreach (SkillSlot slot in hotbarSlots)
        {
            slot.UpdateSlot();
        }
    }
}
