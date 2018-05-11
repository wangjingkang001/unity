using UnityEngine;
using System.Collections;
using System.IO;
using System;

public static class FileUtils  {

    public static void log(this object obj, params object[] objs) {
        if (null != objs && objs.Length > 0) {
            Debug.Log(string.Format(obj.ToString(), objs));
        }
        else {
            Debug.Log(obj.ToString());
        }
    }

    public static void writeBinary(string path, Action<BinaryWriter> act) {
        using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
        using (BinaryWriter bw = new BinaryWriter(fs)) {
            act(bw);
            bw.Close();
            fs.Close();
        }
    }

    public static bool readBinary(string path, Action<BinaryReader> act) {
        if (!File.Exists(path)) return false;
        using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
        using (BinaryReader br = new BinaryReader(fs)) {
            act(br);
            br.Close();
            fs.Close();
            return true;
        }
    }

}
