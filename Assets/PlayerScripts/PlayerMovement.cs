using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float baseSpeed = 5f;
    public float speed;
    private Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public Vector2 movement;

    

    public enum Directions{UP, DOWN, LEFT, RIGHT}
    public Directions facingDirections = Directions.RIGHT;
    private readonly int animWalk = Animator.StringToHash("Player_walk");
    private readonly int animIdle = Animator.StringToHash("Player_idle");

    private readonly int animAttack = Animator.StringToHash("Player_Attack");

    public bool isAttacking = false;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = baseSpeed;
    }
    // Update is called once per frame
    void Update()
    {

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        FacingDirection();
        WalkingAnimation();
        AttackingAnimation();
    
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

    }
    
    void FacingDirection()
    {
        if (movement.x != 0)
        {
            if (movement.x > 0)
            {
                facingDirections = Directions.RIGHT;
            }
            else if (movement.x < 0)
            {
                facingDirections = Directions.LEFT; 
            }        
        }
       
    }
  
    void WalkingAnimation()
    {
        if (facingDirections == Directions.LEFT)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else if (facingDirections == Directions.RIGHT)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }

    
        if (movement.SqrMagnitude() > 0)
        {   
            animator.CrossFade(animWalk, 0);
        }
        else 
        {
        animator.CrossFade(animIdle, 0);
        }
    }
    
    void AttackingAnimation()
    {
        if (Input.GetButtonDown("FireRight") && !isAttacking)
        {
            isAttacking = true;
            animator.SetBool("isAttacking", true);
            animator.CrossFade(animAttack, 0);
        }

        if (Input.GetButtonDown("FireLeft"))
        {
            isAttacking = false;
            animator.SetBool("isAttacking", false);
        }
        if (!isAttacking)
        {
            WalkingAnimation();
        }
    }


    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public void ResetSpeed()
    {
        speed = baseSpeed;
    }
}

   