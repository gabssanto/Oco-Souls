using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class SpikeTrap : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] private Health health;
    private void Reset()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            health.TakeDamage(damage);
        }
    }
}
