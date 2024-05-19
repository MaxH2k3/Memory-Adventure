using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanisionHealth : MonoBehaviour
{
    [SerializeField] private int health = 3;

    private int currentHealth; // current health
    private Animator animator;
    public bool isDead { get; set; } = false;
    private CompanisionAI companionAI;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        companionAI = GetComponent<CompanisionAI>();
    }

    private void Start()
    {
        currentHealth = companionAI.playerState.health;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        StartCoroutine(CheckDetectDeathRoutine());
    }

    private IEnumerator CheckDetectDeathRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        DetectDeath();
    }

    public void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            isDead = true;
            animator.SetBool("dead", true);
            companionAI.reachedEndOfPath = true;
        }
    }

}
