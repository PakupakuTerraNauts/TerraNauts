using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public GameObject HPObject;
    private PlayerStatusData _playerStatusData;

    void Awake()
    {
        _playerStatusData = Resources.Load<PlayerStatusData>("PlayerStatusData");
    }

    void Update()
    {
        SetHPText();
        SetHpBar();
    }
    
    //HPテキストを表示
    public void SetHPText()
    {
        Text hptext = HPObject.GetComponent<Text>();
        int HP = _playerStatusData.HP;
        string maxHP = HP.ToString();
        string nowHP = _playerStatusData.nowHP.ToString();
        hptext.text = nowHP + "/" + maxHP;
    }

    //HPバーを表示
    public void SetHpBar()
    {
        GameObject _HPSlider = GameObject.Find("HPSlider");
        Slider HPSlider_S = _HPSlider.GetComponent<Slider>();
        int HP = _playerStatusData.HP;
        HPSlider_S.maxValue = HP;
        HPSlider_S.value = _playerStatusData.nowHP;
    }
}
