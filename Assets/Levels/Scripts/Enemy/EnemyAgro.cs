using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAgro : MonoBehaviour
{
    [SerializeField] GameObject Skeleton;

    Animator animator;

    [SerializeField] Transform player;
    [SerializeField] float agroRange;
    [SerializeField] float moveSpeed;

    Rigidbody2D rb2d;
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = Skeleton.GetComponent<Animator>();
    }

    
    void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, player.position);
        //print("distToPlayer:" + distToPlayer);

        if (distToPlayer < agroRange)
        {
            ChasePlayer();
        }
        else
        {
            StopChasingPlayer();
        }
    }

    void ChasePlayer()
    {
        if (transform.position.x < player.position.x)
        {
            // enemy to the left of the player
            rb2d.velocity = new Vector2(moveSpeed, 0);
            transform.localScale = new Vector2(1, 1);
        }
        else if (transform.position.x > player.position.x)
        {
            // enemy to the right of the player
            rb2d.velocity = new Vector2(-moveSpeed, 0);
            transform.localScale = new Vector2(-1, 1); 
        }

        animator.SetBool("moving", true);
    }

    void StopChasingPlayer()
    {
        rb2d.velocity = new Vector2(0, 0);

        animator.SetBool("moving", false);
    }
}
