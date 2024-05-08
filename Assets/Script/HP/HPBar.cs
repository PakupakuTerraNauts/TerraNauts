using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] private Image hpGauge;

    private float maxHealth;
    private float currentHealth;

    public void SetHP(float maxHP){
        maxHealth = maxHP;
        currentHealth = maxHealth;
    }
    
    public void UpdateHP(float damage){
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        Debug.Log("damage : " + damage + "currentHealth : " + currentHealth);
        hpGauge.fillAmount = currentHealth / maxHealth;
    }
    
}