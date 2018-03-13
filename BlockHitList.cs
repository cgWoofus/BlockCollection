using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using smartReplace;

[System.Serializable]
public class BlockHitList : ScriptableObject 
{
    public HitObject[] listOfHit;


    public BlockHitList(int listSize)
    {
        listOfHit = new HitObject[listSize];
    }

    public void setPath(int index, Vector2[] points)
    {
        try
        {
            listOfHit[index] = new HitObject();
            listOfHit[index].points = new Vector2[points.Length];


            points.CopyTo(listOfHit[index].points, 0);
        }
        catch
        {
            if (points == null)
                Debug.Log("no path found");
            else
                Debug.Log("error copying path");
        }
    }

    public void setPath2(int index,string objName, List<Vector2[]> paths)
    {
        listOfHit[index] = new HitObject();
        listOfHit[index].objName = objName;
        if (paths == null)
            throw new System.Exception("paths is null");

        listOfHit[index].path = new List<Vector2[]>();
        for (int x = 0; x< paths.Count;x++)
        {
            if(paths[x].Length<1)
            {
                Debug.Log("path with zero length copied is this intended?");
            }
            listOfHit[index].path.Add(paths[x]);
        }

    }

    public void setPath3(int index, string objName, List<Vector2[]> paths)
    {
        listOfHit[index] = new HitObject();
        listOfHit[index].objName = objName;
        if (paths == null)
            throw new System.Exception("paths is null");

        listOfHit[index].go = new List<ListOfPoints>();
        for (int x = 0; x < paths.Count; x++)
        {
            if (paths[x].Length < 1)
            {
                Debug.Log("path with zero length copied is this intended?");
                continue;
            }
            listOfHit[index].go.Add(new ListOfPoints());
            listOfHit[index].go[x]._pointList = new Vector2[paths[x].Length];
            paths[x].CopyTo(listOfHit[index].go[x]._pointList, 0); 
        }

    }

    public void clean()
    {
        var cleanList = new List<HitObject>();

        for (int x = 0; x < this.listOfHit.Length; x++)
        {
            if (this.listOfHit[x] != null)
            {
                cleanList.Add(this.listOfHit[x]);
            }
        }
        listOfHit = new HitObject[cleanList.Count];
        for (int z = 0; z < listOfHit.Length; z++)
            listOfHit[z] = cleanList[z];

    }

    public static BlockHitList  createList(List<Vector2[]> listOfObjects,string filename)
    {
        var bhlist = new BlockHitList(listOfObjects.Count);
        try
        {
            for (int x = 0; x < listOfObjects.Count; x++)
            {
                bhlist.setPath(x, listOfObjects[x]);
                
            }
        }
        catch
        {
            Debug.Log("Problem on BlockHitList Class");
        }
        return bhlist;
    }


    public static BlockHitList createList2(List<List<Vector2[]>> listOfObjects,Transform[] _allTransformList, string fileName)
    {
       var bhList = new BlockHitList(listOfObjects.Count);
        for (int x = 0; x < _allTransformList.Length; x++)
        {

            if (_allTransformList[x] == null)
                continue;

            if (listOfObjects[x] == null)
                continue;

            var anObject = new List<Vector2[]>();     
                        
            for (int y = 0; y < listOfObjects[x].Count; y++)
                {
                    if (listOfObjects[x][y] != null && listOfObjects[x][y].Length>0)
                        anObject.Add(listOfObjects[x][y]);
            }

            if(anObject.Count>0)
                {
                  bhList.setPath3(x, _allTransformList[x].name, anObject);
                }
        }

        bhList.clean();
        return bhList;
    }
}
