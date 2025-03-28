using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    public int slotIndex;
    public SkillDataSO skillData;
    public Image skillImage;
    public Image slotImage;
    public TextMeshProUGUI skillNameText;
    private PlayerHealth _currentPP;

    public Image cantUseMask;
    private bool _wasAvailable = false;
    public Image unlockEffectImage;
    public bool enableUnlockSound = false;

    private void Awake()
    {
        unlockEffectImage?.gameObject.SetActive(false);

        _currentPP = FindAnyObjectByType<PlayerHealth>().GetComponent<PlayerHealth>();
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

        RefreshSkillUI(); // ��l�ɤ]���@���B�n�ˬd
    }

    private void Update()
    {
        if (_currentPP == null)
            return;

        if (skillData == null)
        {
            cantUseMask?.gameObject.SetActive(false);
            return;
        }

        float currentPP = _currentPP.GetCurrentPP();
        bool canUse = currentPP >= skillData.PPCost;

        if (canUse && !_wasAvailable)
        {
            cantUseMask?.gameObject.SetActive(false);

            if (enableUnlockSound)
            {
                this.PlaySound("EquipSkill");
            }

            if (unlockEffectImage != null)
            {
                StartCoroutine(PlayUnlockEffect());
            }

            _wasAvailable = true;
        }
        else if (!canUse && _wasAvailable)
        {
            cantUseMask?.gameObject.SetActive(true);
            _wasAvailable = false;
        }
    }

    private IEnumerator PlayUnlockEffect()
    {
        unlockEffectImage.gameObject.SetActive(true);

        EasyInOut easyInOut = FindAnyObjectByType<EasyInOut>();

        StartCoroutine(easyInOut.ChangeValue(
           new Vector3(1.1f, 1.1f, 1.1f), new Vector3(1.3f, 1.3f, 1.3f), 0.5f,
           value => unlockEffectImage.GetComponent<RectTransform>().localScale = value,
           EasyInOut.EaseOut));

        StartCoroutine(easyInOut.ChangeValue(
            Vector4.one, new Vector4(1f, 1f, 1f, 0f), 0.5f,
           value => unlockEffectImage.color = value,
           EasyInOut.EaseOut));

        yield return null;
    }

    public void SetSlotIndex(int index)
    {
        slotIndex = index;
    }

    public void RefreshSkillUI()
    {
        if (_currentPP == null)
            return;

        if (skillData == null)
        {
            cantUseMask?.gameObject.SetActive(false);
            _wasAvailable = false;
            return;
        }

        float currentPP = _currentPP.GetCurrentPP();
        bool canUse = currentPP >= skillData.PPCost;

        cantUseMask?.gameObject.SetActive(!canUse);
        _wasAvailable = canUse;
    }

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
            skillData = null;
            skillImage.sprite = null;
            skillImage.enabled = false;
            skillNameText.text = "-";
        }

        RefreshSkillUI(); // �C����s�ޯೣ�n��s UI ���A
    }

    public void UpdateHotbarSlot()
    {
        if (skillImage == null)
        {
            Debug.LogError($"SkillSlot: {gameObject.name} �� skillImage �� null�A���խ��s����C");
            skillImage = transform.Find("SkillImage")?.GetComponent<Image>();

            if (skillImage == null)
            {
                Debug.LogError($"SkillSlot: {gameObject.name} ���M�L�k��� skillImage�I");
                return;
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

        RefreshSkillUI(); // �ֱ����s�ɤ]�P�B��s�B�n���A
    }
}
