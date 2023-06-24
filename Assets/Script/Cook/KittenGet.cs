using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KittenGet : MonoBehaviour
{
    public bool isKittenLoaded = false;
    private bool trigger = false;
    private Collider2D _collider;


    private void OnTriggerStay2D(Collider2D collision)
    {
        trigger = true;
        _collider = collision;
    }
    private void OnTriggerExit2D()
    {
        trigger = false;
    }

    [System.Obsolete]
    private void Update()
    {
        
        if (isKittenLoaded && Input.GetKeyDown(KeyCode.Escape))
        {
            Application.UnloadLevel("CookingScean");
            Resources.UnloadUnusedAssets();
            isKittenLoaded = false;
        }

        

        if (trigger == true && _collider.gameObject.tag == ("Player") && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Get");
            isKittenLoaded = !isKittenLoaded;
            if (isKittenLoaded)
            {
                Application.LoadLevelAdditive("CookingScean");
            }
            else
            {
                Application.UnloadLevel("CookingScean");
                Resources.UnloadUnusedAssets();
            }
        }
    }

    public bool GetIsKittenLoaded()
    {
        return isKittenLoaded;
    }
}
