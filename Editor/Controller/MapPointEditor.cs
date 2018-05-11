using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;
using System;
using System.IO;
using System.Linq; 

public class MapPointEditor : EditorWindow {

    public static GameObject terrain = null;			//检测GameObj
    public static GameObject terrain_obj = null;        //物体模块
    public static GameObject terrain_collider = null;   //collider
    public static bool isShowLine;                      //显示射线
    public static float showlineTime = 60;              //60秒

    public static float intervalX = 1.0f;		        //间隔X
    public static float intervalZ = 1.0f;		        //间隔Z
    public static float maxDistance = 1000;	            //射线最大长度
    public static float floatPointHeight = 80;         //射线点高度
	public static string filePath = "1000";             //导出导入文件名
	public static string layers = "Map";              //Layers层级

	private static int maplayerNumber;                  //层级数字
    private static Vector3 _watchPoint;                 //检测点

    private static float numberP;                       //总点数
    private static List<Transform> list;                //terrain的子类transform

    private static float checkX_Min = 0f;
    private static float checkZ_Min = 0f;
    private static float checkX_Max = 1000f;
    private static float checkZ_Max = 1000f;

    private static MapInfo mapInfoRead = null;

	[MenuItem("KY工具/MapPointEditor #m")]
    public static void PointEditorBegin()
    {
		//Editor GUI
		MapPointEditor.CreateInstance<MapPointEditor>().Show();
    }


	public void OnGUI()
	{
        GUILayout.Label("###导出导入地图数据工具###", EditorStyles.boldLabel);
		EditorGUILayout.Space();

        EditorGUILayout.LabelField("场景-GameObject");
		terrain = (GameObject) EditorGUILayout.ObjectField(terrain, typeof(GameObject),true);

        isShowLine = EditorGUILayout.Toggle("显示射线", isShowLine);
        if (isShowLine)
        {
            EditorGUILayout.LabelField("射线显示时间(秒)");
            showlineTime = EditorGUILayout.FloatField(showlineTime);
            // EditorGUILayout.Space();
            GUILayout.Label("--------------------------------------------------------------------------------------");
        }

        if (terrain)
        {
            list = GetChildCollection(terrain.transform);
        }

        if (list !=null)
        {
            String nameAdd = "";
            for (int i = 0; i < list.Count; i++)
            {
                if (i< list.Count-1)
                {
                    nameAdd += list[i].gameObject.name + ",";
                }
                else
                {
                    nameAdd += list[i].gameObject.name;
                }
                
            }
            EditorGUILayout.LabelField("导入场景中有如下子类:");
            EditorGUILayout.LabelField(nameAdd);

            EditorGUILayout.LabelField("请选择:物体模块");
            terrain_obj = (GameObject) EditorGUILayout.ObjectField(terrain_obj,typeof(GameObject),true);
            GUILayout.Label("--------------------------------------------------------------------------------------");
            // EditorGUILayout.LabelField("请选择:collider");
            // terrain_collider = (GameObject) EditorGUILayout.ObjectField(terrain_collider,typeof(GameObject),true);
        }

		EditorGUILayout.LabelField("检测间隔X-float");
		intervalX = EditorGUILayout.FloatField(intervalX);
		
		EditorGUILayout.LabelField("检测间隔Z-float");
		intervalZ = EditorGUILayout.FloatField(intervalZ);
		
		EditorGUILayout.LabelField("检测射线长度-float");
		maxDistance = EditorGUILayout.FloatField(maxDistance);
		
		EditorGUILayout.LabelField("检测射线高度-float");
		floatPointHeight = EditorGUILayout.FloatField(floatPointHeight);
		
        EditorGUILayout.LabelField("检测层级-string");
		layers = EditorGUILayout.TextField(layers);
        
        

		EditorGUILayout.LabelField("导出导入文件名-string");
		filePath = EditorGUILayout.TextField(filePath);

		if (GUILayout.Button("写入地图数据"))
        {
            // Debug.Log("terrain.transform.position:"+terrain.transform.position.ToString());
            Debug.Log("#开始写入地图数据");
            getsceneSize();
			writeMapData();
        }
        
        

		if (GUILayout.Button("加载地图数据"))
        {
            Debug.Log("#开始加载地图数据");
			readMapData();
        }

		if (GUILayout.Button("测试Collider"))
        {
            Debug.Log("#开始测试Collider");
			var col = getCollider(terrain);
            Debug.Log(col.bounds.size.ToString());
        }
	}

    /// <summary>  
    /// 获取子对象变换集合  
    /// </summary>  
    /// <param name="obj"></param>  
    /// <returns></returns>  
    public static List<Transform> GetChildCollection(Transform obj)  
    {  
        List<Transform> list = new List<Transform>();  
        for (int i = 0; i < obj.childCount; i++)  
        {  
            list.Add(obj.GetChild(i)); 
        }  
        return list;  
    }

