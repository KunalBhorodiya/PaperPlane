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

    void Start()
    {
        
    }


    public void GetNewMeshGenerationData(int xSize, int zSize, int xManuSize, int zManuSize, int currentXsize, int currentZSize)
    {
        int newXSize = 0;
        int newZSize = 0;
        if (zManuSize > zSize)
        {
            newZSize = zManuSize - zSize;
        }
        else
        {
            newZSize = zManuSize - currentZSize;
        }
        if (xManuSize > xSize)
        {
            newXSize = xManuSize - zSize;
        }
        else
        {
            newXSize = zManuSize - currentXsize;
        }

        _placingAreaBoundationsXAxis = new Vector2(currentXsize, newXSize);
        _placingAreaBoundationsZAxis = new Vector2(currentZSize, newZSize);
        PlaceTrees();
    }

    private void PlaceTrees()
    {
        if (_treeObjectList == null && _treeObjectList.Count == 0)
            return;

        float newTreeCount = Random.Range(_objectCount.x, _objectCount.y);

        for (int i = 0; i < (int)newTreeCount; i++)
        {
            foreach (GameObject treeobject in _treeObjectList)
            {
                _randomXAxis = Random.Range(_placingAreaBoundationsXAxis.x, _placingAreaBoundationsXAxis.y);
                _randomYAxis = 0.0f;
                _randomZAxis = Random.Range(_placingAreaBoundationsZAxis.x, _placingAreaBoundationsZAxis.y);

                Vector3 treePlacingPosition = new Vector3(_randomXAxis, _randomYAxis, _randomZAxis);

                Instantiate(treeobject, treePlacingPosition, Quaternion.identity, this.gameObject.transform);
            }
        }
    }

}
