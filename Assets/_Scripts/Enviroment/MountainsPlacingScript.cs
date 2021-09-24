using System.Collections.Generic;
using UnityEngine;

public class MountainsPlacingScript : MonoBehaviour
{
    // Inspector Visible
    [SerializeField] private List<GameObject> _mountainObjectList;
    [SerializeField] private Vector2 _placingAreaBoundationsXAxis;
    [SerializeField] private Vector2 _placingAreaBoundationsZAxis;

    [Header("Not Greater than 10")]
    [SerializeField] private Vector2 _objectCount;

    // Private members
    private float _randomXAxis = 0.0f;
    private float _randomYAxis = 0.0f;
    private float _randomZAxis = 0.0f;

    void Start()
    {
        if (_mountainObjectList == null && _mountainObjectList.Count == 0)
            return;

        float newMountainCount = Random.Range(_objectCount.x, _objectCount.y);

        for (int i = 0; i < (int)newMountainCount; i++)
        {
            foreach (GameObject mountainobject in _mountainObjectList)
            {
                _randomXAxis = Random.Range(_placingAreaBoundationsXAxis.x, _placingAreaBoundationsXAxis.y);
                _randomYAxis = 0.0f;
                _randomZAxis = Random.Range(_placingAreaBoundationsZAxis.x, _placingAreaBoundationsZAxis.y);

                Vector3 mountainPlacingPosition = new Vector3(_randomXAxis, _randomYAxis, _randomZAxis);
                Quaternion newMountationRotation = Quaternion.Euler(mountainobject.transform.localRotation.x, Random.Range(0, 360), mountainobject.transform.localRotation.z);

                Instantiate(mountainobject, mountainPlacingPosition, newMountationRotation, this.gameObject.transform);
            }
        }
    }
}
