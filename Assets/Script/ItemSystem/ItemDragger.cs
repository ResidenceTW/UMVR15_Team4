using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragger : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Canvas mainCanvas;
    private RectTransform mainCanvasRect;
    private Transform dragOriginParent = null;
    private Image dragImage = null;
    //������slot
    private Slot originSlot;

    public Transform GetOriginalParent()
    {
        //�O�d��Parent�аO�@��Dropper�������ϥ�
         return dragOriginParent;
    }
    void Awake()
    {
        if (mainCanvas == null)
        {
            mainCanvas = GetComponentInParent<Canvas>();
        }
        if (mainCanvas != null)
        {
            mainCanvasRect = mainCanvas.GetComponent<RectTransform>();
        }
        dragImage = GetComponent<Image>();
        //���o��e��slot
        originSlot = GetComponentInParent<Slot>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //�D���欰�Ůɤ����\�즲
        if (originSlot == null || originSlot.slotItem == null) return;

        //�N�즲����Image raycastTarget�����A�קK�g�u�Q������B��
        //�q�ӵL�kĲ�oDropper�P�_
        if (dragImage != null)
        {
            dragImage.raycastTarget = false;
        }

        //����Parent����Canvas�U�A�קK�Q�B��
        dragOriginParent = transform.parent;
        transform.SetParent(mainCanvas.transform);

        transform.localScale = transform.localScale * 1.2f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //�p�G�OScreenSpaceCamera�����p�A�Ѯv�g��(X
        if (mainCanvas.renderMode == RenderMode.ScreenSpaceCamera && mainCanvas.worldCamera != null)
        {
            Vector2 vOut = Vector2.zero;
            RectTransformUtility.ScreenPointToLocalPointInRectangle
                (mainCanvasRect, Input.mousePosition, mainCanvas.worldCamera, out vOut);
            transform.localPosition = vOut;
        }
        else
        {
            transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(dragOriginParent);
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;

        //���s�}��raycastTarget
        if (dragImage != null)
        {
            dragImage.raycastTarget = true;
        }
    }

    public Slot GetOriginSlot()
    {
        // ���o�쥻��Slot
        return originSlot;
    }

    public Item GetItem()
    {
        return originSlot?.slotItem;
    }
}
