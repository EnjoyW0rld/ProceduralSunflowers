using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathedTile : Tile
{
    //1000 - left only
    //0100 - top only
    //0010 - right only
    //0001 - bottom only
    private BitArray _sides;
    [SerializeField] private bool _left, _top, _right, _bottom;
    [Header("Path prefabs")]
    [SerializeField] private GameObject _oneSided, _LShape, _TShape, _XShape;

    new private void Start()
    {
        print("hello?");
        _sides = new BitArray(4, false);
        _sides[0] = _left;
        _sides[1] = _top;
        _sides[2] = _right;
        _sides[3] = _bottom;
        int sideCount = 0;
        for (int i = 0; i < 4; i++)
        {
            if (_sides[i]) sideCount++;
        }
        switch (sideCount)
        {
            case 0:
                break;
            case 1:
                SpawnOneSide();
                break;
            case 2:
                SpawnLShape();
                break;
            case 3:
                break;
            case 4:
                break;
        }
    }
    private void SpawnOneSide()
    {
        Quaternion targetRot = Quaternion.identity;
        for (int i = 0; i < 4; i++)
        {
            if (_sides[i]) targetRot = Quaternion.Euler(0, 90 * i, 0);
        }
        Instantiate(_oneSided, transform.position, targetRot);
    }
    private void SpawnLShape()
    {
        Quaternion targetRot = Quaternion.identity;
        bool invertedScale = false;
        for (int i = 0; i < 4; i++)
        {
            if (_sides[i])
            {
                targetRot = Quaternion.Euler(0, 90 * i, 0);
                if (i + 1 < 4)
                {
                    invertedScale = _sides[i + 1] == false; //Detects if we need to invert on Z axis to match with the route
                }
                break;
            }
        }
        GameObject instObj = Instantiate(_LShape, transform.position, targetRot);
        instObj.transform.localScale = new Vector3(1, 1, invertedScale ? -1 : 1);
    }
    
}
