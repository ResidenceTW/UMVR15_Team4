using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

//�t�d�N�D��[�iitemlist
//�å�RefreshUI�t�d�Nitemlist�����D����ܦbslot�W
public class SkillListManager : MonoBehaviour
{
    public static SkillListManager instance;
    public SkillBag mySkill;
    public SkillSlot[] skillSlots;

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

    public void RefreshUI()
    {
        for (int i = 0; i < skillSlots.Length; i++)
        {
            skillSlots[i].SetSlotIndex(i);
            if (i < mySkill.skillList.Count)
            {
                skillSlots[i].skillData = mySkill.skillList[i];
            }
            else
            {
                skillSlots[i].skillData = null;
            }
            skillSlots[i].UpdateSkillListSlot();
        }
    }

    public void UnlockSkill(int index)
    {
        if (index >= 0 && index < mySkill.skillList.Count)
        {
            mySkill.skillList[index].isUnlocked = true;
            RefreshUI();
        }
    }
}

