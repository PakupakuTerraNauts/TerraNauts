using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Escapeボタンでメニュー画面表示
public class MenuChange : MonoBehaviour
{
    private static bool isLoaded = false;
    public static bool isMenuOpen = false;
    private static int menuButtonNum;

    [System.Obsolete]
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadMenuScean(0);
        }
    }

/// <summary>
/// メニューシーンを表示する
/// </summary>
/// <param name="openMenuScene">最初に表示したいメニュー 左から0-5</param>
    public static void LoadMenuScean(int openMenuScene)
    {
        isLoaded = !isLoaded;
        if (isLoaded)
        {
            Time.timeScale = 0;
            SceneManager.LoadScene("MenuScean", LoadSceneMode.Additive);
            isMenuOpen = true;

            menuButtonNum = openMenuScene;
            SceneManager.sceneLoaded += MenuSceneLoaded;
        }
        else
        {
            SceneManager.UnloadSceneAsync("MenuScean");
            Resources.UnloadUnusedAssets();
            isMenuOpen = false;
            Time.timeScale = 1;
        }
    }

    private static void MenuSceneLoaded(Scene nextScene, LoadSceneMode mode)
    {
        // "Intaractable"オブジェクトをシーンから取得
        var _interactable = GameObject.Find("MainScean").GetComponent<Interactable>();
        if (_interactable != null)
        {
            _interactable.SetFirstSelect(menuButtonNum);
        }
    }
}
