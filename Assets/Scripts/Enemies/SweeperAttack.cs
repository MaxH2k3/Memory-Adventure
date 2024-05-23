using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweeperAttack : MonoBehaviour
{
    [SerializeField] public int attackDamage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
        if (collision != null && playerHealth)
        {
            playerHealth.TakeDamage(attackDamage, PlayerController.Instance.transform);
        }
    }
}
