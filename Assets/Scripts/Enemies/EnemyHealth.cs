using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int health = 3;
    [SerializeField] private float knockBackThrust = 15f; //xác định mức đẩy lùi khi kẻ địch bị tấn công.
    [SerializeField] private GameObject deathVFXPrefab; //một đối tượng Prefab được sử dụng để tạo hiệu ứng khi kẻ địch bị tiêu diệt.
    [SerializeField] private bool haveAnimationDeath = false; //xác định xem kẻ địch có hiệu ứng chết không.

    private int currentHealth; // current health
    private KnockBack knockBack; // reference to KnockBack script
    private Flash flash; // reference to Flash script
    private Animator animator;

    public bool isDead => currentHealth <= 0;

    private void Awake()
    {
        flash = GetComponent<Flash>();
        knockBack = GetComponent<KnockBack>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        currentHealth = health;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        knockBack.GetKnockBack(PlayerController.Instance.transform, knockBackThrust);
        StartCoroutine(flash.FlashRoutine());
        StartCoroutine(CheckDetectDeathRoutine());
    }

    private IEnumerator CheckDetectDeathRoutine()
    {
        yield return new WaitForSeconds(flash.GetRestoreMatTime());
        DetectDeath();
    }

    public void DetectDeath()
    {
        if(currentHealth <= 0)
        {
            // Instantiate death VFX in the position of the enemy when it dies
            var animationDeath = Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);

            // Destroy the death VFX after 2 seconds
            Destroy(animationDeath, 2);

            // Add level to the player
            PlayerController.Instance.GetComponent<PlayerLevel>().LevelUp();

            // Play death animation
            if (haveAnimationDeath)
            {
                animator.SetTrigger("dead");
                return;
            }

            // Destroy the enemy current gameObject
            Destroy(gameObject);
        }
    }

}
