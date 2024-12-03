using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealthB = 100f;
    public float healthB;
    public int damageB;
    public Player players;

    public GameObject death;
    [SerializeField] 
    private AudioClip deadEnemy; 
    private AudioSource audioSource;
    private bool isDead = false;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        healthB = maxHealthB;
    }

    public void TakeDamage(float damageAmount)
    {
        healthB -= damageAmount; 
        if (healthB <= 0)
        {
            Die();
        }
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.CompareTag("Player"))
        {
            Player players = coll.gameObject.GetComponent<Player>();
            if (players != null)
            {
                players.TakeDamage(damageB);
            }
        }

    }
    void Die()
    {
        if(isDead) return;
        isDead = true;

        audioSource.clip = deadEnemy;
        audioSource.Play();
        GetComponent<LootBag>().InstantiateLoot(transform.position);
        Destroy(gameObject, deadEnemy.length);
    }

}
