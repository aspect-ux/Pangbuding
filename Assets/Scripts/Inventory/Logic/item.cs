using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemName ItemName;

    public void ItemClicked()
    {
        //��ӵ���������������
        gameObject.SetActive(false);
        InventoryManager.Instance.AddItem(ItemName);
        
    }
}
