using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] RectTransform fader;
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

            if (GetComponent<PlayerMovement>() != null)
            {
                GetComponent<PlayerMovement>().enabled = false;

                fader.gameObject.SetActive(true);

                LeanTween.alpha(fader, 0, 0);
                LeanTween.alpha(fader, 1, 1.5f).setOnComplete(() =>
                {
                    fader.gameObject.SetActive(false);
                    gameOverPanel.SetActive(true);
                });

                currentHealth = 10;
            }
            if (GetComponentInParent<SkeletonPatrol>() != null)
                GetComponentInParent<SkeletonPatrol>().enabled = false;

            if (GetComponent<Skeleton>() != null)
            {
                GetComponent<Skeleton>().enabled = false;
            }

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
