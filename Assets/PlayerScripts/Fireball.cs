using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public GameObject hitEffect;
    
    private PowerUpManager powerUpManager;
    public float baseDamage = 20f;
    public float damage;


    private void Start()
    {
        powerUpManager = FindObjectOfType<PowerUpManager>();
        if(powerUpManager != null)
        {
            damage = powerUpManager.GetCurStrength();
        }
        else
        {
            damage = baseDamage;
        }
        
    }
    public void ResetStrength()
    {
        damage = baseDamage;
    }

    public void SetStrength(float newStrength)
    {
        damage = newStrength;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Enemy"))
        {
            Debug.Log("Hit: " + hitInfo.name); 

            Enemy enemy = hitInfo.GetComponent<Enemy>();
            if (enemy != null)
            {
                Debug.Log("Dealing damage to: " + hitInfo.name);
                enemy.TakeDamage(damage); 
            }
            if (hitEffect != null)
            {
                GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
                Destroy(effect, 5f);
            }

            Destroy(gameObject); 

        }
    }

}
