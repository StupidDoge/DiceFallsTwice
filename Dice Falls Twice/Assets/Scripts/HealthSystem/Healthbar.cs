using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    private HealthSystem _healthSystem;
    [SerializeField]
    private Slider _sliderHpBar;
    [SerializeField]
    private Text _currentHp;
    [SerializeField]
    private Text _maxHp;

    public void Setup(HealthSystem healthSystem) 
    {
        this._healthSystem = healthSystem;
        healthSystem.OnHealthChange += HealthSystem_OnHealthChange;
        _maxHp.text = _healthSystem.GetMaxHealth()+"";
        _currentHp.text = _healthSystem.GetHealth()+"";
        _sliderHpBar.maxValue = _healthSystem.GetMaxHealth();
        _sliderHpBar.value = _healthSystem.GetHealth();
    }

    private void HealthSystem_OnHealthChange(object sender, EventArgs e)
    {
        _sliderHpBar.value = _healthSystem.GetHealth();
        _currentHp.text = _healthSystem.GetHealth() + "";
    }
}
