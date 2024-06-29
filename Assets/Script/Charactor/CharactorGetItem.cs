using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//�L�����N�^�[���A�C�e�����Q�b�g�������̏���
public class CharactorGetItem:MonoBehaviour
{
    //Manager
    public PlayerFoodManager _playerFoodManager;
    public PlayerRecipeManager _playerRecipeManager;
    //
    public GameObject meatObject;
    public GameObject waterObject;
    public GameObject grassObject;

    private CapsuleCollider2D capcol = null;

    private void Start()
    {
        capcol = GetComponent<CapsuleCollider2D>();

        int count = 1;
        if(_playerRecipeManager.GetItemCount("meatBun") != 1)
        {
            // ����������ݒ�
            _playerRecipeManager.CountItem("meatBun", count);
            _playerRecipeManager.CountItem("hamburger", count);
            _playerRecipeManager.CountItem("tapiocaMilkTea", count);
        }

    }

    // Update is called once per frame
    void Update()
    {
        SetText("meat", meatObject);
        SetText("water", waterObject);
        SetText("grass", grassObject);
    }

    //�A�C�e���ƏՓ�
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //�^�O�� "item" �̃A�C�e��
        if(collision.tag == "item")
        {
            int count = 1;
            //�A�C�e���ǉ�
            _playerFoodManager.CountItem(collision.gameObject.name, count);
        }
        //�^�O�� "recipe" �̃A�C�e��
        else if(collision.tag == "recipe")
        {
            int count = 1;
            //�A�C�e���ǉ�
            _playerRecipeManager.CountItem(collision.gameObject.name, count);
        }
    }

    //���A���A���̏��������Q�[����ʂɕ\��
    public void SetText(string foodS, GameObject foodObject)
    {
        Text _text = foodObject.GetComponent<Text>();
        string id = foodS;
        string _textS = _playerFoodManager.GetItemCount(id).ToString("d");
        _text.text = _textS;
    }

}
