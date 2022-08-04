using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventHandler
{
    //更新添加物体UI的委托
    public static event Action<ItemDetails, int> UpdateUIEvent;

    //public static string test(ItemDetails itemDetails, int index) { return "where"; }
    public static void CallUpdateUIEvent(ItemDetails itemDetails,int index)
    {
        UpdateUIEvent?.Invoke(itemDetails,index);
    }


    //由于切换场景，在卸载当前场景之前要将物品数据保存，而在加载新场景后又要将物品数据加载出来
    public static event Action beforeSceneUnload;
    public static void CallBeforeSceneUload()
    {
        beforeSceneUnload?.Invoke();
    }
    public static event Action afterSceneLoad;
    public static void CallAfterSceneLoad() { afterSceneLoad?.Invoke(); }



    //选中物体点击事件
    public static event Action<ItemDetails,bool> ItemSelectEvent;

    public static void CallItemSelectEvent(ItemDetails itemDetails,bool isSelected)
    {
        ItemSelectEvent?.Invoke(itemDetails,isSelected);
    }

    //移除使用过的物体
    public static event Action<ItemName> itemUsedEvent;

    public static void CallItemUsedEvent(ItemName itemName)
    {
        itemUsedEvent?.Invoke(itemName);
    }


    //改变选中的背包中的物体
    public static event Action<int> ItemChange;
    public static void CallItemChange(int index)
    {
        ItemChange?.Invoke(index);
    }


    //显示对话UI
    public static event Action<string> ShowDialogueEvent;
    public static void CallShowDialogueEvent(string data)
    {
        ShowDialogueEvent?.Invoke(data);
    }

    //游戏状态切换,比如防止对话时切换场景
    public static event Action<GameStates> ChangeGameState;
    public static void CallChangeGameState(GameStates gameState)
    {
        ChangeGameState?.Invoke(gameState);
    }

    //用于检验H2A小游戏的结束事件
    public static event Action CheckGameH2AState;
    public static void CallCheckGameH2AState()
    {
        CheckGameH2AState?.Invoke();    
    }


}
