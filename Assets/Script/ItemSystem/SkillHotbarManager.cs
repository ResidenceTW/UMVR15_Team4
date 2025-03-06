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

    private void Start()
    {
        //�T�O�C��hotbar slot�����T��index
        for (int i = 0; i < hotbarSlots.Count; i++)
        {
            hotbarSlots[i].SetSlotIndex(i);
        }
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
                
                //�M���쥻�ֱ��椺���D��
                previousSlot.skillData = null;
                previousSlot.UpdateHotbarSlot();
                
                switch(i)
                {
                    case 0:
                        SkillManager.Instance.RemoveSkillBind(GameInput.Bind.Skill1);
                        break;
                    case 1:
                        SkillManager.Instance.RemoveSkillBind(GameInput.Bind.Skill2);
                        break;
                }
                
                break;
            }
        }

        /*
        if (previousSlot != null)
        {
            //�M���쥻�ֱ��椺���D��
            previousSlot.skillData = null;
            previousSlot.UpdateHotbarSlot();
        }
        */

        hotbarSlots[slotIndex].skillData = skill;
        hotbarSlots[slotIndex].UpdateHotbarSlot();
    }
    public void RefreshHotbarUI()
    {
        for (int i = 0; i < hotbarSlots.Count; i++)
        {
            hotbarSlots[i].UpdateHotbarSlot(); 
        }
    }
}
