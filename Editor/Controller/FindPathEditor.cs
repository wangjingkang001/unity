using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;

/// <summary>
/// </summary>
public class FindPathEditor {


    /// <summary>快捷键 Ctrl+Shift + C(或者鼠标右键) ===>复制选中两个游戏对象之间的查找路径x ：transform.FindChild("路径x")
    /// </summary>
    [MenuItem("GameObject/Create Other/Copy Find Child Path _%#_ C")]
    static void CopyFindChildPath()
    {

        Object[] objAry = Selection.objects;
        //Debug.Log(objAry.Length);

        if (objAry.Length == 2)
        {
            GameObject gmObj0 = (GameObject)objAry[0];
            GameObject gmObj1 = (GameObject)objAry[1];
            List<Transform> listGameParent0 = new List<Transform>(gmObj0.transform.GetComponentsInParent<Transform>(true));
            List<Transform> listGameParent1 = new List<Transform>(gmObj1.transform.GetComponentsInParent<Transform>(true));
            System.Text.StringBuilder strBd = new System.Text.StringBuilder("");
            //gmObj0.transform.FindChild("");
            //string findCode = "gmObj0"
            if (listGameParent0.Contains(gmObj1.transform))
            {
                int startIndex = listGameParent0.IndexOf(gmObj1.transform);
                Debug.Log(startIndex);
                for (int i = startIndex; i >= 0; i--)
                {
                    if (i != startIndex)
                    {
                        strBd.Append(listGameParent0[i].gameObject.name).Append(i != 0 ? "/" : "");
                    }
                    
                }
            }
            
            if (listGameParent1.Contains(gmObj0.transform))
            {
                int startIndex = listGameParent1.IndexOf(gmObj0.transform);
                for (int i = startIndex; i >= 0; i--)
                {
                    if (i != startIndex)
                    {
                        strBd.Append(listGameParent1[i].gameObject.name).Append(i != 0 ? "/" : "");
                    }
                    
                }
            }

            TextEditor textEditor = new TextEditor();
            textEditor.text = "\"" + strBd.ToString() + "\"";// "hello world";
            textEditor.OnFocus();
            textEditor.Copy();
            string colorStr = strBd.Length > 0 ? "<color=green>" : "<color=red>";
            Debug.Log(colorStr + "复制：【\"" + strBd.ToString() + "\"】" + "</color>");
        }


    }
}
