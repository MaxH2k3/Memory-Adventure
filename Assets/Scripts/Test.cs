using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Test : MonoBehaviour
{
   private AIDestinationSetter aiDestinationSetter;
    private AIPath aiPath;
    private Rigidbody2D rb;

    //private bool walking = false;
    //private Coroutine moveCoroutine;
    public Animator animator;

    private void Awake()
    {
        aiDestinationSetter = GetComponent<AIDestinationSetter>();
        aiPath = GetComponent<AIPath>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 direction = ((Vector2)PlayerController.Instance.transform.position - rb.position).normalized;
        if (direction.x < 0)
        {
            //spriteRenderer.flipX = true;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        } else if (direction.x > 0)
        {
            //spriteRenderer.flipX = false;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void MoveToTarget()
    {
        aiPath.canMove = true;
        animator.SetBool("walk", true);
    }

    public void StopToTarget()
    {
        aiPath.canMove = false;
        animator.SetBool("walk", false);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Skill"))
        {
            Debug.Log("Hit");
        }
    }

}
