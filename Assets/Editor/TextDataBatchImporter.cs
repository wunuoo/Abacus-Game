using UnityEngine;
using UnityEditor;
using System.IO;

public class TextDataBatchImporter : EditorWindow
{
    private TextAsset textAsset;
    private string[] textLines;

    [MenuItem("Tools/�Ի��Զ�������")]
    private static void Init()
    {
        TextDataBatchImporter window = (TextDataBatchImporter)EditorWindow.GetWindow(typeof(TextDataBatchImporter));
        window.Show();
    }

    private void OnGUI()
    {
        

        EditorGUILayout.HelpBox("��Txt�ϵ�����", MessageType.Info);

        // �����û�ѡ����Ҫת�����ļ�
        textAsset = (TextAsset)EditorGUILayout.ObjectField("Text File", textAsset, typeof(TextAsset), false);

        if (GUILayout.Button("��ʼ����") && textAsset != null)
        {
            ImportTextData();
        }
    }

    private void ImportTextData()
    {
        // ���ı��ĵ����ݰ��зָ�
        textLines = textAsset.text.Split('\n');

        //���ڴ��д���һ��dialog����
        Dialog newDialog = ScriptableObject.CreateInstance<Dialog>();

        foreach (string line in textLines)
        {

            string[] group = line.Split('��');
            group[1].Trim('\r');
            group[1] = group[1].Substring(2, group[1].Length - 4);//ȥ��ǰ������
            NPC npc;
            if(GameConfig.nameToNPC_Map.TryGetValue(group[0], out npc))
            {
                DialogNode node = new DialogNode
                {
                    name = npc.name,
                    ID = npc.ID,
                    portrait = npc.portrait,
                    content = group[1]
                };
                newDialog.dialogNodes.Add(node);
            }
            else
            {
                Debug.LogError("NPC�����ڣ�" + group[0]);
                return;
            }


        }


        // �����ļ�
        string path = AssetDatabase.GenerateUniqueAssetPath("Assets/Prefabs/�¶Ի�.asset");
        AssetDatabase.CreateAsset(newDialog, path);
        AssetDatabase.SaveAssets();

        EditorUtility.DisplayDialog("�ļ�����", "����ɹ�", "ȷ��");
    }
}
