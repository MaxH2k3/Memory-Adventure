using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSummon : MonoBehaviour
{
	private Animator animator;
	private Rigidbody2D rb;
	private GameObject player;
	[SerializeField]
	public float moveSpeed;
	public bool isFaceleft = true;


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


	[SerializeField]
	private bool _isAlive = true;
	public bool IsAlive
	{
		get
		{
			return _isAlive;
		}
		set
		{
			_isAlive = value;
			animator.SetBool("isAlive", value);
		}
	}

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		player = GameObject.FindGameObjectWithTag("Player");
		animator = GetComponent<Animator>();
	}

	void Update()
	{
		if (CanMove)
		{
			flip();
			MoveTowardsPlayer(moveSpeed);
		}
	}

	void flip()
	{
		Vector3 movementDirection = player.transform.position - transform.position;
		if (movementDirection.x > 0 && !isFaceleft)
		{
			isFaceleft = true;
			Vector3 newScale = transform.localScale;
			newScale.x = Mathf.Abs(newScale.x);
			transform.localScale = newScale;
		}
		else if (movementDirection.x < 0 && isFaceleft)
		{
			isFaceleft = false;
			Vector3 newScale = transform.localScale;
			newScale.x = -Mathf.Abs(newScale.x);
			transform.localScale = newScale;
		}
	}

	void MoveTowardsPlayer(float moveSpeed)
	{
		transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
	}

	public void OnHit()
	{
		animator.SetBool("isAlive", false);
	}
}
