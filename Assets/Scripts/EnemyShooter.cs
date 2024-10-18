using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
   public GameObject bullet;
   public Transform bulletposition;
   private float timer;

   private GameObject player;
   void Start()
   {
        player = GameObject.FindGameObjectWithTag("Player");
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
        Instantiate(bullet, bulletposition.position, Quaternion.identity);
   }
}

