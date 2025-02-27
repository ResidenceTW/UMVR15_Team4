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
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        PointerEventData pointerData = eventData;
        GameObject dragTarget = pointerData.pointerDrag;

        //�p�G����Tag������Item�h�NeventData�M�ū�return
        //�T�O����Drag EndDrag���|������i�H����
        if (!dragTarget.CompareTag("Item"))
        {
            eventData.pointerDrag = null;
            return;
        }

        //�N�즲����Image raycastTarget�����A�קK�g�u�Q������B��
        if (dragImage != null)
        {
            dragImage.raycastTarget = false;
        }

        dragOriginParent = dragTarget.transform.parent;
        //����Parent����Canvas�U�A�קK�Q�B��
        dragTarget.transform.SetParent(mainCanvas.transform);
        dragTarget.transform.localScale = dragTarget.transform.localScale * 1.2f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        PointerEventData pointerData = eventData;
        GameObject dragTarget = pointerData.pointerDrag;

        //�p�G�OScreenSpaceCamera�����p�A�Ѯv�g��(X
        if (mainCanvas.renderMode == RenderMode.ScreenSpaceCamera && mainCanvas.worldCamera != null)
        {
            Vector2 vOut = Vector2.zero;
            RectTransformUtility.ScreenPointToLocalPointInRectangle
                (mainCanvasRect, Input.mousePosition, mainCanvas.worldCamera, out vOut);
            dragTarget.transform.localPosition = vOut;
        }
        else
        {
            dragTarget.transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        PointerEventData pointerData = eventData;
        GameObject dragTarget = pointerData.pointerDrag;

        //�P�_����O�_�����ܨ�L�e��
        if (dragTarget.transform.parent == mainCanvas.transform)
        {
            dragTarget.transform.SetParent(dragOriginParent);
        }

        dragTarget.transform.localPosition = Vector3.zero;
        dragTarget.transform.localScale = Vector3.one;

        //���s�}��raycastTarget
        if (dragImage != null)
        {
            dragImage.raycastTarget = true;
        }
    }
}
