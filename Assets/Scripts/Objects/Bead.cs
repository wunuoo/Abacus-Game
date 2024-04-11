using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Bead : MonoBehaviour
{
    public static float height = 0.1f;
    public int index;
    public Shaft owner;
    bool selected;
    public bool isDown = true;//在下面是true
    
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
}

