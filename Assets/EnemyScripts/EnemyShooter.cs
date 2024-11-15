using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class EnemyShooter : MonoBehaviour
{
   public GameObject bullet;
   public Transform bulletposition;
   private float timer;

   private Rigidbody2D rb;
   public float detection = 7f;
   public bool attack = false;

   private Transform player;
   void Start()
   {
        rb = GetComponent<Rigidbody2D>();
        GameObject playerObject = GameObject.FindWithTag("Player");
        
        if(playerObject != null)
        {
          player = playerObject.transform;
        }
   }

     void Update()
     {
        timer += Time.deltaTime;
        if(timer > 2)
        {
            timer = 0;
            shoot();
        }
     }
     void shoot ()
     {
          if (player)
          {
               float distanceToPlayer = Vector2.Distance(transform.position, player.position);
               if(distanceToPlayer <= detection || attack == true)
               {
                    attack = true;
                    Instantiate(bullet, bulletposition.position, Quaternion.identity); 
               }
          }
          
     }
     private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detection);
    }
}

