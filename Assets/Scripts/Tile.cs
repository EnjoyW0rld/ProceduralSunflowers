using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Bounds _bounds;
    [SerializeField] protected int _density = 10;
    private Matrix4x4[] _sunflowerPos;
    private InstancePlacer _instancePlacer;
    private void Start()
    {
        SetUpMatrices();
        _instancePlacer = FindObjectOfType<InstancePlacer>();
        _instancePlacer.AddBatch(_sunflowerPos);
    }
    protected virtual void SetUpMatrices()
    {
        float xSpace = _bounds.size.x / _density;
        float zSpace = _bounds.size.z / _density;
        Vector3 startPos = _bounds.min;
        _sunflowerPos = new Matrix4x4[_density * _density];
        for (int x = 0; x < _density; x++)
        {
            for (int z = 0; z < _density; z++)
            {
                _sunflowerPos[x * _density + z] = Matrix4x4.TRS(startPos + new Vector3(x * xSpace, 0, z * zSpace) + transform.position, Quaternion.Euler(-90, 0, 0), Vector3.one);
            }
        }
    }
    private void OnDestroy()
    {
        _instancePlacer.RemoveBatch(_sunflowerPos);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(_bounds.center + transform.position, _bounds.size);
    }
}
