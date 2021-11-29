using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    [SerializeField] AudioSource hitEffect;

    [Header("Player and Boss Only")]
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
        hitEffect.Play();

        if (GetComponent<Boss>() != null)
        {
            if (!(animator.GetCurrentAnimatorStateInfo(0).IsName("Taunt")))
            {
                currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
            }
        }
        else
        {
            currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        }

        if (currentHealth > 0)
        {
            
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
                    transform.gameObject.SetActive(false);
                });

                currentHealth = 10;
            }
            if (GetComponentInParent<EnemyBehaviour>() != null)
            {
                GetComponentInParent<EnemyBehaviour>().enabled = false;
                GetComponentInChildren<HotZoneCheck>().enabled = false;
            }
            if (GetComponent<Boss>() != null)
            {
                GetComponent<Boss>().enabled = false;

                fader.gameObject.SetActive(true);
                LeanTween.alpha(fader, 0, 0);
                LeanTween.alpha(fader, 1, 8f).setOnComplete(() =>
                {
                    fader.gameObject.SetActive(false);
                    gameOverPanel.SetActive(true);
                });
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
        /*if (Input.GetKeyDown(KeyCode.E))
            TakeDamage(1);
        else if (Input.GetKeyDown(KeyCode.R))
            PlusHealth();*/  
    }
}
