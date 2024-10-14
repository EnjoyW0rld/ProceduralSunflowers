using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Collections;
using UnityEngine;

public class TileData : MonoBehaviour
{
    //Gates are allinged this way - 
    //indx 0 = top
    //indx 1 = right
    //indx 2 = bottom
    //indx 3 = left
    [SerializeField] private Gate[] _gates;
    [SerializeField] private TilePresets _allTiles;
    [Header("Gate values")]
    [SerializeField] private bool _topGate;
    [SerializeField] private bool _rightGate;
    [SerializeField] private bool _bottomGate;
    [SerializeField] private bool _leftGate;
    [SerializeField, HideInInspector] private bool[] _gateValues;

    public enum GatePosition { Top = 0, Right = 1, Bottom = 2, Left = 3 }

    public bool[] GateValue { get { return _gateValues; } }
    /// <summary>
    /// 0 = top, 1 = right, 2 = bottom, 3 = left
    /// </summary>
    /// <param name="pIndex"></param>
    /// <returns></returns>
    public bool GetGate(int pIndex)
    {
        return _gateValues[pIndex];
    }
    public bool GetGate(GatePosition pIndex) => GetGate((int)pIndex);


    private void OnValidate()
    {
        if (_gateValues == null)
        {
            _gateValues = new bool[4];
        }
        _gateValues[0] = _topGate;
        _gateValues[1] = _rightGate;
        _gateValues[2] = _bottomGate;
        _gateValues[3] = _leftGate;
    }

}
[System.Serializable]
struct Gate
{
    public TileData[] PossibleTiles;
}
