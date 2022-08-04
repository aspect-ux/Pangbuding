using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holder : Interactive
{
    public BallName matchBall;
    public Ball currentBall;

    public HashSet<Holder> linkHolder = new HashSet<Holder>();
    public bool isEmpty;


    public void CheckBall(Ball ball)
    {
        currentBall = ball;
        if(ball.ballDetails.ballName == matchBall)
        {
            currentBall.isMatch = true;
            currentBall.SetRight();
        }
        else
        {
            currentBall.isMatch = false;
            currentBall.SetWrong();
        }
    }

    public override void EmptyClick()
    {
        //发生点击事件时，遍历该球的所有连接点，如果为空，那么就将当前的球转移到目标点并设置父物体
        foreach(var holder in linkHolder)
        {
            if (holder.isEmpty)
            {
                //移动球(物理上
                currentBall.transform.position = holder.transform.position;
                currentBall.transform.SetParent(holder.transform);

                //交换球（UI上
                holder.CheckBall(currentBall);
                this.currentBall = null;

                //改变状态（逻辑上
                this.isEmpty = true;
                holder.isEmpty = false;


                //每发生一次移动，都检验一次是否游戏结束
                EventHandler.CallCheckGameH2AState();
            }
        }

        
    }
}
