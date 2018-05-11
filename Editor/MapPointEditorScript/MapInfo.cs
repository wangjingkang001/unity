using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class MapInfo
{
    public string mapName;
    public string path;
    public float width;
    public float height;
    public float intervalX;
    public float intervalZ;
    public List<MapPointItem> gridSource;

    public override string ToString() {
        var gcx = width / intervalX;
        var gcy = height / intervalZ;
//		foreach (var item in gridSource) {
//			Debug.Log (item);
//		}

        return string.Format("name:{0}, path:{1}, width:{2}, height:{3}, gridCountX:{4}, gridCountY:{5}, gridTotalCount:{6}", mapName, path, width, height, gcx, gcy, gridSource.Count);
    }

    public void writeTo(string filepath) {
        FileUtils.writeBinary(filepath, bw => {
            bw.Write(mapName);
            bw.Write(path);
            bw.Write(width);
            bw.Write(height);
            bw.Write(intervalX);
            bw.Write(intervalZ);
            bw.Write((int)gridSource.Count);
            foreach (var item in gridSource) {
                bw.Write(item.x);
                bw.Write(item.z);
                bw.Write(item.mapPoint.x);
                bw.Write(item.mapPoint.y);
                bw.Write(item.mapPoint.z);
            }
        });
        Debug.Log("write map to " + path);
    }

    public bool readFrom(string filepath) {
        return FileUtils.readBinary(filepath, bw => {
            mapName = bw.ReadString();
            path = bw.ReadString();
            width = bw.ReadSingle();
            height = bw.ReadSingle();
            intervalX = bw.ReadSingle();
            intervalZ = bw.ReadSingle();
            var count = bw.ReadInt32();
            gridSource = new List<MapPointItem>();
            for (int i = 0; i < count; i++) {
                var item = new MapPointItem();
                item.x = bw.ReadSingle();
                item.z = bw.ReadSingle();
                item.mapPoint = new Vector3();
                item.mapPoint.x = bw.ReadSingle();
                item.mapPoint.y = bw.ReadSingle();
                item.mapPoint.z = bw.ReadSingle();
                gridSource.Add(item);
            }
        });
    }
}