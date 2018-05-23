using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

/// <summary>
/// 
/// </summary>
public class FileCombineEditor : EditorWindow {

	public static string filesPath = "testFile";
	public static string filesName1 = "files1";             
	public static string filesName2 = "files2";
    public static string outFilePath = "outFiles";
	public static string outFileName = "files";

    public static Object fileObj1 = null;
	public static Object fileObj2 = null;

	public static bool isChangeBaseValue = true;

	private static Dictionary<string, string> filesDic1;

    [MenuItem("KY工具/FileCombineEditor #f")]
    public static void EditorBegin()
    {
        //Editor GUI
        FileCombineEditor.CreateInstance<FileCombineEditor>().Show();
    }

	public void OnGUI()
	{
		GUILayout.Label("###合并flies文件工具###", EditorStyles.boldLabel);
		EditorGUILayout.Space();
		EditorGUILayout.LabelField("---方式1---");

		EditorGUILayout.LabelField("Assets目录下读取目录-string");
		filesPath = EditorGUILayout.TextField(filesPath);

		EditorGUILayout.LabelField("文件files1(父文件)-string");
		filesName1 = EditorGUILayout.TextField(filesName1);

		EditorGUILayout.LabelField("文件files2(子文件)-string");
		filesName2 = EditorGUILayout.TextField(filesName2);

		isChangeBaseValue = EditorGUILayout.Toggle("重复Key子文件替换父文件", isChangeBaseValue);

		EditorGUILayout.LabelField("输出文件目录-string");
		outFilePath = EditorGUILayout.TextField(outFilePath);

		EditorGUILayout.LabelField("输出文件名-string");
		outFileName = EditorGUILayout.TextField(outFileName);

		if (GUILayout.Button("合并导出到"+ outFilePath+"目录"))
        {
			filesDic1 = new Dictionary<string, string>();

			string files1 = AppDataPath +"/"+ filesPath +"/" + filesName1 + ".txt";
			string files2 = AppDataPath +"/"+ filesPath +"/" + filesName2 + ".txt";
			string baseFiles = AppDataPath + "/" + outFilePath;
			string baseFile = AppDataPath + "/" + outFilePath + "/" + outFileName + ".txt";
			if (Directory.Exists(baseFiles))
			{
				Directory.Delete(baseFiles,true);
			}
			Directory.CreateDirectory(baseFiles);
			AssetDatabase.Refresh();

			string[] strsFile1 = ReadAllFile(files1);
			string[] strsFile2 = ReadAllFile(files2);

			for (int i = 0; i < strsFile1.Length; i++)
			{
			string[] sArray = strsFile1[i].Split('|');
			filesDic1.Add(sArray[0],sArray[1]);
			}

			for (int i = 0; i < strsFile2.Length; i++)
			{
			string[] sArray = strsFile2[i].Split('|');
				if (filesDic1.ContainsKey(sArray[0]))
				{
					//相同的key是否处理
					if (isChangeBaseValue)
					{
						//改变原来base文件的value
						filesDic1[sArray[0]] = sArray[1];
					}
					else
					{
						//不改变
					}
				}
				else
				{
					filesDic1.Add(sArray[0],sArray[1]);
				}

			}

			WriteFiles(baseFile,filesDic1);
        }

		//另一种方式
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		EditorGUILayout.LabelField("---方式2(未开发..)---");
        EditorGUILayout.LabelField("导入文件-Object");
		fileObj1 = (Object) EditorGUILayout.ObjectField(fileObj1, typeof(Object),true);
		fileObj2 = (Object) EditorGUILayout.ObjectField(fileObj2, typeof(Object),true);

		if (GUILayout.Button("转换Obj"))
        {
            ReadForObject(fileObj1);
        }
	}


	/// <summary>
    /// 写文件
    /// </summary>
	private static void WriteFiles(string txtfile, Dictionary<string,string> dic)
	{
		//写文件
        if (File.Exists(txtfile)) File.Delete(txtfile);
        FileStream fs = new FileStream(txtfile, FileMode.CreateNew);
        StreamWriter sw = new StreamWriter(fs);
		foreach (string key in dic.Keys)
		{
			sw.WriteLine(key + "|" + dic[key]);
		}
        sw.Close();
        fs.Close();
		AssetDatabase.Refresh();
	}

	/// <summary>
    /// Object转string
    /// </summary>
	static string ReadForObject(Object obj)
	{
		string str = obj.ToString();
		Debug.Log("强制转换Obj:");
		Debug.Log(str);
		return str;
	}

	/// <summary>
    /// 返回整个文本字符串
    /// </summary>
    static string[] ReadAllFile(string FileName)
    {
        string[] strs;
        strs = File.ReadAllLines(FileName);
		return strs;
    }

    /// <summary>
    /// 数据目录
    /// </summary>
    static string AppDataPath {
        get { return Application.dataPath.ToLower(); }
    }

	void OnInspectorUpdate() //更新  
    {  
        Repaint();  //重新绘制  
    }

}
