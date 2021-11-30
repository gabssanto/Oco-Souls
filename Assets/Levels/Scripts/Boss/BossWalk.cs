using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWalk : StateMachineBehaviour
{
    public float speed;

    [SerializeField] float attackRange;

    #region Private Variables
    private Transform player;
    private Rigidbody2D rb;
    private Boss boss;
    private int rand;
    private Health health;
    #endregion

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();
        health = animator.GetComponent<Health>();

        if (health.currentHealth >= 13)
        {
            rand = Random.Range(0, 2);
        }
        else
        {
            rand = Random.Range(0, 3);
        }
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.LookAtPlayer();

        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);

        rb.MovePosition(newPos);

        if (Vector2.Distance(player.position, rb.position) <= attackRange)
        {
            if (rand == 0)
            {
                animator.SetTrigger("lightAttack");
            }
            else if (rand == 1)
            {
                animator.SetTrigger("heavyAttack");
            }
            else
            {
                animator.SetTrigger("attack");
            }
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}