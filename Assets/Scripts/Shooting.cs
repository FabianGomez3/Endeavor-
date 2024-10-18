using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform shootingPoint;
    public GameObject fireBall;

    public float fireSpeed = 10f;
    void Update()
    {
        if (Input.GetButtonDown("FireUp"))   
        {  
            Shoot(Vector2.up);
        }
         if (Input.GetButtonDown("FireRight"))   
        {  
            Shoot(Vector2.right);
        }
         if (Input.GetButtonDown("FireLeft"))   
        {  
            Shoot(Vector2.left);
        }
         if (Input.GetButtonDown("FireDown"))   
        {  
            Shoot(Vector2.down);
        }
    }
    
    //youtube mixed with edits
    void Shoot(Vector2 direction)
    {
        GameObject fire = Instantiate(fireBall, shootingPoint.position, Quaternion.identity);
        Rigidbody2D rb = fire.GetComponent<Rigidbody2D>();
        rb.velocity = direction * fireSpeed; 

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        fire.transform.rotation = Quaternion.Euler(new Vector3(0,0,angle));

        
    }
}
