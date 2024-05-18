using Assets.Scripts.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float roamChangeDirFloat = 2f;
    [SerializeField] private float attackRange = 0f;
    [SerializeField] private MonoBehaviour enemyType;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private bool stopMovingWhileAttacking = false;

    private bool canAttack = true;

    
    private Vector2 roamPosition;
    private float timeRoaming = 0f; 

    private StateEnemy currentState;
    private EnemyPathFinding pathFinding;

    public void Awake()
    {
        pathFinding = GetComponent<EnemyPathFinding>();
        currentState = StateEnemy.Roaming;
    }

    private void Start()
    {
        roamPosition = GetRoamingPosition();
    }

    private void Update()
    {
        MovementStateControl();
    }

    private void MovementStateControl()
    {
        switch(currentState)
        {
            default:
            case StateEnemy.Roaming:
                Roaming();
            break;
            case StateEnemy.Attacking:
                Attacking();
            break;
        }
    }

    private void Roaming()
    {
        timeRoaming += Time.deltaTime;

        pathFinding.MoveTo(roamPosition);

        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < attackRange)
        {
            currentState = StateEnemy.Attacking;
        }

        if (timeRoaming > roamChangeDirFloat)
        {
            roamPosition = GetRoamingPosition();
        }
    }

    private void Attacking()
    {
        if(Vector2.Distance(transform.position, PlayerController.Instance.transform.position) > attackRange)
        {
            currentState = StateEnemy.Roaming;
        }

        if (attackRange != 0 && canAttack){
            canAttack = false;
            (enemyType as IEnemy).Attack();

            if(stopMovingWhileAttacking)
            {
                pathFinding.StopMoving();
            } else
            {
                pathFinding.MoveTo(roamPosition);
            }

            StartCoroutine(AttackCoolDown());
        }
    }

    private IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private Vector2 GetRoamingPosition()
    {
        timeRoaming = 0f;
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

 /*   private void Attacking()
    {
        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) > attackRange)
        {
            currentState = StateEnemy.Roaming;
        }

        if (attackRange != 0 && canAttack)
        {

            canAttack = false;
            (enemyType as IEnemy).Attack();

            if (stopMovingWhileAttacking)
            {
                pathFinding.StopMoving();
            }
            else
            {
                pathFinding.MoveTo(roamPosition);
            }

            StartCoroutine(AttackCooldownRoutine());
        }
    }*/

    private IEnumerator AttackCooldownRoutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }


}
