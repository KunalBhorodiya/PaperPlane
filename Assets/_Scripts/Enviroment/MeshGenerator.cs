using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    // Private Members
    private Mesh _mesh;
    private Vector3[] _vertices;
    private int[] _triangles;
    private int _lastXSize = 0;
    private int _lastZSize = 0;
    private bool _canInstantiateNewTrees = false;

    // Inspector Visible
    [SerializeField] private TreePlacingScript _treePlacingScript;

    // Public Members
    public int xSize = 20;
    public int zSize = 20;
    public int xManuSize = 0;
    public int zManuSize = 0;
    public int manuXVertex = 0;
    public int manuZVertex = 0;

    private void Start()
    {
        _mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = _mesh;

        xManuSize = xSize;
        zManuSize = zSize;

        _lastXSize = xSize;
        _lastZSize = zSize;
        _treePlacingScript.GetNewMeshGenerationData(xSize, zSize, xManuSize, zManuSize, manuXVertex, manuZVertex);
        //GenerateMesh();
    }

    private void Update()
    {
        GenerateMesh();
    }

    void GenerateMesh()
    {

        //  For Mesh Move Settings to X Axis
        if (_lastXSize > xManuSize || _lastXSize < xManuSize)
        {
            manuXVertex = xManuSize - _lastXSize;
            _lastXSize = xSize;
            //_treePlacingScript.GetNewMeshGenerationData(xManuSize, zManuSize, manuXVertex, manuZVertex);
        }
        else if (_lastXSize == xManuSize)
        {
            manuXVertex = 0;
        }
        //  For Mesh Move Settings to Z Axis
        if (_lastZSize > zManuSize || _lastZSize < zManuSize)
        {
            manuZVertex = zManuSize - _lastZSize;
            _lastZSize = zSize;
            //_treePlacingScript.GetNewMeshGenerationData(xManuSize, zManuSize, manuXVertex, manuZVertex);
        }
        else if (_lastZSize == zManuSize)
        {
            manuZVertex = 0;
        }
        
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
                _vertices[i] = new Vector3(x, 0, z);
                i++;
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

    /*private void OnDrawGizmos()
    {
        if (vertices == null)
            return;

        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], .1f);
        }
    }*/
}
