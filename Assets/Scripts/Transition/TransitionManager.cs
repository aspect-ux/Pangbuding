using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TransitionManager : Singleton<TransitionManager>
{
    private bool isFade;
    //���ƽ���
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration;

    private bool canTransition;


    private void Start()
    {
        //todo:������
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
    /// ת��������Э��
    /// </summary>
    /// <returns></returns>
    private IEnumerator TransitonToScene(string from,string to)
    {
        yield return Fade(1);//ж�س���ǰ���ȱ��,Ҳ����StartCoroutine

        //���Ϊ�գ���ֱ�Ӽ��ز���ж��
        if (from != string.Empty)
        {
            EventHandler.CallBeforeSceneUload();//���汳����������

            yield return SceneManager.UnloadSceneAsync(from);
        }
       
        yield return SceneManager.LoadSceneAsync(to,LoadSceneMode.Additive);//��ӵ�persistent,������������

        //�����¼��صĳ���Ϊ�����
        Scene sceneName = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(sceneName);

        EventHandler.CallAfterSceneLoad();//���س���������

        yield return Fade(0);//�����������ʾ����
    }


    /// <summary>
    /// ���뵭������
    /// </summary>
    /// <param name="targetAlpha">1�Ǻ�,0��͸��</param>
    /// <returns></returns>
    private IEnumerator Fade(float targetAlpha)
    {
        isFade = true;
        fadeCanvasGroup.blocksRaycasts = true;
        float speed = Mathf.Abs(fadeCanvasGroup.alpha - targetAlpha) / fadeDuration;

        while (!Mathf.Approximately(fadeCanvasGroup.alpha, targetAlpha))
        {
            //�������û��,���չ̶��ٶȱ仯
            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, targetAlpha, speed*Time.deltaTime); //�����speed=1/0.3,Ҫ����deltaTime��Ȼ�ٶȻغܿ죬һ����˵���ٶȶ�Ҫ����
            //deltaTime��ʵ��ÿ֡�仯
            yield return null;
        }
        //����ѭ����任��ɣ���ô��������Ҫ������
        fadeCanvasGroup.blocksRaycasts = false;
        isFade = false;
        
    }
}
