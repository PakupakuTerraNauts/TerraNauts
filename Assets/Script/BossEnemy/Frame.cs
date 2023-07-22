using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame : MonoBehaviour
{
    private GameObject _light;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("MoveFrame");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            _light = GameObject.Find("FrameLight");
            Destroy(_light);
            Destroy(gameObject);
        }
    }

    IEnumerator MoveFrame()
    {
        for (int i = 0; i <= 10; i++)
        {
            transform.localScale = new Vector3(i/25f, i/25f, i/25f);
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 60; i++)
        {
            transform.Translate(0f, -0.05f, 0);
            yield return new WaitForSeconds(0.05f);
        }
        while(true)
        {
            for (int i = 0; i < 10; i++)
            {
                transform.Translate(0f, 0.01f, 0);
                yield return new WaitForSeconds(0.05f);
            }
            for (int i = 0; i < 10; i++)
            {
                transform.Translate(0f, -0.01f, 0);
                yield return new WaitForSeconds(0.05f);
            }
        }
    }
}
