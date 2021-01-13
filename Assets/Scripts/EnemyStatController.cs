using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStatController : MonoBehaviour
{
    #region Properties
    [SerializeField] private string _name;

    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;

    [SerializeField] private int _damage;

    [SerializeField] private Sprite _battleSprite;

    public string[] loot;

    public string enemyEncounterLine;
    public string playerTurnLine;
    public string enemyFirstTurnLine;
    public string enemyLowHealthLine;
    public string playerLowHealthLine;
    public string enemyDefeatLine;
    #endregion

    #region Methods

    public void ResetHealth()
    {
        _currentHealth = _maxHealth;
    }
    public bool TakeDamage(int amount)
    {
        _currentHealth -= amount;
        if(_currentHealth <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public string GetName()
    {
        return _name;
    }

    public float GetHealthPercentage()
    {
        return _currentHealth / _maxHealth;
    }

    public int GetDamage()
    {
        return _damage;
    }

    public Sprite GetBattleSprite()
    {
        return _battleSprite;
    }
    #endregion
}
