using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ContinueGame()
    {

    }

    public void GoBackToGame()
    {

        //回到主菜单
        var currentScene = SceneManager.GetActiveScene().name;

        TransitionManager.Instance.Transition(currentScene, "Menu");

        //保存游戏进度
    }



    //多周目游戏...省略
}
