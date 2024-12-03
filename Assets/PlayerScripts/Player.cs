using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    public Slider slider; 
    public float maxHealth = 6;
    public float health;
    public bool isInvincible = false;
    private bool isLoading = false;
    [SerializeField] 
    private AudioClip coin, key; 
    private AudioSource audioSource;

    [SerializeField]

    
    public int keys = 0, coins = 0;
 
    public TextMeshProUGUI KeysText;

    public TextMeshProUGUI coinsText;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        SetHealth();

    }
   
   private void OnTriggerEnter2D(Collider2D collision)
   {
        if(collision.tag == "Key")
        {
            
            Destroy(collision.gameObject);
            keys += 1;
            KeysText.text = keys.ToString();

            if(keys >= 3)
            {
                audioSource.clip = key;
                audioSource.Play();
                StartCoroutine(WaitToLoad(audioSource.clip.length));
            }
        }
        if(collision.tag == "Coin")
        {
            audioSource.clip = coin;
            audioSource.Play();
            audioSource.SetScheduledEndTime(AudioSettings.dspTime + 2f);
            Destroy(collision.gameObject);
            coins += 1;
            coinsText.text = coins.ToString();

            if(coins >= 10)
            {
                LoadNextLevel();
            }
        }
   }

    
    public void TakeDamage(float damage)
   {
        if (isInvincible) return;

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

    public void SetInvincibility(bool invincible)
    {
        isInvincible = invincible;  
    }

    public void LoadNextLevel()
    {   
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }
    
    private IEnumerator WaitToLoad(float waitTime)
    {
        if(isLoading) yield break;
        isLoading = true;

        yield return new WaitForSeconds(waitTime);
        LoadNextLevel();
    }
}
