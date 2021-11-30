using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossWalk : StateMachineBehaviour
{
    public float speed;

    [SerializeField] float attackRange;

    #region Private Variables
    private Transform player;
    private Rigidbody2D rb;
    private MiniBoss miniBoss;
    private int rand;
    #endregion

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        miniBoss = animator.GetComponent<MiniBoss>();

        rand = Random.Range(0, 3);
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        miniBoss.LookAtPlayer();

        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);

        rb.MovePosition(newPos);
        
        if (Vector2.Distance(player.position, rb.position) <= attackRange)
        {
            if (rand == 0)
            {
                animator.SetTrigger("attack1");
            }
            else if (rand == 1)
            {
                animator.SetTrigger("attack2");
            }
            else
            {
                animator.SetTrigger("attack3");
            }
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
