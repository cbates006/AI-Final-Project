using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Example scriptable object to maintain player health  
 * @author: Chris Branton
 * @created: 2019/04/20
 */
[CreateAssetMenu(fileName = "PlayerHealth", menuName = "Data/Health", order = 1)]
public class PlayerHealth : ScriptableObject
{
    public delegate void MyDelegate();
    public event MyDelegate onDeath;
    public float currentHealth = 100f;
    public float totalHealth = 100f;
    private float normalizedHealth = 1f;


    public float GetNormalizedHealth()
    {
        return normalizedHealth;
    }

    /**
     * Reduces current health by amount. Minimum health is zero.
     */
    public void ReduceHealth(float amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        if (currentHealth <= 0)
        {
            onDeath.Invoke();
        }
        normalizedHealth = currentHealth / totalHealth;
    }
}
