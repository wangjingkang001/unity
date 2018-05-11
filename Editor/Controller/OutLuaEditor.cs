using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class OutLuaEditor : EditorWindow {

    public static bool isSingleFile;
    public static bool manyFile;
    public static bool isSingleFiles;
    public static bool manyFiles;
    public static bool isAllFiles;

    [MenuItem("KY工具/OutLuaEditor #o")]
    public static void EditorBegin()
    {
        //Editor GUI
        OutLuaEditor.CreateInstance<OutLuaEditor>().Show();
    }

	public void OnGUI()
	{
		GUILayout.Label("###导出Lua工具###", EditorStyles.boldLabel);
		EditorGUILayout.Space();

        isSingleFile = EditorGUILayout.Toggle("单个文件导出", isSingleFile);
        if (isSingleFile)
        {
            //这里开始单个文件的UI逻辑
        }

        manyFile = EditorGUILayout.Toggle("多个文件导出", manyFile);
        if (manyFile)
        {
            //这里开始单个文件的UI逻辑
        }

        isSingleFiles = EditorGUILayout.Toggle("单个文件夹导出", isSingleFiles);
        if (isSingleFiles)
        {
            //这里开始单个文件jia的UI逻辑
        }

        manyFiles = EditorGUILayout.Toggle("多个文件夹导出", manyFiles);
        if (manyFiles)
        {
            //这里开始单个文件jia的UI逻辑
        }

        isAllFiles = EditorGUILayout.Toggle("全部文件导出", isAllFiles);
        if (isAllFiles)
        {
            //这里开始单个文件jia的UI逻辑
        }

	}

	void OnInspectorUpdate() //更新  
    {  
        Repaint();  //重新绘制  
    }

}
