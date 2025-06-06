using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateManager : MonoBehaviour
{
    public float jumpForce = 20.3f;
    public float dashForce = 12f;
    public float airSpeed = 6f;
    public float groundSpeed = 6f;
    public float dashCooldown = 1f; 
    private float lastDashTime = -Mathf.Infinity;
    private bool hasDashedInAir = false;
    public bool canDoubleJump = false;
    public bool hasDoubleJumped = false;
    public bool isControlInverted = false;
    
    public State groundState {get; private set;}
    public State crouchState {get; private set;}
    public State jumpState {get; private set;}
    private State currentState;

    public Rigidbody2D rigidbody2D {get; private set;}
    public Collider2D collider2D {get; private set;}
    public PlayerInput playerInput {get; private set;}
    public Animator animator {get; private set;}
    public Transform transform {get; private set;}
    
    public PlayerWeaponManager playerWeaponManager {get; private set;}

    public Collider2D groundCollider;
    public float move {get; set;}
    public float targetXaxis {get; set;}
    public float targetYaxis {get; set;}
    public bool isGrounded {get; set;}

    public bool isDashing {get; set;}
    public bool isShooting {get; set;}
    
    public enum Direction
    {
        Left = -1,
        Right = 1
    }
    public Direction direction {get; set;}

   
    
    void Awake()
    {
        groundState = new PlayerGroundState(this);
        crouchState = new PlayerCrouchState(this);
        jumpState = new PlayerJumpState(this);
        
        rigidbody2D = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        transform = GetComponent<Transform>();
        playerWeaponManager = GetComponentInChildren<PlayerWeaponManager>();
        playerInput = GetComponent<PlayerInput>();
        // animator.SetInteger("AttackType",0);
        animator.SetInteger("AttackType",PlayerPrefs.GetInt("SelectedWeaponIndex"));
        
    }

    void Start()
    {
        move = 0;
        targetXaxis = 0;
        targetYaxis = 0;

        isGrounded = true;
        isDashing = false;
        isShooting = false;
        if (PerkManager.Instance != null)
        {
            groundSpeed *= PerkManager.Instance.speedMultiplier;
            airSpeed *= PerkManager.Instance.speedMultiplier;
            canDoubleJump = PerkManager.Instance.doubleJumpUnlocked;
        }
        
        direction = Direction.Right;
        currentState = groundState; 
        currentState.Enter();
        Init();
        //playerInput.DeactivateInput();
    }


    void Update()
    {
        currentState.UpdateState();
        Shoot();
    }
   

    
    public void Init(){
        playerInput.ActivateInput();
    }
    public void ChangeState(State newState){
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void FallOneWayPlatform(Collider2D platformCollider)
    {
        StartCoroutine(StartFallOneWayPlatform(platformCollider));
    }

    private IEnumerator StartFallOneWayPlatform(Collider2D platformCollider)
    {
        Physics2D.IgnoreCollision(this.groundCollider, platformCollider, true);
        yield return new WaitForSeconds(0.3f); 
        Physics2D.IgnoreCollision(this.groundCollider, platformCollider, false);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f) 
                {
                    isGrounded = true;
                    hasDashedInAir = false;
                    hasDoubleJumped = false;
                    return;
                }
            }
        }
    }

    
    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();

        if (isControlInverted)
        {
            input.x *= -1;
            input.y *= -1;
        }

        move = input.x;
        targetXaxis = input.x;
        targetYaxis = input.y;

        animator.SetFloat("speed", Mathf.Abs(move));
        animator.SetFloat("targetYaxis", targetYaxis);

        Flip(move);
    }
    public void OnJump(){
        if (isControlInverted)
            PerformDash();
        else
            PerformJump();
    }

    public void OnDash(){
        if (isControlInverted)
            PerformJump();
        else
            PerformDash();
    }
    private void PerformJump()
    {
        currentState.OnJump();
    }

    private void PerformDash()
    {
        if (Time.time < lastDashTime + dashCooldown || isDashing)
            return;
        if (currentState == crouchState)
            return;
        if (isGrounded)
        {
            StartCoroutine(Dash());
            hasDashedInAir = false;
            lastDashTime = Time.time;
        }
        else if (!hasDashedInAir)
        {
            StartCoroutine(Dash());
            hasDashedInAir = true;
            lastDashTime = Time.time;
        }
    }

    public void OnShoot(InputValue value){
        isShooting = value.isPressed;
    }

    public void Shoot()
    {
        if (isShooting && !isDashing && playerWeaponManager != null)
        {
            playerWeaponManager.Shoot(currentState.getTargetRotation());
        }
        else
        {
            
            playerWeaponManager.StopShooting();
        }
    }

    public IEnumerator Dash()
    {
        float gravity_scale = rigidbody2D.gravityScale;
        animator.SetTrigger("dash");
        isDashing = true;
        rigidbody2D.gravityScale = 0;
        rigidbody2D.linearVelocity = new Vector2(dashForce * (int) direction, 0);
        yield return new WaitForSeconds(0.2f);
        isDashing = false;
        rigidbody2D.gravityScale = gravity_scale;
    }

    void Flip(float horizontal)
    {
        if(isDashing){
            return;
        }

        if(horizontal > 0){
            direction = PlayerStateManager.Direction.Right;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        } else if(horizontal < 0){
            direction = PlayerStateManager.Direction.Left;
            transform.rotation = Quaternion.Euler(0, -180, 0);
        } else {
        }
    }
    


}
