using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITaskPointer : MonoBehaviour
{
    public TextMeshProUGUI number;
    public GameObject hintObject;

    public void SetNumber(int number)
    {
        this.number.text = number.ToString();
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
