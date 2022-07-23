using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemName ItemName;

    public void ItemClicked()
    {
        //添加到背包后隐藏物体
        gameObject.SetActive(false);
        InventoryManager.Instance.AddItem(ItemName);
        
    }
}
