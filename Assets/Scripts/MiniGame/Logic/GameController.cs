using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// ���ڿ��Ƶ�ǰ������Ϸ������չ
/// </summary>
public class GameController : Singleton<GameController>
{
    public UnityEvent onFinish;


    [Header("H2A��Ϸ����")]
    public GameH2A_so gameData;

    public Transform[] holderTransform;

    public LineRenderer linePrefab;

    public Ball ballPrefab;

    public GameObject lineParent;

    private void OnEnable()
    {
        EventHandler.CheckGameH2AState += onCheckGameH2AState;
    }

    private void OnDisable()
    {
        EventHandler.CheckGameH2AState -= onCheckGameH2AState;
    }

    

    private void Start()
    {
        //��ʼ������Ϸ����
        DrawLine();
        CreateBall();
    }


    private void onCheckGameH2AState()
    {
        foreach(var ball in FindObjectsOfType<Ball>())
        {
            if (!ball.isMatch) return;
        }

        //������Ϸ����
        Debug.Log("����");
        //�ر�����holder����ײ�壬ʹ����Ϸ�������޷��ټ������
        foreach(var holder in holderTransform)
        {
            holder.GetComponent<Collider2D>().enabled = false;
        }
        //�ڵ��ý����¼�֮ǰ���ȱ����������Ϸ��״̬

        EventHandler.CallGamePassEvent(gameData.gameName);
        onFinish?.Invoke();
    }

    public void DrawLine()
    {
        foreach(var connections in gameData.LineConnectionsList)
        {
            var line = Instantiate(linePrefab, lineParent.transform);

            line.SetPosition(0, holderTransform[connections.from].position);
            line.SetPosition(1, holderTransform[connections.to].position);

            holderTransform[connections.from].GetComponent<Holder>().linkHolder.Add(holderTransform[connections.to].GetComponent<Holder>());
            holderTransform[connections.to].GetComponent<Holder>().linkHolder.Add(holderTransform[connections.from].GetComponent<Holder>());
        }

        
    }

    public void ResetGame()
    {

        //ȥ��������
        foreach(var holder in holderTransform)
        {
            if(holder.childCount > 0)
                Destroy(holder.GetChild(0).gameObject);
        }
        CreateBall();
    }

    public void CreateBall()
    {
        for(int i = 0; i < gameData.ballNamesList.Count; i++)
        {
            if (gameData.ballNamesList[i] == BallName.None)
            {
                //���ÿ�λ��
                holderTransform[i].GetComponent<Holder>().isEmpty = true;
                continue;
            }
            //����ʵ����������������ͼƬ�������λ���Ƿ���ȷ��

            var ball = Instantiate(ballPrefab,holderTransform[i].transform);
            holderTransform[i].GetComponent<Holder>().isEmpty = false;

            //
            //������Ҫע�⣬���ڿ�����;�˳����������ݶ�ʧ��
            //
            holderTransform[i].GetComponent<Holder>().CheckBall(ball);//�ж��Ƿ���ȷ������ͼƬ
            //����ͨ����ȡ�Ѿ����õ�
            ball.SetupBall(gameData.GetBallDetails(gameData.ballNamesList[i]));
        }
    }
}
