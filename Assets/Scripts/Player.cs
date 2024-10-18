using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class Player : MonoBehaviour
{
    public Slider slider; 
   public float maxHealth = 6;
   public float health;

   void Start()
   {
        SetHealth();
   }

   public void TakeDamage(float damage)
   {
        health -= damage; 
        UpdateHealth();
        if (health <= 0)
        {
            Destroy(gameObject);
        }

    }

    public void Heal(float amount)
    {
        health = Mathf.Min(health + amount, maxHealth);
        UpdateHealth();
    }
    public void SetMaxHealth(float health)
    {
        if (slider != null)
        {
            slider.maxValue = health;
        }
    }
    public void SetHealth()
    {
        health = maxHealth;
        SetMaxHealth(maxHealth);
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        if(slider != null)
        {
            slider.value = health;
        }
    }
}
