
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Collections.Generic;
using smartReplace;

namespace smartReplace
{

    public enum BGTYPE { BG0, BG1, BG2 };
    public enum OblockSize { Small, Medium, Large }
    public enum LockedSides
    {
        UL = (1 << 7),
        UR = (1 << 6),
        DL = (1 << 5),
        DR = (1 << 4),
        U = (1 << 3),
        D = (1 << 2),
        L = (1 << 1),
        R = (1 << 0)
    }

    [System.Flags]
    public enum OLayer
    {
        Layer0 = (1 << 0),
        Layer1 = (1 << 1),
        Layer2 = (1 << 2),
        Spike = (1 << 3),
    }
    public enum OblockType { A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z, AA, BB, CC, DD, EE, FF, GG, HH, II, JJ, KK, LL, MM, NN, OO, PP, QQ, RR, SS, TT, UU };


}



[System.Serializable]
public class BlockCollection : ScriptableObject
{
    public GameObject _initBlock;
    public GameObject[] _blockTypes;
    public bool _adaptive = false;
    public BGTYPE _bgType;
    //public OblockBehaviour _oBehavior;
    
    
    public OblockSize _collectionSize;
    [SerializeField][EnumFlag]
    public OLayer _oLayerFlag;

    [SerializeField][EnumFlag]
    public LockedSides _sideLockFlag;

    public void SetBlocks(GameObject[] _blockList,string prefabPath)
    {
        _blockTypes = new GameObject[47];
        var _path = prefabPath;// PlayerConstants.GlobalConstants()._prefabObjectPath;
        var _blockP = _blockList.Length - 1;
        for (int x = 0; x < _blockList.Length; x++)
        {

            var _prefab = AssetDatabase.LoadAssetAtPath(string.Format(_path, _blockList[x].name), typeof(GameObject));

            int num;

            var suffnum = int.TryParse(_prefab.name.Split('_')[3].Substring(3), out num);

          //  Debug.Log(num);

            _blockTypes[num] = (GameObject)_prefab;

            if (x == _blockP)
               SetInitBlock((GameObject)_prefab);
        };


    }


    public void SetInitBlock(GameObject block)
    {
        _initBlock = block;
    }

    public GameObject GetInitBlock
    {
        get {
            if (_initBlock != null)
                return _initBlock;
            else
              return  GetFromMesh();
            }

    }

    GameObject GetFromMesh()
    {
        throw new NotImplementedException();
    }
    public void SetBlockTypes(params GameObject[] blocks)
    {

        _blockTypes = new GameObject[blocks.Length];
        for(int x=0;x<blocks.Length;x++)
        {
            _blockTypes[x] = blocks[x];
        }
    }



    public float GetRayDistanceCast
    {
        get
        {
            switch(_collectionSize)
            {
                case OblockSize.Small:
                    return 2f;
                case OblockSize.Medium:
                    return 2.3f;
                case OblockSize.Large:
                    return 4.1f;
                default:
                    return 0f;
            }

        }

    }
    
    public bool CompareBlock<T>(GameObject obj, T comparer)where T : IEqualityComparer<GameObject>
    {
        //var comparer = new BlockComparer();
        bool val =  _blockTypes.Contains(obj, comparer);
        return val;
    }




}

