using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;

public class RandomWalkEnemy : MonoBehaviour
{
    public float moveSpeed = 5f;      
    public float directionChangeTime = 4f; 
    

    private Rigidbody2D rb;
    public Vector2 curDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ChangeDirection());
    }

    void Update()
    {
        MoveEnemy();
    }

    void MoveEnemy()
    {
        rb.velocity =  curDirection * moveSpeed;
    }

    IEnumerator ChangeDirection()
    {
        while (true)
        {
            curDirection = GetRandomDirection();
            yield return new WaitForSeconds(directionChangeTime);
        }
    }

    private Vector2 GetRandomDirection()
    {
        float random = UnityEngine.Random.Range(0f, 360f) * Mathf.Deg2Rad;
        return new Vector2(MathF.Cos(random), MathF.Sin(random)).normalized;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        curDirection = GetRandomDirection();
    }
}