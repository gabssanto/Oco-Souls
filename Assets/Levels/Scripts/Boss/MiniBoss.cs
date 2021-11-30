using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Animator animator;
    [SerializeField] bool isFlipped = false;

    [Header("Sound Effects")]
    [SerializeField] AudioSource footstep;
    [SerializeField] AudioSource taunt;
    [SerializeField] AudioSource scream;
    [SerializeField] AudioSource attack1;
    [SerializeField] AudioSource attack2;
    [SerializeField] AudioSource attack3;

    private void Start()
    {
        animator = GetComponent<Animator>();
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
    private void Taunt()
    {
        taunt.Play();
    }
    private void Scream()
    {
        scream.Play();
    }
    private void Attack1()
    {
        attack1.Play();
    }
    private void Attack2()
    {
        attack2.Play();
    }
    private void Attack3()
    {
        attack3.Play();
    }
}
