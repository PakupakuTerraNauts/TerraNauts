using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Escapeボタンでメニュー画面表示
public class MenuChange:MonoBehaviour
{
    private static bool isLoaded = false;
    public static bool isMenuOpen = false;
    //public KittenGet _kittenGet;

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            //if (!_kittenGet.isKittenLoaded)
            //{
                LoadMenuScean();
            //}

        }

    }

    public static void LoadMenuScean(){
        isLoaded = !isLoaded;
        if(isLoaded)
        {
            Time.timeScale = 0;
            Application.LoadLevelAdditive("MenuScean");
            isMenuOpen = true;
        }
        else
        {
            Application.UnloadLevel("MenuScean");
            Resources.UnloadUnusedAssets();
            isMenuOpen = false;
            Time.timeScale = 1;
        }
    }

}
