using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Animator animator;
    [SerializeField] bool isFlipped = false;

    [Header("Sound Effects")]
    [SerializeField] AudioSource footstep;
    [SerializeField] AudioSource stab;
    [SerializeField] AudioSource stomp;
    [SerializeField] AudioSource swoosh;

    private Health health;
    private BossWalk bossWalk;

    private void Start()
    {
        animator = GetComponent<Animator>();
        health = animator.GetComponent<Health>();
        bossWalk = animator.GetBehaviour<BossWalk>();
    }
    private void Update()
    {
        if (health.currentHealth == 12)
        {
            animator.SetTrigger("taunt");
            health.TakeDamage(1);
            bossWalk.speed = bossWalk.speed + 1;
        }
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);

            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);

            isFlipped = true;
        }
    }

    private void Footsteps()
    {
        footstep.Play();
    }
    private void Stab()
    {
        stab.Play();
    }
    private void Stomp()
    {
        stomp.Play();
    }
    private void Swoosh()
    {
        swoosh.Play();
    }
}
