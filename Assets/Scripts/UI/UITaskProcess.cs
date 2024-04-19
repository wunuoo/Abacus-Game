using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITaskProcess : MonoBehaviour
{
    public GameObject pointerPrefab;
    public Slider taskProcessSlider;

    bool taskHaveSet;

    List<UITaskPointer> pointers = new List<UITaskPointer>();

    float width;

    bool hintShowed;

    void Start()
    {
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        //宽度
        width = rectTransform.sizeDelta.x;
        Refresh();

        TaskManager.Instance.NewTaskSet += this.OnNewTask;
        TaskManager.Instance.TaskStatusChange.AddListener(this.Refresh);
    }

    public void Refresh()
    {
        if (!taskHaveSet)
        {
            SetTask(TaskManager.Instance.currentTask, hintShowed);
            taskHaveSet = true;
        }

        taskProcessSlider.value = TaskManager.Instance.resultIndex;
    }

    private void OnDestroy()
    {
        TaskManager.Instance.NewTaskSet -= this.OnNewTask;
        TaskManager.Instance.TaskStatusChange.RemoveListener(this.Refresh);
    }

    private void OnNewTask(object sender, Task task)
    {
        Refresh();
    }

    public void SetTask(Task task, bool useHint)//为UI设置打算盘任务的消息
    {
        int pointCount = task.results.Length;
        taskProcessSlider.maxValue = pointCount;


        float stepWidth = width / (task.results.Length);

        for(int i = 0; i < pointCount; i++)//从1开始，因为第一个中间结果处于左边界的右侧一格
        {
            GameObject go = Instantiate(pointerPrefab, gameObject.transform);
            go.transform.position += new Vector3((i + 1) * stepWidth, 0, 0);//逐个根据步长调整坐标

            UITaskPointer ui = go.GetComponent<UITaskPointer>();
            ui.SetNumber(task.results[i]);
            ui.hintObject.SetActive(useHint);
            pointers.Add(ui);

            go.SetActive(true);
        }
    }



    public void OnClickHint()
    {
        hintShowed = !hintShowed;
        foreach (var pointer in pointers)
        {
            pointer.hintObject.SetActive(hintShowed);//若hint已经展示，则应该隐藏
        }

    }
}
