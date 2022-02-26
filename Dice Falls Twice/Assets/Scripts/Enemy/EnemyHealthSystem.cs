using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthSystem
{
    public EventHandler OnHealthChange;

    private int _health;
    private int _healthMax;

    public EnemyHealthSystem(int healthMax)
    {
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
        if (_health < 0)
        {
            _health = 0;
        }
        if (OnHealthChange != null)
        {
            OnHealthChange(this, EventArgs.Empty);
        }
    }
}
