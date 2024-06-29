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

    private CapsuleCollider2D capcol = null;

    private void Start()
    {
        capcol = GetComponent<CapsuleCollider2D>();

        int count = 1;
        if(_playerRecipeManager.GetItemCount("meatBun") != 1)
        {
            // 初期料理を設定
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

    //アイテムと衝突
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //タグが "item" のアイテム
        if(collision.tag == "item")
        {
            int count = 1;
            //アイテム追加
            _playerFoodManager.CountItem(collision.gameObject.name, count);
        }
        //タグが "recipe" のアイテム
        else if(collision.tag == "recipe")
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

}
