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

    public bool isDown = true;//在下面是true

    public void Start()
    {
        slotPos = transform.position;
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

    public void ChangeSlot(float maxDistance)
    {
        this.isDown = !isDown;//若之前算珠在下，现在isdown应该是false =》 此次位移是往上
        slotPos += new Vector2(0, isDown ? -maxDistance : maxDistance);//当isDown为true，说明此次位移是往下
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

