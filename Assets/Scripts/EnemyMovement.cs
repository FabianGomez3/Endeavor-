using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform target;
    private Rigidbody2D rb;
    public float speed = 2f;

    //Partial video partial me
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    
    }

    void Update()
    {
      if (!target) 
      {
        GetTarget();
      } 
    }

    void FixedUpdate () 
    {
        if (target)
        {
            MoveTowardsTarget();
        }
    }

    void GetTarget()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }


    void MoveTowardsTarget()
    {
        Vector2 direction = (target.position - transform.position).normalized;

        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }

}
