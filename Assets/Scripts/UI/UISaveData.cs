using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UISaveData : MonoBehaviour
{
    public TextMeshProUGUI saveTime;
    public TextMeshProUGUI chapterIndex;

    public GameObject emptySave;
    public GameObject fullSave;
    public GameObject emptySaveButton;

    public GameObject fullSaveButton;
    public GameObject fullLoadButton;

    public int slotIndex;

    bool full;

    public void SetSaveInfo(Save save)
    {
        if(save != null)
        {
            this.saveTime.text = save.saveTime.ToString();
            this.chapterIndex.text = save.chapterIndex.ToString();
            full = true;
        }

        emptySave.SetActive(!full);
        fullSave.SetActive(full);
        emptySaveButton.SetActive(!SceneManager.Instance.IsOnTitle());//�ڱ��⻭�棬����������Ϸ
        fullLoadButton.SetActive(SceneManager.Instance.IsOnTitle());//�ڱ��⻭�棬�������ȡ��Ϸ
        fullSaveButton.SetActive(!SceneManager.Instance.IsOnTitle());//�ڱ��⻭�棬����������Ϸ
    }

    public void Refresh()
    {

    }

    public void OnClickSave()
    {
        SaveManager.Instance.Save(slotIndex);
    }

    public void OnClickLoad()
    {
        SaveManager.Instance.Load(slotIndex);
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
