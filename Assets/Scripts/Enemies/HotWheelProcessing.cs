using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotWheelProcessing : MonoBehaviour
{

    [SerializeField] private float attackRange = 0f; // Determine the range of the attack

    private BossAI bossAI;
    private Collider2D detectCollider;
    private DetectionEnemy detectEnemy;
    private float distanceFire;
    private Shooter shooter;


    private void Awake()
    {
        bossAI = GetComponent<BossAI>();
        detectCollider = GetComponentInChildren<Collider2D>();
        detectEnemy = GetComponentInChildren<DetectionEnemy>();
        shooter = GetComponent<Shooter>();
        distanceFire = attackRange - attackRange / 3;
    }

    private void Update()
    {
        var distance = Vector2.Distance(transform.position, PlayerController.Instance.transform.position);
        EnemyControl(distance);
        DetectDistanceToAttack(distance);
    }

    private void DetectDistanceToAttack(float distance)
    {
        if (distance < distanceFire)
        {
            bossAI.StopToTarget();
            bossAI.animator.SetBool("attack", true);
            //shooter.Attack();
        } else
        {
            bossAI.animator.SetBool("attack", false);
        }
    }

    private void EnemyControl(float distance)
    {
        if (distance < attackRange)
        {
            bossAI.MoveToTarget();
        } else
        {
            bossAI.StopToTarget();
        }
    }

}
