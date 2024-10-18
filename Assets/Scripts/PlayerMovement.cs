using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    
    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public Vector2 movement;

    public enum Directions{UP, DOWN, LEFT, RIGHT}
    public Directions facingDirections = Directions.RIGHT;
    private readonly int animWalk = Animator.StringToHash("Player_walk");
    private readonly int animIdle = Animator.StringToHash("Player_idle");

    private readonly int animAttack = Animator.StringToHash("Player_Attack");

    public bool isAttacking = false;
    
    
    // Update is called once per frame
    void Update()
    {

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        FacingDirection();
        WalkingAnimation();
       // AttackingAnimation();
    
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);

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
    
    // void AttackingAnimation()
    // {
    //     if (Input.GetKeyDown(KeyCode.Space) && !isAttacking)
    //     {
    //         isAttacking = true;
    //         animator.SetBool("isAttacking", true);
    //         animator.CrossFade(animAttack, 0);
    //     }

    //     if (Input.GetKeyUp(KeyCode.Space))
    //     {
    //         isAttacking = false;
    //         animator.SetBool("isAttacking", false);
    //     }
    //     if (!isAttacking)
    //     {
    //         WalkingAnimation();
    //     }
    // }

 
}

   