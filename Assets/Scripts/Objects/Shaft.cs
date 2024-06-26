using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Math;

public class Shaft : MonoBehaviour//算盘的某一根轴
{
    public SuanPan owner;
    public int weight;
    public List<Bead> beads;
    public bool isShort;//短轴初始位置是在上的

    public Vector2 preMousePos;//上一帧的鼠标位置
    Vector2 originMousePos;
    public List<Bead> movingBeads = new List<Bead>();

    public static float moveThreshold = 0.3f;
    public  float maxMoveDistance;

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


        if ((canMoveDown && allDistance <= 0) || (!canMoveDown && allDistance >= 0))//向下移动，delta是负数
        {
            //正常移动，什么也不做
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
                if (!bead.hasToMax)
                {
                    System.Random rand = new System.Random();
                    string soundName;
                    switch (rand.Next(0, 2))
                    {
                        case 0:
                            soundName = "bead";
                            break;
                        case 1:
                            soundName = "bead2";
                            break;
                        case 2:
                            soundName = "bead3";
                            break;
                        default:
                            soundName = "bead";
                            break;
                    }
                    SoundManager.Instance.PlaySound(soundName);
                    bead.hasToMax = true;
                }
                bead.MoveTo(bead.otherSlotPos);
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

        

        if ((beads[index].isDown && deltaY > moveThreshold) || (!beads[index].isDown && deltaY < -moveThreshold))
        {
            bool active = isShort ? !beads[index].isDown : beads[index].isDown;//移动有效，这次会是哪种移动
            foreach (var bead in movingBeads)
            {
                bead.ChangeSlot();
                bead.MoveToSlot();
            }

            owner.Add(movingBeads.Count * (active ? weight : -weight));
        }
        else
        {
            ResetPos();
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

