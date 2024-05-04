using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "创建章节", fileName = "章节")]
public class Chapter : ScriptableObject
{
    public string chapterName;

    public Dialog[] dialogs;

    public Task[] tasks;

    public Sprite envBG;

    public bool inShop;

    public bool hasOpenCG;

    //每当获取一个新对话或者任务，就会自动让指针+1，请注意
}
