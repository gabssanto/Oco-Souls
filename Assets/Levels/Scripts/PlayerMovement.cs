using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, ICollisionHandler
{
    public float speed;
    [SerializeField] private float attackCooldown;
    [SerializeField] private int damage;
    [SerializeField] private LayerMask groundLayer;

    [Header("Attack Params")]
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] LayerMask enemyLayers;
    [Header("Sound Effects")]
    [SerializeField] AudioSource footstep;
    [SerializeField] AudioSource slash;

    public static Vector2 lastCheckPointPos = new Vector2(-7.517738f, -88.60466f);
    //public static Vector2 lastCheckPointPos = new Vector2(130f, -121f);

    private float cooldownTimer = Mathf.Infinity;

    private Rigidbody2D body;
    private Animator anim;
    private CapsuleCollider2D capsuleCollider;
    private void Awake()
    {
        // Interage com o codigo do Unity diretamente
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();

        GameObject.FindGameObjectWithTag("Player").transform.position = lastCheckPointPos;
    }

    private void Update()
    {
        HorizontalMove();

        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && isGrounded()) Jump();
        else if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.J)) && isGrounded()) Attack();

        anim.SetBool("grounded", isGrounded());
    }

    private void HorizontalMove ()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        // Mudar pra onde olha conforme move pra Esquerda/Direita
        if (horizontalInput > 0.01f) transform.localScale = new Vector3(10, 10, 10);
        else if (horizontalInput < -0.01f) transform.localScale = new Vector3(-10, 10, 10);

        anim.SetBool("run", horizontalInput != 0);
    }

    private void Jump ()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        anim.SetTrigger("jump");
    }

    private void Attack ()
    {
        cooldownTimer += Time.deltaTime;
         
        if (cooldownTimer >= attackCooldown)
        {
            cooldownTimer = 0;
            anim.SetTrigger("attack");
            
            /*Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            foreach(Collider2D enemy in hitEnemies)
            {
                if (enemy.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    enemy.GetComponent<Health>().TakeDamage(damage);
                }
            }*/
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint)
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(capsuleCollider.bounds.center,
            capsuleCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private void Footsteps()
    {
        footstep.Play();
    }

    private void Slash()
    {
        slash.Play();
    }

    public void CollisionEnter(string colliderName, GameObject other)
    {
        if (colliderName == "KnifeHitBox" && other.tag == "Enemy")
        {
            other.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
 