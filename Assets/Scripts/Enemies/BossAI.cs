using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    [SerializeField] private float nextWaypointDistance = 3f; // Determine the distance between the companion and the target
    [SerializeField] private float moveSpeed = 2f; // Determine the speed of the companion
    public Transform target;

    private Seeker seeker;
    private Path path;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    //private bool walking = false;
    //private Coroutine moveCoroutine;
    public Animator animator;

    [SerializeField] private int currentWaypoint = 0;

    [SerializeField] public bool reachedEndOfPath { get; set; } = false;

    public bool CanMove
    {
        get
        {
            return animator.GetBool("canMove");
        }
        set
        {
            animator.SetBool("canMove", value);
        }
    }

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        InvokeRepeating("CalculatePath", 0f, .5f);
    }

    /// <summary>
    /// Calculate the path to the target
    /// </summary>
    private void CalculatePath()
    {
        // If the seeker is done calculating the path
        if (seeker.IsDone() && !target.IsUnityNull())
        {
            // Start the path from the current position to the target position
            seeker.StartPath(transform.position, target!.position, OnPathComplete);
        }
    }
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void Update()
    {

        //animator.SetBool("attack", true);
        if (path == null || reachedEndOfPath)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        } else
        {
            reachedEndOfPath = false;
        }

        // Move the companion to the target
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        // Calculate the force to move the companion
        Vector2 force = moveSpeed * Time.fixedDeltaTime * direction;
        // Move the companion
        if (CanMove)
        {
            rb.MovePosition(rb.position + force);
        }
        // Calculate the distance between the companion and the target
        //rb.AddForce(force);
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        // Flip the sprite
        if (direction.x < 0)
        {
            //spriteRenderer.flipX = true;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        } else if (direction.x > 0)
        {
            //spriteRenderer.flipX = false;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        // If the distance between the companion and the target is less than the next waypoint distance
        if (distance < nextWaypointDistance)
            currentWaypoint++;


    }

    public void MoveToTarget()
    {
        target = PlayerController.Instance.transform;
        reachedEndOfPath = false;
        animator.SetBool("walk", true);
    }

    public void StopToTarget()
    {
        target = null;
        reachedEndOfPath = true;
        animator.SetBool("walk", false);
    }

}
