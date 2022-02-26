using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem
{
    public EventHandler OnHealthChange;

    private GameObject _character;
    public GameObject Character { set { _character = value; } }

    private int _health;
    private int _healthMax;

    public HealthSystem(int healthMax, GameObject character)
    {
        this._character = character;
        this._healthMax = healthMax;
        this._health = healthMax;
    }
    public int GetHealth() 
    {
        return _health;
    }
    public int GetMaxHealth()
    {
        return _healthMax;
    }
  
    public void TakeDamage(int damageAmount) 
    {
        _health -= damageAmount;
        if (_health <= 0) 
        {
            _health = 0;
        }
        if (OnHealthChange != null) 
        {
            OnHealthChange(this, EventArgs.Empty);
        }
    }
    public void TakeHeal(int healAmount) 
    {
        _health += healAmount;
        if (_health > _healthMax) 
        {
            _health = _healthMax;
        }
        if (OnHealthChange != null)
        {
            OnHealthChange(this, EventArgs.Empty);
        }
    }
}
