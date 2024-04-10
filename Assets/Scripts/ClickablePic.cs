using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ClickablePic : MonoBehaviour
{
    public GameObject normal_Pic;
    public GameObject active_Pic;
    public GameObject click_Pic;


    public void OnMouseDown()
    {
        Debug.Log("c");
        click_Pic.SetActive(true);
        normal_Pic.SetActive(false);
        active_Pic.SetActive(false);
        this.OnClick();
    }

    public void OnMouseEnter()
    {
        Debug.Log("e");

        active_Pic.SetActive(true);
        click_Pic.SetActive(false);
        normal_Pic.SetActive(false);

    }

    public void OnMouseExit()
    {
        normal_Pic.SetActive(true);
        click_Pic.SetActive(false);
        active_Pic.SetActive(false);
    }

    public virtual void OnClick()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
