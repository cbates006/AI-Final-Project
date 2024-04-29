using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int damage;
    public int maxHP;
    public int currentHP;

    public bool CheckDeath()
    {        
        if (currentHP <= 0)
            return true;
        else
            return false;
    }

    public void TakeDamage(int dmg)
    {
        currentHP -= dmg;
    }

    public void Heal(int health)
    {
        currentHP += health;
        if(currentHP > 30)
        {
            currentHP = 30;
        }
    }
}
