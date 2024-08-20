using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid : MonoBehaviour
{
    [SerializeField] private GameObject _tilePrefab;
    [SerializeField] private GameObject[,] _placedGrids;
    [SerializeField] private int _extents = 3;
    private enum ExtentionType { Substract, None, Add };
    void Start()
    {
        _placedGrids = new GameObject[_extents, _extents];
        Vector3 center = Vector3.zero;
        for (int x = 0; x < _extents; x++)
        {
            for (int z = 0; z < _extents; z++)
            {
                GameObject obj = Instantiate(_tilePrefab, new Vector3((x - 1) * 10, 0, (z - 1) * 10) + center, Quaternion.identity);
                obj.name = x + " " + z;
                _placedGrids[x, z] = obj;
            }
        }
    }
    /// <summary>
    /// Shifts all the object positions in 3x3 array by one position on Z axis
    /// Discrads objects that happened to be outside the array
    /// </summary>
    /// <param name="pTargetArray"></param>
    /// <param name="pIsForward"></param>
    private void SlideOnZ(GameObject[,] pTargetArray, bool pIsForward = true)
    {
        for (int z = 0; z < _extents; z++)
        {
            for (int x = 0; x < _extents; x++)
            {
                if (pIsForward ? (z < _extents - 1) : (z > 0))
                    pTargetArray[x, z] = _placedGrids[x, (pIsForward ? z + 1 : z - 1)];
                if (z == (pIsForward ? 0 : _extents - 1))
                    Destroy(_placedGrids[x, z]);
            }
        }
    }
    /// <summary>
    /// Shifts all the object positions in 3x3 array by one position on X axis
    /// Discrads objects that happened to be outside the array
    /// </summary>
    /// <param name="pTargetArray"></param>
    /// <param name="pIsToRight"></param>
    private void SlideOnX(GameObject[,] pTargetArray, bool pIsToRight = true)
    {
        for (int x = 0; x < _extents; x++)
        {
            for (int z = 0; z < _extents; z++)
            {
                //Shifts instantiated tile to the adjacent left or right position of the new array
                //if is to right then "x" position of the new array will be the "x + 1" of the old one
                //Same situation when moving to the left but we take "x - 1" position for "x" position of new array 
                if (pIsToRight ? (x < _extents - 1) : (x > 0))
                {
                    pTargetArray[x, z] = _placedGrids[(pIsToRight ? x + 1 : x - 1), z];
                }
                if (x == (pIsToRight ? 0 : _extents - 1)) //Determines which row needs to be deleted 
                {
                    Destroy(_placedGrids[x, z]);
                }
            }
        }
    }
    private void Update()
    {
        //return;
        bool input = false;
        GameObject[,] newObj = new GameObject[_extents, _extents];
        if (Input.GetKeyDown(KeyCode.D))
        {
            SlideOnX(newObj);
            input = true;

        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            SlideOnX(newObj, false);
            input = true;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            SlideOnZ(newObj);
            input = true;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            SlideOnZ(newObj,false);
            input = true;
        }

        if (input)
        {
            //SlideToLeft(newObj);
            Vector3 center = newObj[1, 1].transform.position;
            for (int x = 0; x < _extents; x++)
            {
                for (int z = 0; z < _extents; z++)
                {
                    if (newObj[x, z] == null)
                    {

                        GameObject obj = Instantiate(_tilePrefab, new Vector3((x - 1) * 10, 0, (z - 1) * 10) + center, Quaternion.Euler(UnityEngine.Random.Range(-90, 90), 0, 0));
                        newObj[x, z] = obj;
                    }
                }
            }
            _placedGrids = newObj;
        }
    }
}
