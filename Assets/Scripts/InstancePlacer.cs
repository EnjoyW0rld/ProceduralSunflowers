using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstancePlacer : MonoBehaviour
{
    [SerializeField] private Mesh _targetMesh;
    [SerializeField] private Material _targetMaterial;
    [SerializeField] private int _xExt,_zExt;
    private Matrix4x4[] _matrices;
    private List<Matrix4x4[]> _complMatrices;
    void Start()
    {
        _matrices = new Matrix4x4[100];
        List<Matrix4x4> matrices = new List<Matrix4x4>();
        _complMatrices = new List<Matrix4x4[]>();
        
        for (int x = 0; x < _xExt; x++)
        {
            for (int z = 0; z < _zExt; z++)
            {
                if(matrices.Count > 100)
                {
                    _complMatrices.Add(matrices.ToArray());
                    matrices = new List<Matrix4x4>();
                }
                int y = Random.Range(-40, 40);
                matrices.Add(Matrix4x4.TRS(new Vector3(x, 0, z), Quaternion.Euler(-90, 0, y), Vector3.one));
                //_matrices[x * 10 + z] = Matrix4x4.TRS(new Vector3(x, 0, z), Quaternion.Euler(-90,0,y), Vector3.one);
            }
        }
        _matrices = matrices.ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < _complMatrices.Count; i++)
        {
            Graphics.DrawMeshInstanced(_targetMesh, 0, _targetMaterial, _complMatrices[i]);
        }
        //Graphics.DrawMeshInstanced(_targetMesh,0,_targetMaterial,_matrices);
    }
}
