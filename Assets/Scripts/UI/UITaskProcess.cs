using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        RectTransform rectTransform = taskProcessSlider.GetComponent<RectTransform>();
        //���
        width = rectTransform.sizeDelta.x;
        Refresh();

        TaskManager.Instance.NewTaskSet.AddListener(this.OnNewTask);
        TaskManager.Instance.TaskStatusChange.AddListener(this.Refresh);
    }

    public void Refresh()
    {
        if (TaskManager.Instance.currentTask != null)
        {
            if (!taskHaveSet)
            {
                SetTask(TaskManager.Instance.currentTask, hintShowed);
                taskHaveSet = true;
            }

        }
        else
        {
            foreach (var pointer in pointers)
            {
                Destroy(pointer.gameObject);
            }
            this.pointers.Clear();
            taskHaveSet = false;
        }


        taskProcessSlider.value = TaskManager.Instance.resultIndex;
    }

    private void OnDestroy()
    {
        TaskManager.Instance.NewTaskSet.RemoveListener(this.OnNewTask);
        TaskManager.Instance.TaskStatusChange.RemoveListener(this.Refresh);
    }

    private void OnNewTask()
    {
        Refresh();
    }

    public void SetTask(Task task, bool useHint)//ΪUI���ô������������Ϣ
    {
        int pointCount = task.results.Length;
        taskProcessSlider.maxValue = pointCount;


        float stepWidth = width / (task.results.Length);

        for(int i = 0; i < pointCount; i++)//��1��ʼ����Ϊ��һ���м���������߽���Ҳ�һ��
        {
            GameObject go = Instantiate(pointerPrefab, gameObject.transform);
            go.transform.position += new Vector3((i + 1) * stepWidth, 0, 0);//������ݲ�����������

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
            pointer.hintObject.SetActive(hintShowed);//��hint�Ѿ�չʾ����Ӧ������
        }

    }

    public void OnClickClear()
    {
        UIMain.Instance.OnClickSuanPan();

    }
}
