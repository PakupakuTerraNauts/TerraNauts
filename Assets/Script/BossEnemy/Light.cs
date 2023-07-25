using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("MoveLight");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void SetTrue()
    {
        gameObject.SetActive(true);
    }
    IEnumerator MoveLight()
    {
        while(true)
        {
            for (int i = 1; i <= 40; i++)
            {
                transform.eulerAngles = new Vector3(0f, 9f * i, 0f);
                yield return new WaitForSeconds(0.05f);
            }
        }
    }
}
