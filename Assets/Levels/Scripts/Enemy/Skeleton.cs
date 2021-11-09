using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private int damage;
    [SerializeField] private CapsuleCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;

    private Animator animator;
    private Health playerHealth;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        // Attack only when player in sight
        if (PlayerInSight() && cooldownTimer >= attackCooldown)
        {
            //Attack
            cooldownTimer = 0;
            animator.SetTrigger("attack");

        }
    }

    private void DamagePlayer()
    {
        if (PlayerInSight())
        {
            // Damage player health
            playerHealth.TakeDamage(damage);
        }
    }

    private bool PlayerInSight()
    {
       var sightDistante = new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z);

        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x / 10 * colliderDistance,
            sightDistante, 0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
            playerHealth = hit.transform.GetComponent<Health>();

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        var sightDistante = new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x / 10 * colliderDistance, sightDistante);
    }
}
