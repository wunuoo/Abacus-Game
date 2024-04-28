using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bead : MonoBehaviour
{
    public int index;
    public Shaft owner;
    bool selected;
    Vector2 slotPos;//����Ŀǰͣ����λ��
    public Vector2 otherSlotPos;//������һ��ͣ����λ��

    public bool isDown;//��������true

    public bool hasToMax = false;//true = �������Ѿ�������һ��

    public void Start()
    {
        slotPos = transform.position;

        float distanceY = isDown ? owner.maxMoveDistance : -owner.maxMoveDistance;
        otherSlotPos = slotPos + new Vector2(0, distanceY);
    }

    public void OnMouseOver()//�������
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
        this.isDown = !isDown;//��֮ǰ�������£�����isdownӦ����false =�� �˴�λ��������
        Vector2 temp = slotPos;
        slotPos = otherSlotPos;//��isDownΪtrue��˵���˴�λ��������
        otherSlotPos = temp;

        hasToMax = false;//�Ѿ������ˣ�Ӧ������Ϊ��
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

