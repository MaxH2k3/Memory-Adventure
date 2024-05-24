using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float boostSpeed = 1.7f;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private Transform weaponCollider; // This is the collider of the weapon
    [SerializeField] public PlayerState playerState;

    private bool isBoosting = false;
    public bool FacingLeft { get; private set; } = false;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private KnockBack knockBack;
    private Animator playerAnimator;
    private SpriteRenderer playerSprite;

    private CompanisionAI companionAI;
    private CompanisionHealth companionHealth;
    [SerializeField] public List<CompanisionAI> list = new List<CompanisionAI>();

    protected override void Awake()
    {
        base.Awake();
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerSprite = GetComponent <SpriteRenderer>();
        knockBack = GetComponent<KnockBack>();

        companionAI = FindObjectOfType<CompanisionAI>();
        companionHealth = FindObjectOfType<CompanisionHealth>();
        list = new List<CompanisionAI>(FindObjectsOfType<CompanisionAI>());
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Start()
    {
        moveSpeed = playerState.moveSpeed;


        playerControls.Action.Dash.performed += _ => Dash();
        ActiveBag.Instance.EquipStartingWeapon();
    }

    private void Update()
    {
        StatePlayer();
        StateDirection();

        
    }

    private void FixedUpdate()
    {
        FlipSprite();
        Move();
    }

    // Getter & Setter

    public Transform GetWeaponCollider()
    {
        return weaponCollider;
    }

    // Method helper

    private void StatePlayer()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();

        if (playerControls.Movement.Behaviour.triggered)
        {
            isBoosting = true;
        }
        playerControls.Movement.Behaviour.canceled += _ => isBoosting = false;
        //companionHealth.DetectDeath();
        /*if (movement != Vector2.zero && !companionHealth.isDead)
        {

            companionAI.reachedEndOfPath = false;
            companionAI.animator.SetBool("walk", true);
        }*/
    }

    private void StateDirection()
    {
        playerAnimator.SetFloat("moveX", movement.x);
        playerAnimator.SetFloat("moveY", movement.y);
    }
    
    private void FlipSprite()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x > playerScreenPoint.x)
        {
            playerSprite.flipX = false;
            FacingLeft = false;
        } else if (mousePos.x < playerScreenPoint.x)
        {
            playerSprite.flipX = true;
            FacingLeft = true;
        }
    }

    private void Move()
    {
        if(knockBack.GettingKnockBack || PlayerHealth.Instance.isDead)
        {
            return;
        }

        float currentMoveSpeed = moveSpeed;

        if (isBoosting)
        {
            currentMoveSpeed *= boostSpeed;
        }

        rb.MovePosition(rb.position + currentMoveSpeed * Time.fixedDeltaTime * movement);

        //rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void Dash()
    {
        if(!isBoosting && Stamina.Instance.CurrentStamina > 0)
        {
            Stamina.Instance.UseStamina();

            // Tăng tốc độ di chuyển lên dashSpeed
            moveSpeed *= dashSpeed;

            // Bật trailRenderer để hiển thị hiệu ứng Dash
            trailRenderer.emitting = true;

            // Xử lý việc kết thúc Dash sau một khoảng thời gian nhất định.
            StartCoroutine(EndDashRoutine());
        }
        
    }

    private IEnumerator EndDashRoutine()
    {
        // Đặt thời gian Dash và thời gian chờ giữa các lần Dash
        float dashTime = .2f;
        float dashCD = .25f;

        // Chờ một khoảng thời gian dashTime
        yield return new WaitForSeconds(dashTime);
        moveSpeed /= dashSpeed;
        trailRenderer.emitting = false;

        // Chờ một khoảng thời gian dashCD trước khi cho phép Dash tiếp
        yield return new WaitForSeconds(dashCD);
        isBoosting = false;
    }

}
