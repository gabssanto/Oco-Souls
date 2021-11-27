using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour, ICollisionHandler
{
    private float initialTimer;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void CollisionEnter(string colliderName, GameObject other)
    {
        if (colliderName == "WeaponHitBox" && other.tag == "Player")
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("LightAttack"))
            {
                other.GetComponent<Health>().TakeDamage(2);
                Debug.Log("LightAttack DMG=" + 2);
            }
            else if (anim.GetCurrentAnimatorStateInfo(0).IsName("HeavyAttack"))
            {
                other.GetComponent<Health>().TakeDamage(4);
                Debug.Log("HeavyAttack DMG="+ 4);
            }
            else
            {
                other.GetComponent<Health>().TakeDamage(3);
                Debug.Log("FullAttack DMG=" + 3);
            }
        }
    }
}
