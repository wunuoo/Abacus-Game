using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum Movement
{
    Down,
    None,
    Up
}

public class Shaft : MonoBehaviour//算盘的某一根轴
{
    int weight;
    public List<Bead> beads;
    public Vector2 prePos;
    public float upBorder;
    public float downBorder;

    public void Move(int index, Vector2 newPos)
    {
        float deltaY = newPos.y - prePos.y;
        if (deltaY < 0)//向下移动
        {
            for(int i = index; i < beads.Count; i++)
            {
                if (!beads[i].isDown)
                {
                    beads[i].transform.position += new Vector3(0, deltaY, 0);
                    //beads[i].isDown = true;

                }
                
            }
        }
        else
        {
            for (int i = index; i >= 0; i--)
            {
                if (beads[i].isDown)
                {
                    beads[i].transform.position += new Vector3(0, deltaY, 0);
                    //beads[i].isDown = false;
                }
            }
        }
        prePos = newPos;
    }

    public void RecordMouse()
    {
        prePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void Judge(int index, Vector2 endPos)
    {
        bool down = endPos.y - prePos.y < 0;//说明希望把算珠往下移动
        

        
    }
}

