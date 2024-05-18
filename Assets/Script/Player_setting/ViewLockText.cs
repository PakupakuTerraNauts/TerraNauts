using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewLockText : MonoBehaviour
{
    private bool viewLock = false;

    public GameObject viewLockObj;
    private Text viewLockText;

    void Start(){
        viewLockText = viewLockObj.GetComponent<Text>();
    }

    void Update(){
        bool vKey = Input.GetKeyDown("v");

        if(vKey){
            if(viewLock){
                viewLock = false;
                viewLockText.text = "���E���Œ�I�t";
            }
            else{
                viewLock = true;
                viewLockText.text = "���E���Œ�I��";
            }
        }
    }
}
