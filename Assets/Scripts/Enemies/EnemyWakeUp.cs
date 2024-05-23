using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWakeUp : MonoBehaviour
{
    private DetectionEnemy detectEnemy;
    private Animator animator;

    private void Awake()
    {
        detectEnemy = GetComponent<DetectionEnemy>();
        animator = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        StartCoroutine(StartWake());
    }

    private IEnumerator StartWake()
    {
        if (detectEnemy.IsPlayerDetected())
        {
            animator.SetTrigger("wake");
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

            animator.SetBool("idle", true);
            //yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        } else
        {
            animator.SetBool("idle", false);
           // yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        }
    }

}
