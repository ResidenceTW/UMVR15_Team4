using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagScrollReset : MonoBehaviour
{
    void Start()
    {
        ScrollRect scrollRect = GetComponentInParent<ScrollRect>();
        if (scrollRect != null)
        {
            // �]�� 1 �N��ƨ�̤W��
            scrollRect.verticalNormalizedPosition = 1; 
        }
    }
}
