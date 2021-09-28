using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePooling : MonoBehaviour
{
    // Inspector Visible
    [SerializeField] private List<GameObject>   _treeObjectList;
    [SerializeField] private int                _numOfTrees;

    // Private Members
    private static List<GameObject> _cloneTreeObjectList;

    // Static Members
    private static int _numberOfTrees;

    private void Awake()
    {
        _numberOfTrees = _numOfTrees;
        _cloneTreeObjectList = new List<GameObject>();
    }

    void Start()
    {
        for (int i = 0; i < _numOfTrees; i++)
        {
            int pickRandomTree = Random.Range(0, _treeObjectList.Count - 1);
            GameObject treeObject = Instantiate(_treeObjectList[pickRandomTree], Vector3.zero, Quaternion.identity);
            treeObject.SetActive(false);
            treeObject.transform.parent = transform;
            _cloneTreeObjectList.Add(treeObject);
        }
    }

    public static GameObject GetTreeFromPool()
    {
        for(int i = 0; i < _cloneTreeObjectList.Count; i++)
        {
            if (!_cloneTreeObjectList[i].activeSelf)
            {
                return _cloneTreeObjectList[i];
            }
        }
        return null;
    }
}
