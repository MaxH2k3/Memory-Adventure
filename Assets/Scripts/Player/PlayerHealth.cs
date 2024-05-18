using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Singleton<PlayerHealth>
{
    [SerializeField] private int health = 5;
    [SerializeField] private float knockBackThrust = 15f; //xác định mức đẩy lùi khi kẻ địch bị tấn công. 
    [SerializeField] private float damgeRecoveryTime = 3f; //xác định thời gian hồi phục sau khi bị tấn công.

    private int currentHealth; // current health
    private KnockBack knockBack; // reference to KnockBack script
    private Flash flash; // reference to Flash script
    //private bool isInvulnerable = false; //determine if the player is invulnerable
    private bool canTakeDamage = true; //determine if the player can take damage

    protected override void Awake()
    {
        base.Awake();
        flash = GetComponent<Flash>();
        knockBack = GetComponent<KnockBack>();
    }

    private void Start()
    {
        currentHealth = health;
    }

    // Update is called once per frame
    private void OnCollisionStay2D(Collision2D collision)
    {
        // Check if the player is colliding with an enemy
        EnemyAI enemy = collision.gameObject.GetComponent<EnemyAI>();

        // If the player is colliding with an enemy and can take damage
        if (enemy)
        {
            // Take damage
            TakeDamage(1, transform);
            // Knock back the player
            knockBack.GetKnockBack(collision.gameObject.transform, knockBackThrust);
            // Flash the player
            StartCoroutine(flash.FlashRoutine());
        }

    }

    public void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            // Destroy the enemy current gameObject
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Take damage from the enemy
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="hitTransform"></param>
    public void TakeDamage(int damage, Transform hitTransform)
    {
        // If the player can't take damage, return
        if (!canTakeDamage) return;
        // Knock back the player
        knockBack.GetKnockBack(hitTransform, knockBackThrust);
        // Flash the player
        StartCoroutine(flash.FlashRoutine());
    }
    
    public void HealPlayer()
    {
        currentHealth += 1;
    }

    private void TakeDamage(int damage)
    {
        ScreenShakeManager.Instance.ShakeScreen();
        canTakeDamage = false;
        // Take damage from the enemy 
        currentHealth -= damage;
        // Check if the player is dead
        StartCoroutine(DamageRecoveryRoutine());
        
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damgeRecoveryTime);
        canTakeDamage = true;
    }

}
