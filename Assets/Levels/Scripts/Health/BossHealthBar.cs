using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] private Health bossHealth;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealth;

    private void Start()
    {
        
    }

    private void Update()
    {
        currentHealth.fillAmount = bossHealth.currentHealth / 20;
    }
}
