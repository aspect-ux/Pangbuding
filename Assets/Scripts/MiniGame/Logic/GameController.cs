using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// 用于控制当前迷你游戏，可拓展
/// </summary>
public class GameController : Singleton<GameController>
{
    public UnityEvent onFinish;


    [Header("H2A游戏数据")]
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
        //初始化子游戏界面
        DrawLine();
        CreateBall();
    }


    private void onCheckGameH2AState()
    {
        foreach(var ball in FindObjectsOfType<Ball>())
        {
            if (!ball.isMatch) return;
        }

        //迷你游戏结束
        Debug.Log("结束");
        //关闭所有holder的碰撞体，使得游戏结束后，无法再继续点击
        foreach(var holder in holderTransform)
        {
            holder.GetComponent<Collider2D>().enabled = false;
        }
        //在调用结束事件之前，先保存该迷你游戏的状态

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

        //去除所有球
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
                //设置空位置
                holderTransform[i].GetComponent<Holder>().isEmpty = true;
                continue;
            }
            //将球实例化出来，并设置图片（看球的位置是否正确）

            var ball = Instantiate(ballPrefab,holderTransform[i].transform);
            holderTransform[i].GetComponent<Holder>().isEmpty = false;

            //
            //这里需要注意，由于可能中途退出，导致数据丢失，
            //
            holderTransform[i].GetComponent<Holder>().CheckBall(ball);//判断是否正确并设置图片
            //这里通过获取已经建好的
            ball.SetupBall(gameData.GetBallDetails(gameData.ballNamesList[i]));
        }
    }
}
