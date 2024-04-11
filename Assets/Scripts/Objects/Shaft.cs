using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Math;

public class Shaft : MonoBehaviour//算盘的某一根轴
{
    int weight;
    public List<Bead> beads;
    public Vector2 preMousePos;
    
    Vector2 originMousePos;
    public List<Bead> movingBeads = new List<Bead>();
    public static float moveThreshold = 0.3f;
    public  float maxMoveDistance = 1.4f;
    
    public void Move(int index, Vector2 newMousePos)
    {
        float deltaY = newMousePos.y - preMousePos.y;
        bool canMoveDown = !beads[index].isDown;
        bool toMax = false;

        //第一次移动,确认需要移动的算珠，添加到列表
        if (movingBeads.Count == 0)
        {
            if (deltaY == 0)
            {

            }
            else if (deltaY < 0)//向下移动
            {
                for (int i = index; i < beads.Count; i++)
                {
                    if (!beads[i].isDown)
                    {
                        movingBeads.Add(beads[i]);
                    }
                }
            }
            else
            {
                for (int i = index; i >= 0; i--)
                {
                    if (beads[i].isDown)
                    {
                        movingBeads.Add(beads[i]);
                    }
                }
            }

        }

        float allDistance = newMousePos.y - originMousePos.y;

        toMax = Abs(allDistance) > maxMoveDistance;//如果总移动距离超过上限，标记此次移动后将会超限


        if ((canMoveDown && allDistance <= 0) || (!canMoveDown && allDistance >= 0))
        {
            //
        }
        else//无效移动
        {
            deltaY = 0;
        }

        foreach (Bead bead in movingBeads)
        {
            Vector2 nextPos = bead.transform.position + new Vector3(0, deltaY, 0);
            if (toMax)
            {
                bead.MoveToSlot();
            }
            else
            {
                bead.MoveTo(nextPos);
            }
            
        }

        preMousePos = newMousePos;
    }

    public void RecordMouse()
    {
        preMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        originMousePos = preMousePos;
    }

    public void Judge(int index, Vector2 endPos)
    {
        float deltaY = endPos.y - originMousePos.y;
        Debug.Log(deltaY);
        
        if (deltaY < moveThreshold && deltaY > -moveThreshold)//不移动
        {
            ResetPos();
        }
        else
        {
            foreach(var bead in movingBeads)
            {
                bead.ChangeSlot(this.maxMoveDistance);
                bead.MoveToSlot();
            }
        }

        movingBeads.Clear();
    }

    private void ResetPos()
    {
        foreach (var bead in movingBeads)
        {
            bead.MoveToSlot();
        }
    }
}

