using System.Collections.Generic;
using UnityEngine;

public class TreePlacingScript : MonoBehaviour
{
    // Inspector Visible
    [SerializeField] private List<GameObject>   _treeObjectList;

    [Header("Not Greater than 80")]
    [SerializeField] private Vector2            _objectCount;

    // Private members
    private float       _randomXAxis = 0.0f;
    private float       _randomYAxis = 0.0f;
    private float       _randomZAxis = 0.0f;
    private Vector2     _placingAreaBoundationsXAxis;
    private Vector2     _placingAreaBoundationsZAxis;

    private List<GameObject> _treeList;

    void Start()
    {
        _treeList = new List<GameObject>();
        _placingAreaBoundationsXAxis = new Vector2(0, 100);
        _placingAreaBoundationsZAxis = new Vector2(0, 100);
        PlaceTrees(_objectCount);
    }

    public void GetNewMeshGenerationData(int xSize, int zSize, int xManuSize, int zManuSize, int currentXsize, int currentZSize)
    {
        int newXSize = 0;
        int newZSize = 0;

        if (currentXsize != 0)
            newXSize = xManuSize - currentXsize;
        else
            newXSize = currentXsize;

        if (currentZSize != 0)
            newZSize = zManuSize - currentZSize;
        else
            newZSize = currentZSize;

        _placingAreaBoundationsXAxis = new Vector2(newXSize, xManuSize);
        _placingAreaBoundationsZAxis = new Vector2(newZSize, zManuSize);
        PlaceTrees(new Vector2(1, 2));
    }

    private void PlaceTrees(Vector2 objectCount)
    {
        if (_treeObjectList == null && _treeObjectList.Count == 0)
            return;

        float newTreeCount = Random.Range(objectCount.x, objectCount.y);

        for (int i = 0; i < (int)newTreeCount; i++)
        {
            foreach (GameObject treeobject in _treeObjectList)
            {
                _randomXAxis = Random.Range(_placingAreaBoundationsXAxis.x, _placingAreaBoundationsXAxis.y);
                _randomYAxis = 0.0f;
                _randomZAxis = Random.Range(_placingAreaBoundationsZAxis.x, _placingAreaBoundationsZAxis.y);

                Vector3 treePlacingPosition = new Vector3(_randomXAxis, _randomYAxis, _randomZAxis);

                GameObject newTreeClone = Instantiate(treeobject, treePlacingPosition, Quaternion.identity, this.gameObject.transform);
                _treeList.Add(newTreeClone);
            }
        }

        for (int i = 0; i < _treeObjectList.Count; i++)
        {
            Destroy(_treeList[i]);
            _treeList.RemoveAt(i);
        }
    }

}
