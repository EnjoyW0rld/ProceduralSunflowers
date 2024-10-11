using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class Generator : MonoBehaviour
{
    [SerializeField] private TilePresets _tilePresets;
    [SerializeField] private int _gridWidth, _gridHeight;
    private TileData[,] _createdTiles;
    private void Start()
    {
        _createdTiles = new TileData[_gridWidth, _gridHeight];
        for (int x = 0; x < _gridWidth; x++)
        {
            for (int z = 0; z < _gridHeight; z++)
            {
                SpawnAtPos(x, z);
            }
        }
    }
    bool isInput;
    int tempX = 0, tempZ = 0;
    private void Update()
    {
        return;
        if (Input.GetKeyDown(KeyCode.V))
        {
            SpawnAtPos(tempX, tempZ);
            tempZ++;
            if(tempZ == _gridHeight)
            {
                tempX++;
                tempZ = 0;
            }
        }

    }
    private void SpawnAtPos(Vector3 pos)
    {
        List<TileData> currentPresets = new List<TileData>(_tilePresets.GetTileDataCopy());


        Instantiate(currentPresets[Random.Range(0, currentPresets.Count)], pos, Quaternion.identity);
    }
    private void SpawnAtPos(int pX, int pZ)
    {
        List<TileData> currentPresets = new List<TileData>(_tilePresets.GetTileDataCopy());
        
        DoChecks(pX, pZ, currentPresets);
        print(currentPresets.Count);
        _createdTiles[pX, pZ] = Instantiate(currentPresets[Random.Range(0, currentPresets.Count)], new Vector3(pX * 2, 0, pZ * 2), Quaternion.identity);
    }
    private void DoChecks(int pX, int pZ, List<TileData> pCurrentPresets)
    {

        //Here is happening check if it is needed to delete the tile accoriding to the top/bottom gate
        if (pZ - 1 >= 0 && _createdTiles[pX, pZ - 1] != null)
        {
            //print(_createdTiles[pX, pZ - 1].GetGate(2));
            bool topGateToDelete = !_createdTiles[pX, pZ - 1].GetGate(2);
            print(topGateToDelete + " top gate to delete");
            for (int i = pCurrentPresets.Count - 1; i >= 0; i--)
            {
                if (pCurrentPresets[i].GetGate(0) == topGateToDelete)
                {
                    pCurrentPresets.RemoveAt(i);
                }
            }
        }
        //Here is happening check if it is needed to delete the tile accoriding to the right/left gate

        if (pX - 1 >= 0 && _createdTiles[pX - 1, pZ] != null)
        {
            bool rightGateToDelete = !_createdTiles[pX - 1, pZ].GetGate(3);
            for (int i = pCurrentPresets.Count - 1; i >= 0; i--)
            {
                if (pCurrentPresets[i].GetGate(1) == rightGateToDelete)
                {
                    pCurrentPresets.RemoveAt(i);
                }
            }
        }
    }
}
