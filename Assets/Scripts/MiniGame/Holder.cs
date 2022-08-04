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
        //��������¼�ʱ������������������ӵ㣬���Ϊ�գ���ô�ͽ���ǰ����ת�Ƶ�Ŀ��㲢���ø�����
        foreach(var holder in linkHolder)
        {
            if (holder.isEmpty)
            {
                //�ƶ���(������
                currentBall.transform.position = holder.transform.position;
                currentBall.transform.SetParent(holder.transform);

                //������UI��
                holder.CheckBall(currentBall);
                this.currentBall = null;

                //�ı�״̬���߼���
                this.isEmpty = true;
                holder.isEmpty = false;


                //ÿ����һ���ƶ���������һ���Ƿ���Ϸ����
                EventHandler.CallCheckGameH2AState();
            }
        }

        
    }
}
