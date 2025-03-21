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

    private void Awake()
    {
        if (slotImage == null)
        {
            slotImage = GetComponent<Image>();
            if (slotImage == null)
            {
                Debug.LogError($"SkillSlot: {gameObject.name} �S�� Image �ե�I");
            }
        }

        if (skillImage == null)
        {
            skillImage = transform.Find("SkillImage")?.GetComponent<Image>();
            if (skillImage == null)
            {
                Debug.LogError($"SkillSlot: {gameObject.name} �䤣�� SkillImage�I");
            }
        }
    }

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
        if (skillImage == null)
        {
            Debug.LogError($"SkillSlot: {gameObject.name} �� skillImage �� null�A���խ��s����C");
            skillImage = transform.Find("SkillImage")?.GetComponent<Image>();

            if (skillImage == null)
            {
                Debug.LogError($"SkillSlot: {gameObject.name} ���M�L�k��� skillImage�I");
                return; // ����i�@�B���~
            }
        }

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
