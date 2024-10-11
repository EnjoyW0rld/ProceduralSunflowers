using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PresetData")]
public class TilePresets : ScriptableObject
{
    public TileData[] tileData;
    public TileData[] GetTileDataCopy() => tileData.Clone() as TileData[];
}
