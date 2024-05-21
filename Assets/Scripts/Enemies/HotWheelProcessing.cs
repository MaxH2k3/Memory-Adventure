using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotWheelAttack : MonoBehaviour
{

    [SerializeField] private float attackRange = 0f; // Determine the range of the attack

    private BossAI bossAI;
    private Collider2D detectCollider;
    private DetectionEnemy detectEnemy;


    private void Awake()
    {
        bossAI = GetComponent<BossAI>();
        detectCollider = GetComponentInChildren<Collider2D>();
        detectEnemy = GetComponentInChildren<DetectionEnemy>();
    }

    private void Update()
    {
        EnemyControl();
        //DetectDistanceToAttack();
    }

    private void DetectDistanceToAttack()
    {
        if (detectEnemy.IsPlayerDetected())
        {
            bossAI.StopToTarget();
            bossAI.animator.SetTrigger("attack");
        }
    }

    private void EnemyControl()
    {
        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < attackRange)
        {
            Debug.Log("Moving to target...");
            bossAI.MoveToTarget();
        } else
        {
            Debug.Log("Stopping to target...");
            bossAI.StopToTarget();
        }
    }

}
