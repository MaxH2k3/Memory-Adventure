using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowWeapon : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject hitVFXPrefab;

    private WeaponInfo weaponInfo;
    private Vector3 startPosition;
    private Vector3 moveDirection;
    private Rigidbody2D rb;
    private bool isReturning = false;

    private AudioManager audioManager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        startPosition = transform.position;
        moveDirection = transform.right;
    }

    private void Update()
    {
        // Move the projectile
        if(!isReturning)
        {
            rb.velocity = moveSpeed * moveDirection;

            // Check if the projectile exceeds the weapon range
            DetectFireDistance();
        }
        else
        {
            // Calculate the direction to return to the player
            Vector3 returnDirection = (PlayerController.Instance.transform.position - transform.position).normalized;
            // Move the projectile towards the player
            rb.velocity = moveSpeed * returnDirection;
            // Check if the projectile has reached the player
            /*if (Vector3.Distance(transform.position, PlayerController.Instance.transform.position) < 0.1f)
            {
                // Destroy the projectile
                Destroy(gameObject);
                // Set the player's weapon to active

            }*/


        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();

        if (enemyHealth)
        {
            // Deal damage to the enemy
            enemyHealth.TakeDamage(weaponInfo.weaponDamage);
            // Init animation for the enemy
            var animationHit = Instantiate(hitVFXPrefab, transform.position, Quaternion.identity);
            // Destroy the hit VFX after 2 seconds
            Destroy(animationHit, 2);
        }

        if(collision.gameObject.CompareTag("Player"))
        {
            // Destroy the projectile
            Destroy(gameObject);
            // Set the player's weapon to active
            ActiveWeapon.Instance.gameObject.SetActive(true);
            ActiveWeapon.Instance.isAttacking = false;
            audioManager.StopSFX();
        }
    }

    /// <summary>
    /// Destroy the projectile if it exceeds the weapon range
    /// </summary>
    private void DetectFireDistance()
    {
        if (Vector3.Distance(startPosition, transform.position) > weaponInfo.weaponRange)
        {
            //Destroy(gameObject);
            isReturning = true;
        }
    }

    /// <summary>
    /// Update the weapon info of the projectile
    /// </summary>
    /// <param name="weaponInfo"></param>
    public void UpdateWeaponInfo(WeaponInfo weaponInfo)
    {
        this.weaponInfo = weaponInfo;
    }
}
