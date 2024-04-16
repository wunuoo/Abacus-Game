using UnityEngine;
using UnityEditor;
using System.IO;

public class TextDataBatchImporter : EditorWindow
{
    private TextAsset textAsset;
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

            string[] group = line.Split('：');
            group[1].Trim('\r');
            group[1] = group[1].Substring(2, group[1].Length - 4);//去除前后引号
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
                Debug.LogError("NPC不存在：" + group[0]);
                return;
            }


        }


        // 创建文件
        string path = AssetDatabase.GenerateUniqueAssetPath("Assets/Prefabs/新对话.asset");
        AssetDatabase.CreateAsset(newDialog, path);
        AssetDatabase.SaveAssets();

        EditorUtility.DisplayDialog("文件导入", "导入成功", "确认");
    }
}
