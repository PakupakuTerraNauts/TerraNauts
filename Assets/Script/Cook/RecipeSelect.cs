using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//レシピがセレクトされた時にボタンを押す処理
public class RecipeSelect : MonoBehaviour
{
    GameObject _selectedObject;
    Button button;

    PushRecipeButton _pushRecipeButton;

    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject)
        {
            _pushRecipeButton = GameObject.Find("PrefabRecipeButton").GetComponent<PushRecipeButton>();
            _selectedObject = EventSystem.current.currentSelectedGameObject;
            if (_selectedObject != null &&  button != null)
            {
                button = _selectedObject.GetComponent<Button>();
                _pushRecipeButton.RecipeButton(button);
            }    
            
        }
    }
}
