using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    protected int _maxHealth;
    protected int _currentHealth;

    public virtual void TakeDame(int dame)
    {
        if (_currentHealth > 0)
        {
            _currentHealth -= dame;
        }
    }
    public virtual void Heal(int healValue)
    {
        if(_currentHealth < _maxHealth)
        {
            _currentHealth += healValue;
        }
        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
    }
}
