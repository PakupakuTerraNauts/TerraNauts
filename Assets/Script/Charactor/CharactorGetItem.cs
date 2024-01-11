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
    public GameObject HPObject;

    private void Start()
    {
        int count = 1;
        if(_playerRecipeManager.GetItemCount("meatBun") != 1)
        {
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
        SetHPText();
        SetHpBar();
    }

    //�A�C�e���ƏՓ�
    public void OnCollisionEnter2D(Collision2D collision)
    {
        //�^�O�� "item" �̃A�C�e��
        if(collision.gameObject.tag == "item")
        {
            int count = 1;
            //�A�C�e���ǉ�
            _playerFoodManager.CountItem(collision.gameObject.name, count);

        }
        //�^�O�� "recipe" �̃A�C�e��
        else if(collision.gameObject.tag == "recipe")
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

    //HP�e�L�X�g��\��
    public void SetHPText()
    {
        Text hptext = HPObject.GetComponent<Text>();
        string maxHP = Player.HP.ToString();
        string nowHP = Player.nowHP.ToString();
        hptext.text = nowHP + "/" + maxHP;
    }

    //HP�o�[��\��
    public void SetHpBar()
    {
        GameObject _HPSlider = GameObject.Find("HPSlider");
        Slider HPSlider_S = _HPSlider.GetComponent<Slider>();
        HPSlider_S.maxValue = Player.HP;
        HPSlider_S.value = Player.nowHP;
    }
}
