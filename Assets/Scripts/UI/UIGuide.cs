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
            SetText(TaskManager.Instance.currentTask == null ? "无" : "请在算盘中拨出结果：" + TaskManager.Instance.currentTask.report);
        }
        else
        {
            SetText("请继续对话");
        }
        
    }

    private void OnDestroy()
    {
        ChapterManager.Instance.OnLoadFinish.RemoveListener(Refresh);
        TaskManager.Instance.NewTaskSet.RemoveListener(this.Refresh);
    }
}
