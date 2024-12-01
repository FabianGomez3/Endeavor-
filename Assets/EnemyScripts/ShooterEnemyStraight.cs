using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class ShooterEnemyStraight : MonoBehaviour
{
   public GameObject bullet;
   public Transform bulletposition;
   private float timer  = 10f;
   private float shootingInterval = 2f;

   



    void Update()
    {
        timer += Time.deltaTime;
        GameObject player = GameObject.FindWithTag("Player");
    if(player != null)
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance >= shootingInterval)
        {
            shoot();
            timer = 0f;

        }
    }
}
     void shoot ()
     {
        GameObject bulletShot = Instantiate(bullet, bulletposition.position, bulletposition.rotation);
        Rigidbody2D rb = bulletShot.GetComponent<Rigidbody2D>();
        if(rb != null)
        {
            rb.velocity = bulletposition.right;
        }
     }
}
