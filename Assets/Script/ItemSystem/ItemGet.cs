using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//��Script��b�������n�Q�ߨ����D��
public class ItemGet : MonoBehaviour
{
    //�ŧi�Q�ߨ����D��ë��V��ScriptableObject�ݩ�
    public ItemData thisItem;
    public Inventory playerInventory;
    public InventoryManager inventoryManager;

    //���աA�����ߨ�
    private void OnMouseDown()
    {
        if (thisItem != null)
        {
            inventoryManager.AddItem(thisItem);
            
            //Destroy(gameObject); 
        }
    }
}
