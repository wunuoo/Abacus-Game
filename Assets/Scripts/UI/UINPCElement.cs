using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UINPCElement : MonoBehaviour, IPointerClickHandler
{
    public NPC charInfo;
    public UICharInfo owner;
    public Image portrait;

    public void OnPointerClick(PointerEventData eventData)
    {
        owner.OnClickCharPic(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void SetPortrait()
    {
        portrait.sprite = charInfo.portrait;
        portrait.SetNativeSize();
        gameObject.SetActive(true);
    }
}
