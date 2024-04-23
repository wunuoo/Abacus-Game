using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;

public class TextDataBatchImporter : EditorWindow
{
    private TextAsset textAsset;
    private TextAsset[] texts;
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
            int maoHaoPos = 0;

            for (int i = line.Length - 1; i >= 0; i--)
            {
                if (line[i] == '��') maoHaoPos = i;
            }
            string speaker = line.Substring(0, maoHaoPos);
            string latter = line.Substring(maoHaoPos + 1);
            char[] temp = new char[latter.Length];
            int j = 0;
            for (int i = 0; i < latter.Length; i++)
            {
                if (latter[i] != '��' && latter[i] != '��')
                {
                    temp[j] = latter[i];
                    j++;
                }
            }
            string content = string.Join("",temp).Trim(' ');



            NPC npc;
            if(GameConfig.nameToNPC_Map.TryGetValue(speaker, out npc))
            {
                DialogNode node = new DialogNode
                {
                    name = npc.name,
                    ID = npc.ID,
                    portrait = npc.portrait,
                    content = content
                };
                newDialog.dialogNodes.Add(node);
            }
            else
            {
                Debug.LogError("NPC�����ڣ�" + speaker);
                return;
            }


        }


        // �����ļ�
        string path = AssetDatabase.GenerateUniqueAssetPath("Assets/Prefabs/" + textAsset.name + ".asset");
        AssetDatabase.CreateAsset(newDialog, path);
        AssetDatabase.SaveAssets();

        EditorUtility.DisplayDialog("�ļ�����", "����ɹ�", "ȷ��");
    }
}
