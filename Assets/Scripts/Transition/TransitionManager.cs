using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TransitionManager : Singleton<TransitionManager>
{
    private bool isFade;
    //控制渐变
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration;

    private bool canTransition;


    private void Start()
    {
        //todo:待调整
        canTransition = true;
    }
    private void OnEnable()
    {
        EventHandler.ChangeGameState += onChangeGameState;
    }

    private void OnDisable()
    {
        EventHandler.ChangeGameState -= onChangeGameState;
    }

    private void onChangeGameState(GameStates gameStates)
    {
        if (gameStates == GameStates.Pause)
            canTransition = false;
        else
            canTransition = true;
    }

    public void Transition(string from, string to)
    {
        if(!isFade && canTransition)
            StartCoroutine(TransitonToScene(from, to));
    }

    /// <summary>
    /// 转换场景的协程
    /// </summary>
    /// <returns></returns>
    private IEnumerator TransitonToScene(string from,string to)
    {
        yield return Fade(1);//卸载场景前，先变黑,也可以StartCoroutine

        //如果为空，就直接加载不用卸载
        if (from != string.Empty)
        {
            EventHandler.CallBeforeSceneUload();//保存背包物体数据

            yield return SceneManager.UnloadSceneAsync(from);
        }
       
        yield return SceneManager.LoadSceneAsync(to,LoadSceneMode.Additive);//添加到persistent,共有两个场景

        //设置新加载的场景为激活场景
        Scene sceneName = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(sceneName);

        EventHandler.CallAfterSceneLoad();//加载出背包数据

        yield return Fade(0);//场景激活后显示场景
    }


    /// <summary>
    /// 淡入淡出场景
    /// </summary>
    /// <param name="targetAlpha">1是黑,0是透明</param>
    /// <returns></returns>
    private IEnumerator Fade(float targetAlpha)
    {
        isFade = true;
        fadeCanvasGroup.blocksRaycasts = true;
        float speed = Mathf.Abs(fadeCanvasGroup.alpha - targetAlpha) / fadeDuration;

        while (!Mathf.Approximately(fadeCanvasGroup.alpha, targetAlpha))
        {
            //如果渐变没好,按照固定速度变化
            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, targetAlpha, speed*Time.deltaTime); //这里的speed=1/0.3,要乘以deltaTime不然速度回很快，一般来说，速度都要乘以
            //deltaTime，实现每帧变化
            yield return null;
        }
        //结束循环则变换完成，那么所有设置要换回来
        fadeCanvasGroup.blocksRaycasts = false;
        isFade = false;
        
    }
}
