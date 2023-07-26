using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBoss : MonoBehaviour
{
    private Vector3 _player;
    private bool isStopping = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("PlayerBossScene").transform.position;
        if(_player.x >= -27f && isStopping == false)
        {
            isStopping = true;
            StartCoroutine("MoveCamera");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator MoveCamera()
    {
        for (int i = 0; i <= 100; i++)
        {
            transform.Translate(0.3f, 0f, 0f);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
