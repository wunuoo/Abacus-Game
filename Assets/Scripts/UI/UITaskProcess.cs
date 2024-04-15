using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITaskProcess : MonoBehaviour
{
    public GameObject pointerPrefab;
    public Slider taskProcessSlider;

    List<UITaskPointer> pointers = new List<UITaskPointer>();

    float leftBound;
    float width;

    bool hintShowed;

    void Start()
    {
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);

        //���
        width = rectTransform.sizeDelta.x;
        // ��߽�����
        leftBound = corners[0].x;

        // �ұ߽�����
        //float right = corners[2].x;

        TaskManager.Instance.NewTaskSet += this.OnNewTask;
        TaskManager.Instance.TaskPartFinished += this.OnTaskPartFinished;
    }

    private void OnDestroy()
    {
        TaskManager.Instance.NewTaskSet -= this.OnNewTask;
        TaskManager.Instance.TaskPartFinished -= this.OnTaskPartFinished;
    }

    private void OnTaskPartFinished(object sender, EventArgs e)
    {
        taskProcessSlider.value++;
    }

    private void OnNewTask(object sender, Task task)
    {
        SetTask(task, hintShowed);
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
}
