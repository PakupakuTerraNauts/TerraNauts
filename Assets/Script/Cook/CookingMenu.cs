using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Cooking���j���[�Ƀ��V�s��\��
public class CookingMenu:MonoBehaviour
{
    //Script
    public ItemDataBase _itemDataBase;
    PlayerRecipeManager _playerRecipeManager;
    FoodSourceData _foodSourceData;
    //
    public GameObject _prefabObject;
    public GameObject _animationCanvas;

    GameObject _viewRecipeText;
    GameObject _textPrefab;
    GameObject _main;

    



    void Start()
    {
        _viewRecipeText = GameObject.Find("TileDishContent");

        _main = GameObject.Find("Main");
        _playerRecipeManager = _main.GetComponent<PlayerRecipeManager>();

        //���V�s�\��
        TextDelete(_viewRecipeText);
        SetRecipeText();

        //AnimationCanvas�̔�\��
        AnimationOFF();

    }

    //�e�L�X�g���폜
    public void TextDelete(GameObject _objectText)
    {
        foreach(Transform child in _objectText.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    //���V�s�̃e�L�X�g�쐬
    public void SetRecipeText()
    {
        Debug.Log("SetRecipeText");
        string[] _id = _playerRecipeManager.GetItemId();

        for(int i = 0; i < _id.Length; i++)
        {
            _foodSourceData = _itemDataBase.ItemSearch(_id[i]);

            _textPrefab = (GameObject)Instantiate(_prefabObject, transform.position, Quaternion.identity);
            _textPrefab.transform.SetParent(_viewRecipeText.transform, false);

            GameObject _cloneObject = GameObject.Find("ItemButton2(Clone)");

            Image _cloneText = _cloneObject.transform.GetChild(0).GetComponent<Image>();
            Text _cloneText2 = _cloneObject.transform.GetChild(1).GetComponent<Text>();
            Button _cloneButton = _cloneObject.GetComponent<Button>();
            Image _cloneImage = _cloneObject.GetComponent<Image>();

            _cloneText.enabled = true;
            _cloneText2.enabled = true;
            _cloneButton.enabled = true;
            _cloneImage.enabled = true;

            _cloneText.sprite = _foodSourceData.icon;
            _cloneText2.text = "";
            _cloneObject.name = _id[i];
        }
    }

    //AnimaitonCanvas���\���ɂ���
    public void AnimationOFF()
    {
        _animationCanvas.SetActive(false);
    }
}
