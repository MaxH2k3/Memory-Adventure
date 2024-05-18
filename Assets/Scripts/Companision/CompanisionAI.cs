using Pathfinding;
using System.Collections;
using UnityEngine;

public class CompanisionAI : MonoBehaviour
{
    [SerializeField] private float nextWaypointDistance = 3f; // Determine the distance between the companion and the target
    [SerializeField] public PlayerState playerState; // Get the player state

    private Transform target;
    private Seeker seeker;
    private Path path;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    //private bool walking = false;
    //private Coroutine moveCoroutine;
    public Animator animator;

    [SerializeField] private int currentWaypoint = 0;

    [SerializeField] public bool reachedEndOfPath { get; set; } = false;

    private void Start()
    {
        target = FindAnyObjectByType<PlayerController>().transform;
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
        if (seeker.IsDone())
        {
            // Start the path from the current position to the target position
            seeker.StartPath(transform.position, target.position, OnPathComplete);
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
        if (path == null || reachedEndOfPath)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            animator.SetBool("walk", false);
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        animator.SetBool("walk", true);
        // Move the companion to the target
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        // Calculate the force to move the companion
        Vector2 force = playerState.moveSpeed * Time.fixedDeltaTime * direction;
        // Move the companion
        rb.MovePosition(rb.position + force);
        // Calculate the distance between the companion and the target
        rb.AddForce(force);
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        // Flip the sprite
        if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
        } else if (direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        // If the distance between the companion and the target is less than the next waypoint distance
        if (distance < nextWaypointDistance)
            currentWaypoint++;

        
    }

    


}


/*void MoveToTarget()
    {
        if (moveCoroutine != null) StopCoroutine(moveCoroutine);
        moveCoroutine = StartCoroutine(MoveToTargetCoroutine());
    }

    private void Update()
    {
    }

    IEnumerator MoveToTargetCoroutine()
    {
        int currentWP = 0;

        while (currentWP < path.vectorPath.Count - 1)
        {
            Vector2 direction = ((Vector2)path.vectorPath[currentWP] - rb.position).normalized;
            Vector2 force = direction * moveSpeed * Time.deltaTime;
            transform.position += (Vector3)force;

            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWP]);
            if (distance < nextWaypointDistance)
                currentWP++;

            if (force.x != 0)
                if (force.x < 0)
                    spriteRenderer.transform.localScale = new Vector3(-1, 1, 0);
                else
                    spriteRenderer.transform.localScale = new Vector3(1, 1, 0);

            yield return null;

            animator.SetFloat("walk", path.vectorPath.Count - 1 - currentWP);
        }


        
    }*/