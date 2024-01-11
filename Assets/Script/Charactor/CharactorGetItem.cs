using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//キャラクターがアイテムをゲットした時の処理
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

    //アイテムと衝突
    public void OnCollisionEnter2D(Collision2D collision)
    {
        //タグが "item" のアイテム
        if(collision.gameObject.tag == "item")
        {
            int count = 1;
            //アイテム追加
            _playerFoodManager.CountItem(collision.gameObject.name, count);

        }
        //タグが "recipe" のアイテム
        else if(collision.gameObject.tag == "recipe")
        {
            int count = 1;
            //アイテム追加
            _playerRecipeManager.CountItem(collision.gameObject.name, count);
        }

    }

    //肉、水、草の所持数をゲーム画面に表示
    public void SetText(string foodS, GameObject foodObject)
    {
        Text _text = foodObject.GetComponent<Text>();
        string id = foodS;
        string _textS = _playerFoodManager.GetItemCount(id).ToString("d");
        _text.text = _textS;
    }

    //HPテキストを表示
    public void SetHPText()
    {
        Text hptext = HPObject.GetComponent<Text>();
        string maxHP = Player.HP.ToString();
        string nowHP = Player.nowHP.ToString();
        hptext.text = nowHP + "/" + maxHP;
    }

    //HPバーを表示
    public void SetHpBar()
    {
        GameObject _HPSlider = GameObject.Find("HPSlider");
        Slider HPSlider_S = _HPSlider.GetComponent<Slider>();
        HPSlider_S.maxValue = Player.HP;
        HPSlider_S.value = Player.nowHP;
    }
}
