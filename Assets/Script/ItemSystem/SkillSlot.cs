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
    public TextMeshProUGUI skillNameText;

    public void SetSlotIndex(int index)
    {
        slotIndex = index;
    }

    public void UpdateSlot()
    {
        SkillListManager skillListManager = SkillListManager.instance;

        if (skillListManager.mySkill.skillList.Count > slotIndex)
        {
            skillData = skillListManager.mySkill.skillList[slotIndex];
           
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
}
