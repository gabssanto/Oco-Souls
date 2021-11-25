using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour, ICollisionHandler
{
    #region Hid from Inspector
    [HideInInspector] public Transform target;
    [HideInInspector] public bool inRange;
    #endregion
    #region Serialized Field
    [Header("Enemy Parameters")]
    [SerializeField] private float attackDistance; // Minimum attack distance
    [SerializeField] private float moveSpeed;
    [SerializeField] private float cooldown;
    [Header("Patrolling Area")]
    [SerializeField] Transform leftLimit;
    [SerializeField] Transform rightLimit;

    [SerializeField] float damage;
    #endregion
    #region Public Variables
    [Header("Player Detection")]
    public GameObject hotZone;
    public GameObject triggerArea;
    #endregion
    #region Private Variables
    private Animator anim;
    private float distance; // Distance between enemy and player
    private bool attackMode;
    private bool cooling;
    private float initialTimer;
    #endregion

    private void Awake()
    {
        SelectTarget();
        
        initialTimer = cooldown;
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (!attackMode)
        {
            Move();
        }

        if (!InsideOfLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("SkeletonAttack"))
        {
            SelectTarget();
        }

        if (inRange)
        {
            EnemyLogic();
        }
    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);

        if (distance > attackDistance)
        {
            StopAttack();
        }
        else if (attackDistance >= distance && cooling == false)
        {
            Attack();
        }

        if (cooling)
        {
            Cooldown();
            anim.SetBool("attack", false);
        }
    }

    void Move()
    {
        anim.SetBool("canWalk", true);

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("SkeletonAttack"))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        cooldown = initialTimer;
        attackMode = true;

        anim.SetBool("canWalk", false);
        anim.SetBool("attack", true);
    }
    void StopAttack()
    {
        cooling = false;
        attackMode = false;

        anim.SetBool("attack", false);
    }

    void Cooldown()
    {
        cooldown -= Time.deltaTime;

        if (cooldown <= 0 && cooling && attackMode)
        {
            cooling = false;
            cooldown = initialTimer;
        }
    }

    public void TriggerCooling()
    {
        cooling = true;
    }

    private bool InsideOfLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    public void SelectTarget()
    {
        float distanceToLeft  = Vector3.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector3.Distance(transform.position, rightLimit.position);

        if (distanceToLeft > distanceToRight)
        {
            target = leftLimit;
        }
        else
        {
            target = rightLimit;
        }

        Flip();
    }

    public void Flip()
    {
        Vector3 rotation = transform.eulerAngles;

        if (transform.position.x > target.position.x)
        {
            rotation.y = 180;
        }
        else
        {
            rotation.y = 0;
        }

        transform.eulerAngles = rotation;
    }

    public void CollisionEnter(string colliderName, GameObject other)
    {
        if (colliderName == "AxeHitBox" && other.tag == "Player")
        {
            other.GetComponent<Health>().TakeDamage(damage);
        }
    }
}