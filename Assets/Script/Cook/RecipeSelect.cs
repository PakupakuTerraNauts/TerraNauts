using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//���V�s���Z���N�g���ꂽ���Ƀ{�^������������
public class RecipeSelect:MonoBehaviour
{
    GameObject _selectedObject;
    Button button;

    PushRecipeButton _pushRecipeButton;

    void Update()
    {
        if(EventSystem.current.currentSelectedGameObject)
        {
            _pushRecipeButton = GameObject.Find("ItemButton").GetComponent<PushRecipeButton>();
            _selectedObject = EventSystem.current.currentSelectedGameObject;

            if(_selectedObject != null && button != null)
            {
                button = _selectedObject.GetComponent<Button>();
                _pushRecipeButton.RecipeButton(button);
            }

        }
    }
}
