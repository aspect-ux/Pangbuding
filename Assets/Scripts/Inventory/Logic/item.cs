using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    public ItemName ItemName;

    public void ItemClicked()
    {
        //��ӵ���������������
        InventoryManager.Instance.AddItem(ItemName);
        gameObject.SetActive(false);
    }
}
