using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsController : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public int maxEnergyTokens;
    public int currentEnergyTokens;

    public float moveSpeed;
    public int power;

    public bool TakeDamage(int amount)
    {
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ConsumeEnergyToken(int amount)
    {
        currentEnergyTokens -= amount;
    }

    public void Rest()
    {
        currentEnergyTokens += 1;
        if(currentEnergyTokens >= maxEnergyTokens)
        {
            currentEnergyTokens = maxEnergyTokens;
        }
    }
}
