using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    //该类用来记录背包中道具的存在与否，可互动事件（一次性）发生与否
    //防止因为切换场景，导致数据丢失错乱


    private Dictionary<ItemName,bool> itemAvaliableDict = new Dictionary<ItemName,bool>();
    private Dictionary<string,bool> interactiveStateDict = new Dictionary<string,bool>();   

    private void OnEnable()
    {
        EventHandler.beforeSceneUnload += onBeforeSceneUload;
        EventHandler.afterSceneLoad += onAfterSceneLoad;
        EventHandler.UpdateUIEvent += onUpdataUIEvent;
    }

    private void OnDisable()
    {
        EventHandler.beforeSceneUnload -= onBeforeSceneUload;
        EventHandler.afterSceneLoad -= onAfterSceneLoad;
        EventHandler.UpdateUIEvent -= onUpdataUIEvent;
    }


    private void onAfterSceneLoad()
    {
        //逻辑点在于，在别的场景中获取的道具，想要在另一个场景中从背包中使用或选中
        //就要保存这些道具数据
        //然后将场景中的所有道具加入管理道具的字典中
        foreach (var item in FindObjectsOfType<Item>())
        {
            //如果当前场景中没有之前保存的物体，那就将该物体显示出来
            if (!itemAvaliableDict.ContainsKey(item.ItemName))
            {
                Debug.Log("场景中的道具还不在背包中");
                itemAvaliableDict.Add(item.ItemName, true);
            }
            else//如果有，那么就不必多此一举，直接让物体保持之前的状态就行
            {
                Debug.Log("场景中已经存在"+itemAvaliableDict.Count);

                item.gameObject.SetActive(!itemAvaliableDict[item.ItemName]);
            }
        }

        //加载新场景，加载互动数据
        foreach (var item in FindObjectsOfType<Interactive>())
        {
            if (interactiveStateDict.ContainsKey(item.name))
            {
                item.isDone = interactiveStateDict[item.name];
            }
            else
            {
                interactiveStateDict.Add(item.name, item.isDone);
            }
        }
    }

    private void onBeforeSceneUload()
    {
        //保存已经获取的道具
        foreach (var item in FindObjectsOfType<Item>())
        {
            if (!itemAvaliableDict.ContainsKey(item.ItemName))
            {
                itemAvaliableDict.Add(item.ItemName, true);
            }
        }

        //卸载场景前，保存当前所有可互动事件的完成与否的数据
        foreach(var item in FindObjectsOfType<Interactive>())
        {
            if (interactiveStateDict.ContainsKey(item.name))
            {
                interactiveStateDict[item.name] = item.isDone;
            }
            else
            {
                interactiveStateDict.Add(item.name, item.isDone);
            }
        }
    }

    private void onUpdataUIEvent(ItemDetails itemDetails, int arg2)
    {
        if(itemDetails != null)
        {
            itemAvaliableDict[itemDetails.itemName] = false;
        }
    }
}
