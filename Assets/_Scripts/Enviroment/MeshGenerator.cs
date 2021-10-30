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
   // private int         _positionsCounts = 0;
    private List<GameObject> _cloneTreeObjectList;

    // Public Members
    public int xSize            = 20;
    public int zSize            = 20;
    public int xManuSize        = 0;
    public int zManuSize        = 0;
    public int manuXVertex      = 0;
    public int manuZVertex      = 0;
    public int newZSize         = 0;


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

        _lastXSize = xSize;
        _lastZSize = zSize;

        newZSize = zManuSize;
    }

    private void Update()
    {
        //GenerateMesh();
    }

    public void GenerateMesh(bool canInstantiateNewTrees, bool isFirstTimeOfInstantiate)
    {
        manuXVertex = xManuSize - _lastXSize;
        manuZVertex = zManuSize - _lastZSize;

        CreateShape(canInstantiateNewTrees, isFirstTimeOfInstantiate);
        UpdateShape();
    }

    // Create MeshShape using Trangles with code
    void CreateShape(bool canInstantiateNewTrees, bool isFirstTimeOfInstantiate)
    {
        int maxTreeInstantiate = 0;

        // Creating Vertices
        _vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        for (int i = 0, z = manuZVertex; z <= zManuSize; z++)
        {
            for (int x = manuXVertex; x <= xManuSize; x++)
            {
                float height = Mathf.PerlinNoise(x * .3f, z * .3f) * 2f;
                _vertices[i] = new Vector3(x, height, z);
                i++;

                // New Trees Generation
                bool canInstantiate = UnityEngine.Random.Range(1, 100) == 3;

                if (canInstantiateNewTrees)
                    if (canInstantiate)
                    {
                        if (isFirstTimeOfInstantiate)
                        {
                            GenerateNewTrees(x, height, z);
                        }
                        else
                        {
                            if (z <= newZSize && maxTreeInstantiate <= 10)
                            {
                                GenerateNewTrees(x, height, UnityEngine.Random.Range(newZSize, newZSize + 10));
                                maxTreeInstantiate++;
                            }
                            //else
                            //{
                              //  newZSize += 10;
                            //}
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

    // Generating New Trees
    void GenerateNewTrees(float x, float height, float z)
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

    // Replace Old Shape and Create new Shape
    private void UpdateShape()
    {
        _mesh.Clear();
        _mesh.vertices = _vertices;
        _mesh.triangles = _triangles;
        _mesh.RecalculateNormals();
    }

    // Hide UnWanted Trees to their default position
    private void HideUnwantedTrees()
    {
        List<GameObject> tempList = new List<GameObject>();

        if (_cloneTreeObjectList != null)
        {
            for (int i = 0; i < _cloneTreeObjectList.Count; i++)
            {
                if (_cloneTreeObjectList[i] != null)
                {
                    if (_cloneTreeObjectList[i].transform.position.z < zManuSize - zSize)
                    {
                        tempList.Add(_cloneTreeObjectList[i]);
                    }
                }
            }
        }
            
        if(tempList.Count > 0)
        {
            for (int i = 0; i < tempList.Count; i++)
            {
                tempList[i].SetActive(false);
                _cloneTreeObjectList.Remove(tempList[i]);
            }
            tempList.Clear();
        }

        //Debug.Log(_cloneTreeObjectList.Count);
    }

    // Creating new Mesh As Per Plane Movement
    public void GenerateNewMesh(int xAxis, int zAxis, int zPositionCount)
    {
        xManuSize += xAxis;
        zManuSize += zAxis;
        //_positionsCounts += 1;

        //if (zPositionCount - _lastZPositionsCounts == 10
        //  
        //{
            bool canInstantiateNewTrees = zPositionCount % 10 == 0;

            if (canInstantiateNewTrees)
            {
                GenerateMesh(canInstantiateNewTrees, /*Is First Time To instantiate*/ false);
                HideUnwantedTrees();
                newZSize = zManuSize;
            }
       // }
        
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
