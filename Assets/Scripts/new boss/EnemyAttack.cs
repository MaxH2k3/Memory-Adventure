using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int attackDamage;
    public Vector2 knockBack;

    private void Awake()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.GetComponent<PlayerController>())
        {
            //PlayerController.Instance.OnHit(attackDamage, knockBack);
            PlayerHealth.Instance.TakeDamage(attackDamage, PlayerController.Instance.transform);
        }

    }
}
