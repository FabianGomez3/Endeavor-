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
    

    


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    
    void Update()
    {
        YellowMovement();
        
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

}