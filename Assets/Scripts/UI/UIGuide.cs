using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIGuide : MonoBehaviour
{
    public TextMeshProUGUI guideText;

    string text;

    public bool showed;

    private void Start()
    {
        TaskManager.Instance.NewTaskSet.AddListener(this.Refresh);
        Refresh();
    }

    private void OnEnable()
    {
        Refresh();
    }

    public void SetText(string text)
    {
        guideText.text = text;
    }

    void Refresh()
    {
        if(EventManager.Instance.gameStatus == GameStatus.OnTask)
        {
            SetText(TaskManager.Instance.currentTask == null ? "ÎÞ" : TaskManager.Instance.currentTask.report);
        }
        else
        {
            SetText("ÆäËü");
        }
        
    }

    private void OnDestroy()
    {
        ChapterManager.Instance.OnLoadFinish.RemoveListener(Refresh);
        TaskManager.Instance.NewTaskSet.RemoveListener(this.Refresh);
    }
}
