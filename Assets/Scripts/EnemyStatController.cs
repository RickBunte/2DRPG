using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatController : MonoBehaviour
{
    public string enemyName;

    public int maxHealth;
    public int currentHealth;

    public int damage;

    public string[] loot;

    public string enemyEncounterLine;
    public string playerTurnLine;
    public string enemyFirstTurnLine;
    public string enemyLowHealthLine;
    public string playerLowHealthLine;
    public string enemyDefeatLine;

    public bool TakeDamage(int amount)
    {
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
