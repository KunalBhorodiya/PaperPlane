using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    // Private Members
    private Mesh        _mesh;
    private Vector3[]   _vertices;
    private int[]       _triangles;
    private int         _lastXSize      = 0;
    private int         _lastZSize      = 0;
    private int         _positionsCounts = 0;
    private List<GameObject> _cloneTreeObjectList;

    // Inspector Visible
    //[SerializeField] TreePooling    _treePooling;

    // Public Members
    public int xSize            = 20;
    public int zSize            = 20;
    public int xManuSize        = 0;
    public int zManuSize        = 0;
    public int manuXVertex      = 0;
    public int manuZVertex      = 0;
    //public int manuXVertexTemp = 0;
    //public int manuZVertexTemp = 0;

    private void Awake()
    {
        _cloneTreeObjectList = new List<GameObject>();
    }

    private void Start()
    {
        _mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = _mesh;

        xManuSize = xSize;
        zManuSize = zSize;

        //manuXVertexTemp = xSize;
        //manuZVertexTemp = zSize;

        _lastXSize = xSize;
        _lastZSize = zSize;

        GenerateMesh();
    }

    private void Update()
    {
        //GenerateMesh();
    }

    void GenerateMesh()
    {
        manuXVertex = xManuSize - _lastXSize;
        manuZVertex = zManuSize - _lastZSize;

        CreateShape();
        UpdateShape();
    }

    void CreateShape()
    {

        // Creating Vertices
        _vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        for (int i = 0, z = manuZVertex; z <= zManuSize; z++)
        {
            for (int x = manuXVertex; x <= xManuSize; x++)
            {
                float height = Mathf.PerlinNoise(x * .3f, z * .3f) * 2f;
                _vertices[i] = new Vector3(x, height, z);
                i++;

                bool canInstantiate = UnityEngine.Random.Range(1, 80) == 3;

                if (canInstantiate)
                {
                    GameObject newTreeToPlace = TreePooling.GetTreeFromPool();
                    if (newTreeToPlace != null)
                    {
                        Vector3 newTreePosition = new Vector3(x, height, z);
                        newTreeToPlace.transform.position = newTreePosition;
                        newTreeToPlace.SetActive(true);
                        _cloneTreeObjectList.Add(newTreeToPlace);
                    }
                }
            }
        }


        // Creating Traingles
        _triangles = new int[xSize * zSize * 6];

        int vert = 0;
        int tris = 0;
        for(int i = 0; i < zSize; i++)
        {
            for (int j = 0; j < xSize; j++)
            {
                _triangles[tris + 0] = vert + 0;
                _triangles[tris + 1] = vert + xSize + 1;
                _triangles[tris + 2] = vert + 1;
                _triangles[tris + 3] = vert + 1;
                _triangles[tris + 4] = vert + xSize + 1;
                _triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }

            vert++;
        }

    }

    private void UpdateShape()
    {
        _mesh.Clear();
        _mesh.vertices = _vertices;
        _mesh.triangles = _triangles;
        _mesh.RecalculateNormals();
    }

    private void HideUnwantedTrees()
    {
        for(int i = 0; i < _cloneTreeObjectList.Count; i++)
        {
            if(_cloneTreeObjectList[i] != null)
            {
                if(_cloneTreeObjectList[i].transform.position.z < _positionsCounts)
                {
                    _cloneTreeObjectList[i].SetActive(false);
                }
            }
        }
        _cloneTreeObjectList.Clear();
    }

    public void GenerateNewMesh(int xAxis, int zAxis)
    {
        xManuSize += xAxis;
        zManuSize += zAxis;
        _positionsCounts += 1;
        GenerateMesh();
        HideUnwantedTrees();
    }

    /*private void OnDrawGizmos()
    {
        if (_vertices == null)
            return;

        for (int i = 0; i < _vertices.Length; i++)
        {
            Gizmos.DrawSphere(_vertices[i], .1f);
        }
    }*/
}
