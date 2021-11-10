using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator animator;
    private bool dead;

    private void Awake()
    {
        currentHealth = startingHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            animator.SetTrigger("hurt");
            //iframes
        }
        else if (!dead)
        {
            animator.SetTrigger("die");
            GetComponent<PlayerMovement>().enabled = false;
            dead = true;
        }
    }

    public void PlusHealth()
    {
        if (currentHealth < 10)
            currentHealth += 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            TakeDamage(1);
        else if (Input.GetKeyDown(KeyCode.R))
            PlusHealth();
            
    }
}