using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnLockSystem : MonoBehaviour
{
    private LevelSystem level;
    [SerializeField] private int unlockLevel;
    [SerializeField] private GameObject unlockMessagePrefab;
    [SerializeField] private Transform messageParent;

    private string skillName = "";

    void Start()
    {
        level = FindAnyObjectByType<LevelSystem>();
        if (level == null)
        {
            Debug.LogError("UnLockSystem: �䤣�� LevelSystem�I");
            return;
        }

        // **���զb�P�@�h (�S�̪���) �M�� SkillSlot**
        SkillSlot skillSlot = transform.parent != null ? transform.parent.GetComponentInChildren<SkillSlot>() : null;

        if (skillSlot != null && skillSlot.skillData != null)
        {
            skillName = skillSlot.skillData.skillName; // **���o�ޯ�W��**
            Debug.Log($"UnLockSystem: ���ޯ� {skillName}");
        }
    }
    void Update()
    {
        if (level.playerData.CurrentLevel >= unlockLevel)
        {
            ShowUnlockMessage();
            gameObject.SetActive(false); // **���ŹF�ЮɡA���Φۨ�**
        }
    }
    private void ShowUnlockMessage()
    {
        if (unlockMessagePrefab != null && messageParent != null)
        {
            // **�ͦ��T������**
            GameObject messageObj = Instantiate(unlockMessagePrefab, messageParent);
            TextMeshProUGUI messageText = messageObj.GetComponentInChildren<TextMeshProUGUI>();
            CanvasGroup canvasGroup = messageObj.GetComponent<CanvasGroup>();

            if (messageText != null)
                messageText.text = $"���o�ޯ�G{skillName}";
        }
    }
}
