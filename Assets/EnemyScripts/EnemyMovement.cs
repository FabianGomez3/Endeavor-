using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform target;
    private Rigidbody2D rb;
    public float speed = 2f;

    public float detection = 7f;
    public bool attack = false;
    //Partial video partial me
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GetTarget();
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
            float distanceToPlayer = Vector2.Distance(transform.position, target.position);
            if(distanceToPlayer <= detection || attack == true)
            {
                attack = true;
                MoveTowardsTarget();
            }
        }
    }

    void GetTarget()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            target = playerObject.transform;
        }
    }


    void MoveTowardsTarget()
    {
        Vector2 direction = (target.position - transform.position).normalized;

        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detection);
    }

}
