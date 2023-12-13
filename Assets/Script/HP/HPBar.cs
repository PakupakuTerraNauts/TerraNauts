using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] private Image hpGauge;
    [SerializeField] public float maxHealth;    // �{�X�̃X�N���v�g����Q�Ƃ��邽��public�ɕύX

    public float currentHealth;
    public static HPBar instance;

    public void Awake(){
        if(instance == null){
            instance = this;
        }
    }
    
    public void UpdateHP(float damage){
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        Debug.Log(currentHealth);
        hpGauge.fillAmount = currentHealth / maxHealth;
    }
    
}