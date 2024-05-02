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
    public Dictionary<int, bool> toolGottenTable;//����Ŀǰ�Ѿ��õ�����Ʒ
    public Dictionary<int, bool> npcMeetTable;//����Ŀǰ�Ѿ�֪����NPC
    public GameStatus gamestatus;

    public DateTime saveTime;//����浵ʱ���
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
        //д��浵

        string filePath = Application.dataPath + "/gameSave" + slotIndex + ".save";
        //���������Ƹ�ʽ�������ļ���
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(filePath);
        //��save�������л���file��
        bf.Serialize(file, save);
        file.Close();

        this.saves[slotIndex] = save;
        OnGameSave?.Invoke();

    }

    //�ڴ浵ϵͳ����ʱ����ȡ�����ļ������ڴ�
    public void GetSaves(int slotIndex)
    {
        string filePath = Application.dataPath + "/gameSave" + slotIndex + ".save";

        Save save = null;
        //����Ŀ��λ���Ƿ��д浵
        if (File.Exists(filePath))
        {
            //���������Ƹ�ʽ�����򣬴��ļ���
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(filePath, FileMode.Open);

            //��file�������л���save����		
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
            Debug.LogError("�浵Ϊ�գ�");

        }
        else
        {
            currentSave = save;
        }
        //�������ݵĲ�����ChapterManager���
        SceneManager.Instance.LoadScene("Main");

    }

}
