using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBS : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("MoveP");
    }

    IEnumerator MoveP()
    {
        while(true)
        {
            for (int i = 0; i < 70; i++)
            {
                transform.position += new Vector3(0.1f, 0f);
                yield return new WaitForSeconds(0.05f);
            }
            for (int i = 0; i < 140; i++)
            {
                transform.position -= new Vector3(0.1f, 0f);
                yield return new WaitForSeconds(0.05f);
            }
            for (int i = 0; i < 70; i++)
            {
                transform.position += new Vector3(0.1f, 0f);
                yield return new WaitForSeconds(0.05f);
            }
        }
    }
}
