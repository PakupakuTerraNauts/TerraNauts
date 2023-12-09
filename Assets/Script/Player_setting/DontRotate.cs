using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontRotate : MonoBehaviour
{
    public void dontRotate(){
        transform.localScale = new Vector3(-2, 2, 1);
    }
}
