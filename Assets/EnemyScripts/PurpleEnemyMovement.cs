using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class YellowEnemyMovement : MonoBehaviour
{    
    public float speed;
    public float up;
    public float down;
    private bool movingUp = true;
    private Rigidbody2D rb;
    public GameObject bullet;
    public Transform bulletposition;
    private float timer;
    public float shootTimer = 2f;
    [SerializeField]
    private float detection = 7f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    
    void Update()
    {
        YellowMovement();
        EnemyShoot();
    }

    private void YellowMovement()
    {
         rb.velocity = new Vector2(rb.velocity.x, speed * (movingUp ? 1 : -1));
        
        if (movingUp && transform.position.y >= up)
        {
            movingUp = false;
        }
        else if (!movingUp && transform.position.y <= down) 
        {
            movingUp = true;
        }
    }

    private void EnemyShoot()
    {
        timer += Time.deltaTime;
        if (timer >= shootTimer)
        {
            timer = 0f; 
            shoot();
        }
    }
    private void shoot()
    {
        
        Instantiate(bullet, bulletposition.position, Quaternion.identity); 
         
    }

     private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detection);
    }

}