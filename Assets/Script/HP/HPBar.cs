using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] private Image hpGauge;
    [SerializeField] public float maxHealth;    // �{�X�̃X�N���v�g����Q�Ƃ��邽��public�ɕύX

    private float currentHealth;

    public void Awake(){
        currentHealth = maxHealth;
        Debug.Log(maxHealth);
    }
    
    public void UpdateHP(float damage){
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        Debug.Log("damage : " + damage + "currentHealth : " + currentHealth);
        hpGauge.fillAmount = currentHealth / maxHealth;
    }
    
}