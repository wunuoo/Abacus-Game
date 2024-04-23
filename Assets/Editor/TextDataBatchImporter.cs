using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;

public class TextDataBatchImporter : EditorWindow
{
    private TextAsset textAsset;
    private TextAsset[] texts;
    private string[] textLines;

    [MenuItem("Tools/对话自动创建机")]
    private static void Init()
    {
        TextDataBatchImporter window = (TextDataBatchImporter)EditorWindow.GetWindow(typeof(TextDataBatchImporter));
        window.Show();
    }

    private void OnGUI()
    {
        

        EditorGUILayout.HelpBox("将Txt拖到这里", MessageType.Info);

        // 允许用户选中需要转换的文件
        textAsset = (TextAsset)EditorGUILayout.ObjectField("Text File", textAsset, typeof(TextAsset), false);

        if (GUILayout.Button("开始导入") && textAsset != null)
        {
            ImportTextData();
        }
    }

    private void ImportTextData()
    {
        // 将文本文档内容按行分割
        textLines = textAsset.text.Split('\n');

        //在内存中创建一个dialog对象
        Dialog newDialog = ScriptableObject.CreateInstance<Dialog>();

        foreach (string line in textLines)
        {
            int maoHaoPos = 0;

            for (int i = line.Length - 1; i >= 0; i--)
            {
                if (line[i] == '：') maoHaoPos = i;
            }
            string speaker = line.Substring(0, maoHaoPos);
            string latter = line.Substring(maoHaoPos + 1);
            char[] temp = new char[latter.Length];
            int j = 0;
            for (int i = 0; i < latter.Length; i++)
            {
                if (latter[i] != '“' && latter[i] != '”')
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
                Debug.LogError("NPC不存在：" + speaker);
                return;
            }


        }


        // 创建文件
        string path = AssetDatabase.GenerateUniqueAssetPath("Assets/Prefabs/" + textAsset.name + ".asset");
        AssetDatabase.CreateAsset(newDialog, path);
        AssetDatabase.SaveAssets();

        EditorUtility.DisplayDialog("文件导入", "导入成功", "确认");
    }
}
