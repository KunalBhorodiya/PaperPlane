using System.Collections.Generic;
using UnityEngine;

public class TreePlacingScript : MonoBehaviour
{
    // Inspector Visible
    [SerializeField] private List<GameObject>   _treeObjectList;
    [SerializeField] private Vector2            _placingAreaBoundationsXAxis;
    [SerializeField] private Vector2            _placingAreaBoundationsZAxis;

    [Header("Not Greater than 80")]
    [SerializeField] private Vector2            _objectCount;

    // Private members
    private float _randomXAxis = 0.0f;
    private float _randomYAxis = 0.0f;
    private float _randomZAxis = 0.0f;

    void Start()
    {
        if (_treeObjectList == null && _treeObjectList.Count == 0)
            return;

        float newTreeCount = Random.Range(_objectCount.x, _objectCount.y);

        for(int i = 0; i < (int) newTreeCount; i++)
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
