using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;
using System.Collections.Generic;
public class PowerUpManager : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private float curStrength = 20f;
    private Player player;
    public List<string> activePowerUps = new List<string>();
    public TextMeshProUGUI powerUpText;
    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        player = GetComponent<Player>();
       
    }
    
    public void AddPowerUp(string powerUpName)
    {
        if (!activePowerUps.Contains(powerUpName))
        {
            activePowerUps.Add(powerUpName);
            UpdatePowerUpDisplay();
        }
    }

    public void RemovePowerUp(string powerUpName)
    {
        if (activePowerUps.Contains(powerUpName))
        {
            activePowerUps.Remove(powerUpName);
            UpdatePowerUpDisplay();
        }
    }

    void UpdatePowerUpDisplay()
    {
        if (powerUpText != null)
        {
            powerUpText.text = string.Join(" ", activePowerUps);
        }
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PowerUpInvincibility"))
        {
            Destroy(collision.gameObject);
            StartCoroutine(PowerUpTimer("Invincibility", 5f)); 
        }

         if (collision.CompareTag("PowerUpHealth"))
        {
            Destroy(collision.gameObject);
            player.Heal(20f); 
        }

        if (collision.CompareTag("PowerUpSpeed"))
        {
            Destroy(collision.gameObject);
            StartCoroutine(PowerUpTimer("Speed", 5f));
        }

        if (collision.CompareTag("PowerUpStrength"))
        {
            Destroy(collision.gameObject);
            StartCoroutine(PowerUpTimer("Strength", 5f)); 
        }
    }

   

    private IEnumerator PowerUpTimer(string powerUpName, float duration)
    {
        AddPowerUp(powerUpName);

        if (powerUpName == "Invincibility")
        {
            player.SetInvincibility(true);
        }
        else if (powerUpName == "Speed")
        {
            playerMovement.SetSpeed(10f);
        }
        else if (powerUpName == "Strength")
        {
            curStrength = 40f;
        }
    
        yield return new WaitForSeconds(duration);

        if (powerUpName == "Invincibility")
        {
            player.SetInvincibility(false);
        }
        else if (powerUpName == "Speed")
        {
            playerMovement.ResetSpeed();
        }
        else if (powerUpName == "Strength")
        {
            curStrength = 20f;
        }

        RemovePowerUp(powerUpName);
    }
}
