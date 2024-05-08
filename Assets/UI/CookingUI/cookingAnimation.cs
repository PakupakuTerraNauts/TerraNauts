using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cookingAnimation : MonoBehaviour
{
    public GameObject _AnimationCanvas;
    public GameObject _animationChara;
    public GameObject _CompleteText;
    public GameObject _Ani_DishName;
    public GameObject _dishBack;
    public GameObject _dishIcon;

    public ItemDataBase _itemDataBase;
    FoodSourceData _foodSourceData;

    Text cmp_Text;
    Text dishText;
    Image dishImage;

    bool animation = true;
    public GameObject animationObj;
    Text animationText;

    // Start is called before the first frame update
    void Start()
    {
        cmp_Text = _CompleteText.GetComponent<Text>();
        dishText = _Ani_DishName.GetComponent<Text>();
        dishImage = _dishIcon.GetComponent<Image>();
        animationText = animationObj.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C) && PushCookButton.cookOK && animation)
        {
            Debug.Log("アニメーションスタート");
            _AnimationCanvas.SetActive(true);
            _animationChara.SetActive(true);
            _CompleteText.SetActive(false);
            _Ani_DishName.SetActive(false);
            _dishBack.SetActive(false);
            _dishIcon.SetActive(false);

            StartCoroutine(Stop());
            
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(animation)
            {
                animation = false;
                animationText.text = "アニメーションオフ";
            }else
            {
                animation = true;
                animationText.text = "アニメーションオン";
            }
        }
    }

    IEnumerator Stop()
    {
        yield return new WaitForSecondsRealtime(2);

        _foodSourceData = _itemDataBase.ItemSearch(PushDishButton.nowPushDish);
        dishImage.sprite = _foodSourceData.icon;
        dishText.text = _foodSourceData.itemName;

        _animationChara.SetActive(false);
        _CompleteText.SetActive(true);
        _Ani_DishName.SetActive(true);
        _dishBack.SetActive(true);
        _dishIcon.SetActive(true);

        StartCoroutine(EndAnimation());
    }

    IEnumerator EndAnimation()
    {
        yield return new WaitForSecondsRealtime(2);
        _AnimationCanvas.SetActive(false);
    }
}
