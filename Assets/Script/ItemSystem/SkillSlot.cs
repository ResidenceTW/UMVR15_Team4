using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//�ƻsmyBag�t�Ψӧ�g
public class SkillSlot : MonoBehaviour
{   
    public int slotIndex;
    public SkillDataSO skillData;
    public Image skillImage;
    public Image slotImage;
    public TextMeshProUGUI skillNameText;

    public void SetSlotIndex(int index)
    {
        slotIndex = index;
    }

    //��s�ޯ�C��UI
    public void UpdateSkillListSlot()
    {
        SkillListManager skillListManager = SkillListManager.instance;

        if (skillListManager.mySkill.skillList.Count > slotIndex)
        {
            skillData = skillListManager.mySkill.skillList[slotIndex];

            slotImage.sprite = skillData.skillIcon;
            skillImage.sprite = skillData.skillIcon;
            skillImage.enabled = true;
            skillNameText.text = skillData.skillName;
        }
        else
        {
            skillImage.sprite = null;
            skillImage.enabled = false;
            skillNameText.text = "-";
        }
    }

    //��s�ޯ�ֱ���UI
    public void UpdateHotbarSlot()
    {
        if (skillData != null)
        {
            skillImage.sprite = skillData.skillIcon;
            skillImage.enabled = true;
        }
        else
        {
            skillImage.sprite = null;
            skillImage.enabled = false;
        }
    }
}
