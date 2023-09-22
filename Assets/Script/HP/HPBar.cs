using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] private Image hpGauge;
    [SerializeField] private float maxHealth;

    private float currentHealth;

    void Awake(){
        currentHealth = maxHealth;
    }
    public void UpdateHP(float damage){
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        hpGauge.fillAmount = currentHealth / maxHealth;
    }
}