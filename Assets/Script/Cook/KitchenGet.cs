using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//調理台前での処理
public class KittenGet:MonoBehaviour
{
    public bool isKittenLoaded = false;
    bool trigger = false;
    Collider2D _collider;
    GameObject _cookInfoCanvas;

    private void Start()
    {
        _cookInfoCanvas = GameObject.Find("CookInfoCanvas");
        _cookInfoCanvas.SetActive(false);
    }

    //調理台に触れている時
    private void OnTriggerStay2D(Collider2D collision)
    {
        trigger = true;
        _collider = collision;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        trigger = true;
        _collider = collision;
    }

    //調理台から出た時
    private void OnTriggerExit2D()
    {
        trigger = false;
        _collider = null;
    }

    [System.Obsolete]
    private void Update()
    {

        if(isKittenLoaded && Input.GetKeyDown(KeyCode.Escape))
        {
            Application.UnloadLevel("CookingScean");
            Resources.UnloadUnusedAssets();
            isKittenLoaded = false;
        }


        //プレイヤーが調理台前で"E"を押した時
        if(trigger && _collider.gameObject.tag == ("Player") && Input.GetKeyDown(KeyCode.E))
        {
            isKittenLoaded = !isKittenLoaded;
            if(isKittenLoaded)
            {
                Application.LoadLevelAdditive("CookingScean");
            }
            else
            {
                Application.UnloadLevel("CookingScean");
                Resources.UnloadUnusedAssets();
            }
        }

        if(trigger && _collider.gameObject.tag == ("Player"))
        {
            _cookInfoCanvas.SetActive(true);
        }
        else
        {
            _cookInfoCanvas.SetActive(false);
        }
    }

    public bool GetIsKittenLoaded()
    {
        return isKittenLoaded;
    }
}
