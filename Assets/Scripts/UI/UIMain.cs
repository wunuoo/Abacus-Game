using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMain : MonoBehaviour
{
    public Button button_NextChapter;

    void Refresh()
    {
        button_NextChapter.gameObject.SetActive(ChapterManager.Instance.canGoNextChapter);
    }


    public void OnClickNextChapter()
    {
        ChapterManager.Instance.StartNewChapter();
    }

    public void TestDialog()
    {
        //DialogManager.Instance.PlayDialog(0);
    }

    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.OnChapterFinish += this.Refresh;
        Refresh();
    }

    private void OnDestroy()
    {
        EventManager.Instance.OnChapterFinish -= this.Refresh;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
