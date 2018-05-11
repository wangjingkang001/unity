using UnityEngine;
using System.Collections;


class MapPointItem
{
    public float x;
    public float z;
    public Vector3 mapPoint = new Vector3();

    public override string ToString() {
        return string.Format("x:{0},z:{1},vec:{2}", x, z, mapPoint.ToString());
    }
}