using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Save
{
    public int chapterIndex;
    public int dialogIndex;
    public int dialogNodeIndex;
    public int taskIndex;
    public int pptIndex;

    public int recordIndex;
    public Dictionary<int, bool> toolGottenTable;//保存目前已经得到的物品
    public Dictionary<int, bool> npcMeetTable;//保存目前已经知晓的NPC
    public GameStatus gamestatus;

    public DateTime saveTime;//保存存档时间戳
}


public class SaveManager : Singleton<SaveManager>
{
    public Save[] saves = new Save[3];

    public UnityAction OnGameSave;

    internal bool saveMode;

    public Save currentSave;

    public SaveManager()
    {
        for (int i = 0; i < saves.Length; i++)
        {
            GetSaves(i);
        }
    }

    public void Save(int slotIndex)
    {
        Save save = new Save();

        save.chapterIndex = ChapterManager.Instance.chapterIndex;
        save.dialogIndex = ChapterManager.Instance.dialogIndex;
        save.dialogNodeIndex = DialogManager.Instance.nodeIndex;
        save.taskIndex = ChapterManager.Instance.taskIndex;
        save.recordIndex = RecordManager.Instance.recordsUnlockIndex;
        save.pptIndex = CGManager.Instance.pptIndex;
        save.toolGottenTable = ToolManager.Instance.toolGotten;
        save.npcMeetTable = CharInfoManager.Instance.npcMeet;

        save.gamestatus = EventManager.Instance.gameStatus;
        save.saveTime = DateTime.Now;
        //写入存档

        string filePath = Application.dataPath + "/gameSave" + slotIndex + ".save";
        //创建二进制格式化程序及文件流
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(filePath);
        //将save对象序列化到file流
        bf.Serialize(file, save);
        file.Close();

        this.saves[slotIndex] = save;
        OnGameSave?.Invoke();

    }

    //在存档系统启动时，读取各个文件进入内存
    public void GetSaves(int slotIndex)
    {
        string filePath = Application.dataPath + "/gameSave" + slotIndex + ".save";

        Save save = null;
        //检验目标位置是否有存档
        if (File.Exists(filePath))
        {
            //创建二进制格式化程序，打开文件流
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(filePath, FileMode.Open);

            //将file流反序列化到save对象		
            save = (Save)bf.Deserialize(file);
            file.Close();


        }
        saves[slotIndex] = save;
    }

    public void Load(int slotIndex)
    {
        SoundManager.Instance.PlaySound(GameConfig.ButtonSound);


        Save save = saves[slotIndex];
        if(save == null)
        {
            Debug.LogError("存档为空！");

        }
        else
        {
            currentSave = save;
        }
        //读入数据的操作由ChapterManager完成
        SceneManager.Instance.LoadScene("Main");

    }

}
