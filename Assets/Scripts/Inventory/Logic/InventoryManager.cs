using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    //由清单类来控制道具数据,比如获取是否存在，加入道具，删除道具
    public ItemDataList_so itemData;

    [SerializeField]private List<ItemName> itemList = new List<ItemName>();

    private void OnEnable()
    {
        EventHandler.itemUsedEvent += onItemUsedEvent;
    }

    private void OnDisable()
    {
        EventHandler.itemUsedEvent -= onItemUsedEvent;
    }

    private void onItemUsedEvent(ItemName itemName)
    {
        //将使用过的物品移除清单
        var index = GetItemIndex(itemName);
        itemList.RemoveAt(index);


        //更新UI
        if (itemList.Count == 0)
            EventHandler.CallUpdateUIEvent(null,-1);
    }

    public void AddItem(ItemName itemName)
    {
        if (!itemList.Contains(itemName))
        {
            itemList.Add(itemName);
            //ui显示
            try
            {
                EventHandler.CallUpdateUIEvent(itemData.GetItemDetails(itemName), itemList.Count - 1);
            }
            catch
            {
                if (itemData.GetItemDetails(itemName) == null)
                    Debug.Log("wrong");
            }
            finally
            {
                
            }
            
        }
        
    }

    //异步返回类型
    private int GetItemIndex(ItemName itemName)
    {
        for(int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i] == itemName)
            {
                return i;
            }
        }
        return -1;
    }
}
