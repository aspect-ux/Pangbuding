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
        //�ֱ��������洢������Ϸ�Ľ���״���Ͷ�ȡ����״̬
        //ΪʲôҪ��ȡ�أ��������������¼�
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


        //������Ϸ����
        ISavable savable = this;
        savable.SavableRegister();
    }


    private void onGamePassEvent(string gameName)
    {
        //����Ϸ������
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
