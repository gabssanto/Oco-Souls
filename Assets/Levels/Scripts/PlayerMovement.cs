using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float attackCooldown;
    [SerializeField] private int damage;
    [SerializeField] private LayerMask groundLayer;

    [Header("Attack Params")]
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] LayerMask enemyLayers;


    private float cooldownTimer = Mathf.Infinity;
    private Rigidbody2D body;
    private Animator animator;
    private CapsuleCollider2D capsuleCollider;


    private void Awake()
    {
        // Interage com o codigo do Unity diretamente
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        HorizontalMove();

        if (Input.GetKey(KeyCode.Space) && isGrounded()) Jump();
        else if (Input.GetKey(KeyCode.Mouse0) && isGrounded()) Attack();


        animator.SetBool("grounded", isGrounded());
    }

    private void HorizontalMove ()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        // Mudar pra onde olha conforme move pra Esquerda/Direita
        if (horizontalInput > 0.01f) transform.localScale = new Vector3(10, 10, 10);
        else if (horizontalInput < -0.01f) transform.localScale = new Vector3(-10, 10, 10);

        animator.SetBool("run", horizontalInput != 0);
    }

    private void Jump ()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        animator.SetTrigger("jump");
    }

    private void Attack ()
    {
        cooldownTimer += Time.deltaTime;
         
        if (cooldownTimer >= attackCooldown)
        {
            cooldownTimer = 0;
            animator.SetTrigger("attack");

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            foreach(Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<Health>().TakeDamage(damage);
            }
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
}
 