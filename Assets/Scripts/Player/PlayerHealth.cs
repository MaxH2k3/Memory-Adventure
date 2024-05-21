using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : Singleton<PlayerHealth>
{
    public bool isDead {  get; private set; }
    [SerializeField] private int health = 5;
    [SerializeField] private float knockBackThrust = 15f; //xác định mức đẩy lùi khi kẻ địch bị tấn công. 
    [SerializeField] private float damgeRecoveryTime = 3f; //xác định thời gian hồi phục sau khi bị tấn công.
    
    private Slider healthSlider;
    private int currentHealth; // current health
    private KnockBack knockBack; // reference to KnockBack script
    private Flash flash; // reference to Flash script
    //private bool isInvulnerable = false; //determine if the player is invulnerable
    private bool canTakeDamage = true; //determine if the player can take damage

    const string HEALTH_SLIDER_TEXT = "Health Slider";
    const string TOWN_TEXT = "Scene1";
    readonly int DEATH_HASH = Animator.StringToHash("Death");

    protected override void Awake()
    {
        base.Awake();
        flash = GetComponent<Flash>();
        knockBack = GetComponent<KnockBack>();
    }

    private void Start()
    {
        isDead = false;
        currentHealth = health;
        UpdateHealthSlider();
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
        canTakeDamage = false;
        currentHealth -= damage;
        StartCoroutine(DamageRecoveryRoutine());
        UpdateHealthSlider();
        CheckIfPlayerDeath();
    }
    
    public void HealPlayer()
    {
        if(currentHealth < health)
        {
            currentHealth += 1;
        }
        UpdateHealthSlider();
       
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

    private void CheckIfPlayerDeath()
    {
        if (currentHealth <= 0 && !isDead)  
        {
            isDead = true;
            Destroy(ActiveWeapon.Instance.gameObject);
            currentHealth = 0;
            GetComponent<Animator>().SetTrigger(DEATH_HASH);
            StartCoroutine((string)DeathLoadSceneRoutine());
        }
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damgeRecoveryTime);
        canTakeDamage = true;
    }

    private IEnumerable DeathLoadSceneRoutine()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
        SceneManager.LoadScene(TOWN_TEXT);
    }

    private void UpdateHealthSlider()
    {
        if (healthSlider == null)
        {
            healthSlider = GameObject.Find("Health Slider").GetComponent<Slider>();
        }

        healthSlider.maxValue = health;
        healthSlider.value = currentHealth;
    }

}
