using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bead : MonoBehaviour
{
    public static float height = 0.1f;
    public int index;
    public Shaft owner;
    bool selected;
    Vector2 slotPos;//算珠目前停靠的位置
    public Vector2 otherSlotPos;//算珠另一个停靠的位置

    public bool isDown;//在下面是true

    public void Start()
    {
        slotPos = transform.position;

        float distanceY = isDown ? owner.maxMoveDistance : -owner.maxMoveDistance;
        otherSlotPos = slotPos + new Vector2(0, distanceY);
    }

    public void OnMouseOver()//跟随鼠标
    {
        if (Input.GetMouseButtonDown(0))
        {
            selected = true;
        }

    }

    public void OnMouseDown()
    {
        owner.RecordMouse();
        //owner.preMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    }
    public void OnMouseUp()
    {
        owner.Judge(this.index, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        selected = false;
    }

    private void Update()
    {

        //Debug.Log(Camera.main.ScreenToViewportPoint(Input.mousePosition))
        if (selected)
        {
            
            Vector2 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            

            owner.Move(this.index, newPos);
            //transform.position = new Vector2(transform.position.x, transform.position.y + deltaPosY);
            
        }
    }

    public void ChangeSlot()
    {
        this.isDown = !isDown;//若之前算珠在下，现在isdown应该是false =》 此次位移是往上
        Vector2 temp = slotPos;
        slotPos = otherSlotPos;//当isDown为true，说明此次位移是往下
        otherSlotPos = temp;
    }

    public void MoveToSlot()
    {
        transform.position = slotPos;
    }

    public void MoveTo(Vector2 Destination)
    {
        transform.position = Destination;
    }
}

