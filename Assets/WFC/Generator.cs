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
                //SpawnAtPos(x, z);
                TileData objToSpawn = GetTileToSpawn(x, z, _createdTiles, new List<TileData>(_tilePresets.GetTileDataCopy()));
                TileData spawnedObject = Instantiate(objToSpawn, new Vector3(x * 2, 0, z * 2), Quaternion.identity);
                _createdTiles[x, z] = spawnedObject;

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
            //SpawnAtPos(tempX, tempZ);
            tempZ++;
            if (tempZ == _gridHeight)
            {
                tempX++;
                tempZ = 0;
            }
        }

    }
    public static TileData GetTileToSpawn(int pX, int pZ, TileData[,] pCreatedTiles, List<TileData> pCurrentPresets)
    {
        //List<TileData> currentPresets = new List<TileData>(_tilePresets.GetTileDataCopy());
        DoChecks(pX, pZ, pCreatedTiles, pCurrentPresets);
        return pCurrentPresets[Random.Range(0, pCurrentPresets.Count)];
    }
    private static void DoChecks(int pX, int pZ, TileData[,] pCreatedTiles, List<TileData> pCurrentPresets)
    {
        if (pZ - 1 >= 0 && pCreatedTiles[pX, pZ - 1] != null)
        {
            //Check if bottom gate of the tile on top of the current is active
            bool topGateToDelete = !pCreatedTiles[pX, pZ - 1].GetGate(TileData.GatePosition.Bottom);
            print(topGateToDelete + " top gate to delete");
            for (int i = pCurrentPresets.Count - 1; i >= 0; i--)
            {
                if (pCurrentPresets[i].GetGate(TileData.GatePosition.Top) == topGateToDelete)
                {
                    pCurrentPresets.RemoveAt(i);
                }
            }
        }

        if (pX - 1 >= 0 && pCreatedTiles[pX - 1, pZ] != null)
        {
            bool rightGateToDelete = !pCreatedTiles[pX - 1, pZ].GetGate(TileData.GatePosition.Left);
            for (int i = pCurrentPresets.Count - 1; i >= 0; i--)
            {
                if (pCurrentPresets[i].GetGate(TileData.GatePosition.Right) == rightGateToDelete)
                {
                    pCurrentPresets.RemoveAt(i);
                }
            }
        }
    }
}