    private void getsceneSize()
    {
        List<Transform> objList = GetChildCollection(terrain_obj.transform);
        Debug.Log("计算实体场景区域");
        List<float> objx = new List<float>();
        List<float> objz = new List<float>();

        for(int i = 0;i < objList.Count;i++)
        {
            objx.Add(objList[i].position.x);
            // Debug.Log(objList[i].gameObject.name);
            // Debug.Log(objList[i].position);
            objz.Add(objList[i].position.z);

        }

        checkX_Max = objx.Max();
        checkX_Min = objx.Min();
        checkZ_Max = objz.Max();
        checkZ_Min = objz.Min();

        Debug.Log("x:");
        Debug.Log(objx.Max());
        Debug.Log(objx.Min());
        
        

        Debug.Log("z:");
        Debug.Log(objz.Max());
        Debug.Log(objz.Min());
    }

	private void writeMapData() 
    {
        //  var collider = terrain.GetComponent<Collider>();
        //  1.这里要计算出一个区域
        //  var maxX = collider.bounds.size.x;
        //  var maxY = collider.bounds.size.y;
        //  var maxZ = collider.bounds.size.z;
        var maxX = checkX_Max;
        var maxZ = checkZ_Max;

        _watchPoint = new Vector3();
        _watchPoint.y = floatPointHeight;
        //numberP = maxX * maxZ;
        // Debug.Log(collider.bounds.size.ToString());
        List<MapPointItem> mapPoints = new List<MapPointItem>();
        float i = checkX_Min;
        var total = 0;
        // var numberPoint = maxX * maxZ;
        while (i <= maxX) {
            float j = checkZ_Min;
            while (j <= maxZ) {
                var mp = new MapPointItem();
                mp.x = i;
                mp.z = j;
                mp.mapPoint = getMapItemPoint(i, j);
                mapPoints.Add(mp);
                j = Convert.ToSingle(Math.Round(j + intervalZ, 1));
                total++;
                //进度条
                var valF = i/maxX + 0.1f;
                if(valF < 1){
                    EditorUtility.DisplayProgressBar("场景碰撞检测中", "正在拼命发射线...",valF) ;
                }else{
                    EditorUtility.ClearProgressBar();
                }
                
            }
            i = Convert.ToSingle(Math.Round(i + intervalX, 1));
        } 

        MapInfo mapInfo = new MapInfo();
        mapInfo.mapName = terrain.name;
        mapInfo.path = filePath;
        mapInfo.width = maxX;
        mapInfo.height = maxZ;
        mapInfo.intervalX = intervalX;
        mapInfo.intervalZ = intervalZ;
        mapInfo.gridSource = mapPoints;
        mapInfo.writeTo(Path.Combine(Application.streamingAssetsPath, string.Format("{0}.map", mapInfo.path)));
    }

	private Vector3 getMapItemPoint(float i, float j) 
    {
        _watchPoint.x = i;
        _watchPoint.z = j;
        var ray = new Ray(_watchPoint, Vector3.down);
		maplayerNumber = LayerMask.GetMask(layers);
        RaycastHit rayCast;//碰撞点
		if(Physics.Raycast(ray, out rayCast, maxDistance, maplayerNumber)){
			//Debug.Log (rayCast.collider.gameObject.tag);
            // Debug.Log (rayCast.point);
            //Debug.Log(rayCast.transform.gameObject.tag);
            // Debug.Log(rayCast.transform.gameObject.layer);
            Debug.Log(rayCast.transform.gameObject.name);
            //Debug.Log(rayCast.collider.bounds.size.ToString());
            //Debug.Log(rayCast.collider.gameObject.GetComponent<Renderer>().bounds.size);
            if (isShowLine)
            {
                Debug.DrawLine(_watchPoint, rayCast.point, Color.yellow,showlineTime);
            }
            return rayCast.point;
        }else{
			Debug.Log ("没有碰撞");
            Debug.Log (_watchPoint);
            return _watchPoint;
		}
        
        
    }

     void OnDrawGizmos(){
      if(mapInfoRead != null)
      {
        Debug.Log("OnDrawGizmos");
        List<MapPointItem> mapPoints = new List<MapPointItem>();
        mapPoints = mapInfoRead.gridSource;
        for (int i = 0; i < mapPoints.Count; i++)
        {
            var mp = new MapPointItem();
            mp = mapPoints[i];
            Gizmos.color = Color.yellow;            //在变换位置处绘制一个黄色的球体
            Gizmos.DrawSphere(mp.mapPoint,1);
        }
        
      }
      
    }

	private void readMapData() 
    {
        var path = filePath;
        var mapPath = Path.Combine(Application.streamingAssetsPath, string.Format("{0}.map", path));
        var mapInfo = new MapInfo();
        mapInfoRead = mapInfo;

        mapInfo.readFrom(mapPath);
        Debug.Log(mapInfo.ToString());
    }

    Collider getCollider (GameObject go) 
    {
        var collider = go.GetComponent<Collider>();
        var colliders = go.GetComponentsInChildren<Collider>();
        var middle = new Vector3();
        foreach(Collider c in colliders) {
            Debug.Log(c.gameObject.name + ":" + c.bounds.size.ToString());
            middle += c.bounds.center;
        }
        middle = middle / colliders.Length;
        return collider;
    }

    void OnInspectorUpdate() //更新  
    {  
        Repaint();  //重新绘制  
    }

}
