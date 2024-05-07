﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int health = 3;
    [SerializeField] private float knockBackThrust = 15f; //xác định mức đẩy lùi khi kẻ địch bị tấn công.
    [SerializeField] private GameObject deathVFXPrefab; //một đối tượng Prefab được sử dụng để tạo hiệu ứng khi kẻ địch bị tiêu diệt.

    private int currentHealth; // current health
    private KnockBack knockBack; // reference to KnockBack script
    private Flash flash; // reference to Flash script

    private void Awake()
    {
        flash = GetComponent<Flash>();
        knockBack = GetComponent<KnockBack>();
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
    }

    public void DetectDeath()
    {
        if(currentHealth <= 0)
        {
            // Instantiate death VFX in the position of the enemy when it dies
            Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);

            // Destroy the enemy current gameObject
            Destroy(gameObject);
        }
    }

}
