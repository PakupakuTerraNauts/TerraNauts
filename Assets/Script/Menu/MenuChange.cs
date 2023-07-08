using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Escapeボタンでメニュー画面表示
public class MenuChange : MonoBehaviour
{
    private bool isLoaded = false;
    public KittenGet _kittenGet;

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
