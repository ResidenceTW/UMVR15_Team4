using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemTake : MonoBehaviour
{
    //�ŧi�Q�ߨ����D��ë��V��ScriptableObject�ݩ�
    public Item thisItem;
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
