using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour,ISavable
{
    Dictionary<string,bool> miniGameStateDict = new Dictionary<string,bool>();
    int gameWeek;

    private void OnEnable()
    {
        Debug.Log("oneanbale");
        //分别是用来存储迷你游戏的结束状况和读取这种状态
        //为什么要读取呢？这是用来触发事件
        EventHandler.afterSceneLoad += onAfterSceneLoad;
        EventHandler.GamePassEvent += onGamePassEvent;
    }

    private void OnDisable()
    {
        Debug.Log("ondis");
        EventHandler.afterSceneLoad -= onAfterSceneLoad;
        EventHandler.GamePassEvent -= onGamePassEvent;
    }
    private void Start()
    {
        Debug.Log("start");
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
        EventHandler.CallChangeGameState(GameStates.GamePlay);


        //保存游戏数据
        ISavable savable = this;
        savable.SavableRegister();
    }


    private void onGamePassEvent(string gameName)
    {
        //该游戏结束了
        miniGameStateDict[gameName] = true;
    }

    private void onAfterSceneLoad()
    {
        foreach (var miniGame in FindObjectsOfType<MiniGame>())
        {
            if (miniGameStateDict.TryGetValue(miniGame.gameName, out bool isPass))
            {
                miniGame.isPass = isPass;
                miniGame.UpdateMiniGameState();
            }
        }
    }

    public void SavableRegister()
    {
        SaveLoadManager.Instance.Register(this);
    }

    public GameSaveData SaveData()
    {
        GameSaveData saveData = new GameSaveData();
        saveData.week = this.gameWeek;
        return saveData;
    }

    public void RegisterGameData(GameSaveData gameSaveData)
    {
        this.gameWeek = gameSaveData.week;
    }
}
