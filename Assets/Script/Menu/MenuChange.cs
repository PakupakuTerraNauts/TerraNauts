using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuChange : MonoBehaviour
{
    private bool isLoaded = false;
    public KittenGet _kittenGet;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_kittenGet.isKittenLoaded)
            {
                isLoaded = !isLoaded;
                if (isLoaded)
                {
                    Application.LoadLevelAdditive("MenuScean");
                }
                else
                {
                    Application.UnloadLevel("MenuScean");
                    Resources.UnloadUnusedAssets();
                }
            }
            
        }

    }



}
