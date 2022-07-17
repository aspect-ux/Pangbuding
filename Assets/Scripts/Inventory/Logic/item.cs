using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    public ItemName ItemName;

    public void ItemClicked()
    {
        //添加到背包后隐藏物体
        InventoryManager.Instance.AddItem(ItemName);
        gameObject.SetActive(false);
    }
}
