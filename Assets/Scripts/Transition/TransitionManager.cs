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

    public void Transition(string from, string to)
    {
        if(!isFade)
        StartCoroutine(TransitonToScene(from, to));
    }

    /// <summary>
    /// 转换场景的协程
    /// </summary>
    /// <returns></returns>
    private IEnumerator TransitonToScene(string from,string to)
    {
        yield return Fade(1);//卸载场景前，先变黑,也可以StartCoroutine
        yield return SceneManager.UnloadSceneAsync(from);
        yield return SceneManager.LoadSceneAsync(to,LoadSceneMode.Additive);//添加到persistent,共有两个场景

        //设置新加载的场景为激活场景
        Scene sceneName = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(sceneName);

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
        Debug.Log("WRONG");
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
