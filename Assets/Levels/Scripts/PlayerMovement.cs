using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator animator;

    private void Awake()
    {
        // Interage com o codigo do Unity diretamente
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        // Mudar pra onde olha conforme move pra Esquerda/Direita
        if (horizontalInput > 0.01f) transform.localScale = new Vector3(10, 10, 10);
        else if (horizontalInput < -0.01f) transform.localScale = new Vector3(-10, 10, 10);

        if (Input.GetKey(KeyCode.Space)) body.velocity = new Vector2(body.velocity.x, speed / 2);

        // Set Animator params
        animator.SetBool("run", horizontalInput != 0);

    }
}
 