using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealth;

    private void Start()
    {
        totalHealthBar.fillAmount = playerHealth.currentHealth / 10;
    }

    private void Update()
    {
        currentHealth.fillAmount = playerHealth.currentHealth / 10;
    }
}
