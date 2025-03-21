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
        skillListManager = GetComponent<SkillListManager>();

        if (instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;

        // �T�O hotbarSlots �Q��l��
        if (hotbarSlots == null || hotbarSlots.Count == 0)
        {
            hotbarSlots = new List<SkillSlot>(GetComponentsInChildren<SkillSlot>());
        }

        foreach (var slot in hotbarSlots)
        {
            if (slot == null)
            {
                Debug.LogError("SkillHotbarManager: hotbarSlots ���� null �ȡI");
                continue;
            }

            slot.slotImage = slot.GetComponent<Image>();

            if (slot.slotImage == null)
            {
                Debug.LogError($"SkillHotbarManager: Slot {slot.name} �S�� Image �ե�I");
            }
        }
    }

    private void Start()
    {
        // �T�O�C�� hotbarSlot �����T�� index
        if (hotbarSlots == null || hotbarSlots.Count == 0)
        {
            Debug.LogWarning("SkillHotbarManager: hotbarSlots �b Start �ɤ����šA���խ��s����I");
            hotbarSlots = new List<SkillSlot>(GetComponentsInChildren<SkillSlot>());
        }

        for (int i = 0; i < hotbarSlots.Count; i++)
        {
            hotbarSlots[i].SetSlotIndex(i);
        }

        // �T�O UI ��l��
        RefreshHotbarUI();
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

                switch (i)
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
        if (hotbarSlots == null)
        {
            Debug.LogError("SkillHotbarManager: hotbarSlots �� null�A�i����������᥼��l�ơI");
            return;
        }

        if (hotbarSlots.Count == 0)
        {
            Debug.LogError("SkillHotbarManager: hotbarSlots �ƶq�� 0�I");
            return;
        }

        foreach (var slot in hotbarSlots)
        {
            if (slot == null)
            {
                Debug.LogError("SkillHotbarManager: �Y�� hotbarSlot �� null�A���L��s�I");
                continue;
            }

            slot.UpdateHotbarSlot();
        }
    }
}
