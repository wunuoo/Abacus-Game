using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class UILDArea : MonoBehaviour
{
    public Animator anim;
    public bool isBig;

    public RectTransform rect;

    public void OnClickExpand()
    {
        if (!isBig)
        {
            //anim.SetTrigger("MouseEnter");
            anim.SetBool("GetMouse", true);
            isBig = true;
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (isBig && Input.GetMouseButtonDown(0))
        //{
        //    // 检测点击位置是否在UI上
        //    if (EventSystem.current.currentSelectedGameObject == null || EventSystem.current.currentSelectedGameObject != this.gameObject)
        //    {
        //        //anim.SetTrigger("MouseOut");
        //        anim.SetBool("GetMouse", false);

        //        isBig = false;
        //    }
        //}

        if (isBig && Input.GetMouseButtonDown(0))
        {
            
            if (Input.mousePosition.x > transform.position.x + rect.rect.width / 2 ||
                Input.mousePosition.y > transform.position.y + rect.rect.height / 2)
            {
                anim.SetBool("GetMouse", false);
                isBig = false;
            }
            else
            {

            }
        }


    }

}
