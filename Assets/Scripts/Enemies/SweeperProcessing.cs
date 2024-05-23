using Assets.Scripts.Enums;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SweeperProcessing : MonoBehaviour
{
    [SerializeField] private float attackRange = 0f; // Determine the range of the attack

    private BossAI bossAI;
    private Collider2D detectCollider;
    private DetectionEnemy detectEnemy;
    private BossHealth bossHealth;
    

    private void Awake()
    {
        bossAI = GetComponent<BossAI>();
        detectCollider = GetComponentInChildren<Collider2D>();
        detectEnemy = GetComponentInChildren<DetectionEnemy>();
        bossHealth = GetComponent<BossHealth>();
    }

    private void Update()
    {
        if(bossHealth.isDead)
        {
            return;
        }
        EnemyControl();
        DetectDistanceToAttack();
    }

    private void DetectDistanceToAttack()
    {
        if(detectEnemy.IsPlayerDetected())
        {
            bossAI.StopToTarget();
            bossAI.animator.SetTrigger("attack");
        }
    }

    private void EnemyControl()
    {
        if(detectEnemy.IsPlayerDetected())
        {
            return;
        }

        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < attackRange)
        {
            bossAI.MoveToTarget();
        } else
        {
            bossAI.StopToTarget();
        }
    }

    

}

