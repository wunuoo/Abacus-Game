using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//���Manager�ƿ�����Ϸ�ľ�����ȣ��������
public class ChapterManager : MonoSingleton<ChapterManager>
{
    

    public bool canGoNextChapter = false;

    public List<Chapter> chapters = new List<Chapter>();
    Chapter currentChapter;

    //��Щָ������������Chapter�У�������ΪScriptableObj��Ȼ�ڲ���󲻻ᱣ��ֵ�����ڱ༭���ᣬ���µ��ǳ��鷳
    int chapterIndex = 0;
    int dialogIndex = 0;
    int taskIndex = 0;

    // Start is called before the first frame update
    protected override void OnStart()
    {
        StartChapter(chapterIndex);
    }

    //һ���½ڵĻ��������ǣ������Ի� --�� �������� --�� ������� --�� �ɹ��Ի� --�� �������� ������ --�� �����½ں���� --�� ��һ�¿����Ի�
    void StartChapter(int index)
    {
        currentChapter = chapters[index];
        AssignNewDialog();//�����Ի�
        
    }

    public void StartNewChapter()
    {
        if(canGoNextChapter)
            StartChapter(chapterIndex);
    }

    internal void AssignNewDialog()
    {
        DialogManager.Instance.PlayDialog(currentChapter.dialogs[dialogIndex]);
        dialogIndex++;
    }

    internal void OnChapterFinished()
    {
        canGoNextChapter = true;
        chapterIndex++;
    }

    internal void AssignNewTask()
    {
        TaskManager.Instance.StartNewTask(currentChapter.tasks[taskIndex]);
        taskIndex++;
    }


}
