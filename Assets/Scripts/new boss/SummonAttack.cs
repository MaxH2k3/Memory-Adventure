using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonAttack : MonoBehaviour
{
	public int attackDamage;
	public Vector2 knockBack;
	public DeathSummon deathSummon;

	private void Awake()
	{
		deathSummon = GetComponentInParent<DeathSummon>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{

        PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
        if (collision != null && playerHealth)
        {
            playerHealth.TakeDamage(attackDamage, PlayerController.Instance.transform);
            deathSummon.OnHit();
        }

    }
}
