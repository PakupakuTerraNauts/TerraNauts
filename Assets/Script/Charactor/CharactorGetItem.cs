using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharactorGetItem : MonoBehaviour
{

    private FoodSourceData food;
    public PlayerFoodManager playerFoodManager;
    public PlayerRecipeManager _playerRecipeManager;
    public ItemDataBase itemDataBase;
    public GameObject meetObject;
    public GameObject waterObject;
    public GameObject grassObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetText("meet", meetObject);
        SetText("water", waterObject);
        SetText("grass", grassObject);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "item")
        {
            Debug.Log("Hit -> " + collision.gameObject.name);
            food = itemDataBase.ItemSearch(collision.gameObject.name);
            int count = 1;
            playerFoodManager.CountItem(collision.gameObject.name, count);

        }
        else if (collision.gameObject.tag == "recipe")
        {
            food = itemDataBase.ItemSearch(collision.gameObject.name);
            int count = 1;
            _playerRecipeManager.CountItem(collision.gameObject.name, count);
        }

    }

    public void SetText(string foodS, GameObject foodObject)
    {
        Text _text = foodObject.GetComponent<Text>();
        //playerFoodManager = GameObject.FindObjectOfType<playerFoodManager>();
        string id = foodS;
        string _textS = playerFoodManager.GetItemCount(id).ToString("d");
        _text.text = _textS;
    }
}
