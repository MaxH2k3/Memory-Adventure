using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEnemy : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
	private Collider2D col;
    private GameObject player;
	[SerializeField]
    public GameObject summonPrefab;
	[SerializeField]
	public float moveSpeed;
	[SerializeField]
	public float initialSummonTimer;
	public float summonTimer;
	public DetectionZone attackZone;
	public bool isFaceleft = true;


	private bool _hasTarget = false;
	public bool HasTarget
	{
		get
		{
			return _hasTarget;
		}
		set
		{
			_hasTarget = value;
			animator.SetBool("hasTarget", value);
		}
	}

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
	private float _maxHealth;
	public float MaxHealth
	{
		get
		{
			return _maxHealth;
		}
		set
		{
			_maxHealth = value;
		}
	}

	[SerializeField]
	private float _health;
	public float Health
	{
		get
		{
			return _health;
		}
		set
		{
			_health = value;
			if (_health <= 0)
			{
				IsAlive = false;
				col.enabled = false;
			}
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

	[SerializeField]
	private bool isInvincible = false;
	private float timeSinceHit;
	public float invincibilityTime = 0.25f;
	public float hitProbability = 100f;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		player = GameObject.FindGameObjectWithTag("Player");
		animator = GetComponent<Animator>();
		col = GetComponent<Collider2D>();
	}

    void Update()
    {
		HasTarget = attackZone.detectedCollider.Count > 0;
		if (CanMove)
		{
			flip();
			MoveTowardsPlayer(moveSpeed);
		}
		
		if (isInvincible)
		{
			if (timeSinceHit > invincibilityTime)
			{
				isInvincible = false;
				timeSinceHit = 0;
			}

			timeSinceHit += Time.deltaTime;
		}



	}

	private void FixedUpdate()
	{
		summonTimer -= Time.deltaTime;
		if (summonTimer <= 0 && CanMove)
		{
			StartCoroutine(Summon());
			summonTimer = initialSummonTimer;
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
		//Vector2 direction = (player.transform.position - transform.position).normalized;
		float distance = Vector2.Distance(transform.position, player.transform.position);
		if (distance > 2)
		{
			transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
		}
	}

	public IEnumerator Summon()
	{
		animator.SetTrigger("summon");
		yield return new WaitForSeconds(1);
		var spawnPosition1 = new Vector2(gameObject.transform.position.x + 3f * gameObject.transform.localScale.x, gameObject.transform.position.y + 1.5f);
		var spawnPosition2 = new Vector2(gameObject.transform.position.x + 3f * gameObject.transform.localScale.x, gameObject.transform.position.y + 0f);
		var spawnPosition3 = new Vector2(gameObject.transform.position.x + 3f * gameObject.transform.localScale.x, gameObject.transform.position.y + -1.5f);
		Instantiate(summonPrefab, spawnPosition1, gameObject.transform.rotation);
		Instantiate(summonPrefab, spawnPosition2, gameObject.transform.rotation);
		Instantiate(summonPrefab, spawnPosition3, gameObject.transform.rotation);
	}

	public void OnHit(float damage)
	{
		if (!isInvincible)
		{
			if (UnityEngine.Random.value < (hitProbability / 100f))
			{
				animator.SetTrigger("hit");
			}
			isInvincible = true;
			Health -= damage;
            //CharacterEvents.characterDamaged.Invoke(gameObject, damage);
            //PlayerHealth.TakeDamage(attackDamage, PlayerController.Instance.transform);
        }
	}

}
