using UnityEngine;
using System.Collections.Generic;
[System.Serializable]
public class HitObject
{
    [SerializeField]
    public List<Vector2[]> path;
    public Vector2[] points;
    public string objName;
    public List<ListOfPoints> go;
}

[System.Serializable]
public class ListOfPoints
{
    public Vector2[] _pointList;
}
