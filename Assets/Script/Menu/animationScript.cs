using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class animationScript : MonoBehaviour
{

    Animator _animator;
    GameObject _statusCanvas;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _statusCanvas = GameObject.Find("MenuScean");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
