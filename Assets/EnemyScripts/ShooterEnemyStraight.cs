using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class ShooterEnemyStraight : MonoBehaviour
{
    public float damage = 20f;
    public float lifetTime = 5f;
   public GameObject bullet;
   public Transform bulletposition;
   private float timer  = 0f;
   private float shootingInterval = 2f;

   void Start ()
   {
        
   }
   
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= shootingInterval)
        {
            shoot();
            timer = 0f;
        }
       
    }
     void shoot ()
     {
        GameObject bulletShot = Instantiate(bullet, bulletposition.position, bulletposition.rotation);
        Rigidbody2D rb = bulletShot.GetComponent<Rigidbody2D>();
        if(rb != null)
        {
            rb.velocity = bulletposition.right * 5f;
        }
     }
        private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Player playerScript = other.gameObject.GetComponent<Player>();
            
            if (playerScript != null)
            {
                playerScript.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
        else if(other.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
