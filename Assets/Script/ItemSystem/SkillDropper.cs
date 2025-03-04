using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillDropper : MonoBehaviour, IDropHandler
{
    private SkillSlot targetSlot;

    void Awake()
    {
        targetSlot = GetComponent<SkillSlot>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        SkillDragger dragger = eventData.pointerDrag.GetComponent<SkillDragger>();
        if (dragger == null) return;

        //�u���\�ޯ�즲��SkillHotbar
        if (transform.CompareTag("SkillHotbar") && dragger.gameObject.CompareTag("SkillList"))
        {
            //�Φ���k�d���ޭȡA�קKHierarchy�Ƴ\�y�����ޭȿ��ê����D�I�I�I
            int slotIndex = SkillHotbarManager.instance.hotbarSlots.IndexOf(GetComponent<SkillSlot>());
            
            SkillHotbarManager.instance.AssignSkillToHotbar(dragger.GetSkill(), slotIndex);
            SkillHotbarManager.instance.RefreshHotbarUI();
        }
    }
}
