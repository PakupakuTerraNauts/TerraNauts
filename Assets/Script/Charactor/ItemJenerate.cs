using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemJenerate : MonoBehaviour
{
    public GameObject _prefabObject;

    GameObject _newPrefab;
    GameObject _items;

    // Start is called before the first frame update
    void Start()
    {
        _items = GameObject.Find("Item");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            _newPrefab = (GameObject)Instantiate(_prefabObject, transform.position, Quaternion.identity);
            _newPrefab.transform.SetParent(_items.transform, false);
        }
    }
}
