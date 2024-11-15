using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
public class PowerUpManager : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private float curStrength = 20f;
    private Player player;
    
    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        player = GetComponent<Player>();
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PowerUpInvincibility"))
        {
            Destroy(collision.gameObject);
            ActivateInvincibility(5f); 
        }

         if (collision.CompareTag("PowerUpHealth"))
        {
            Destroy(collision.gameObject);
            player.Heal(20f); 
        }

        if (collision.CompareTag("PowerUpSpeed"))
        {
            Destroy(collision.gameObject);
            ActivateSpeedPowerUp(10f, 5f); 
        }

        if (collision.CompareTag("PowerUpStrength"))
        {
            Destroy(collision.gameObject);
            ActivateStrengthPowerUp(40f, 5f); 
        }
    }

   
    private void ActivateInvincibility(float duration)
    {
        StartCoroutine(PowerUpTimer(duration));
    }

    public float GetCurStrength()
    {
        return curStrength;
    }

    private void ActivateStrengthPowerUp(float newStrength, float duration)
    {
        curStrength = newStrength;
        StartCoroutine(PowerUpTimer(duration));
    }

    public void ActivateSpeedPowerUp(float newSpeed, float duration)
    {
        
        playerMovement.SetSpeed(newSpeed);
        StartCoroutine(PowerUpTimer(duration));
    }

    private IEnumerator PowerUpTimer(float duration)
    {
        player.SetInvincibility(true);
        yield return new WaitForSeconds(duration);
        player.SetInvincibility(false);
        playerMovement.ResetSpeed(); 
        curStrength = 20f;

    }


}
