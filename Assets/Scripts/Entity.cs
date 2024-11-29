using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region Components

    public Animator Anim { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    public EntityFx FX { get; private set; }
    #endregion
    
    [Header("Knockback info")]
    [SerializeField] protected Vector2 knockbackDirection;
    [SerializeField] protected float knockbackDuration;
    protected bool IsKnocked;
    
    
    [Header("Collision info")] 
    public Transform attackCheck;
    public float attackCheckRadius;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;

    public int FacingDirection { get; private set; } = 1;
    private bool facingRight = true;

    protected virtual void Awake()
    {
    }

    protected virtual void Start()
    {
        FX = GetComponentInChildren<EntityFx>();
        Anim = GetComponentInChildren<Animator>();
        Rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
    }

    public virtual void Damage()
    {
        FX.StartCoroutine("FlashFX");

        StartCoroutine("HitKnockback");
    }

    protected virtual IEnumerator HitKnockback()
    {
        IsKnocked = true;
        
        Rb.velocity = new Vector2(knockbackDirection.x * -FacingDirection, knockbackDirection.y);
        
        yield return new WaitForSeconds(knockbackDuration);
        
        IsKnocked = false;
    }
    
    #region Velocity

    public void SetZeroVelocity()
    {
        if (IsKnocked)
        {
            return;
        }

        Rb.velocity = new Vector2(0, 0);
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        if (IsKnocked)
        {
            return;
        }
        
        Rb.velocity = new Vector2(xVelocity, yVelocity);
        FlipController(xVelocity);
    }

    #endregion

    #region Collision

    public virtual bool IsGroundDetected() =>
        Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

    public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection,
        wallCheckDistance, whatIsGround);

    protected virtual void OnDrawGizmos()
    {
        var groundCheckPosition = groundCheck.position;
        Gizmos.DrawLine(groundCheckPosition,
            new Vector3(groundCheckPosition.x, groundCheckPosition.y - groundCheckDistance));

        var wallCheckPosition = wallCheck.position;
        Gizmos.DrawLine(wallCheckPosition, new Vector3(wallCheckPosition.x + wallCheckDistance, wallCheckPosition.y));

        var attackCheckPosition = attackCheck.position;
        Gizmos.DrawWireSphere(attackCheckPosition, attackCheckRadius);
    }

    #endregion

    #region Flip

    public virtual void Flip()
    {
        FacingDirection = FacingDirection * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    public virtual void FlipController(float x)
    {
        switch (x)
        {
            case > 0 when !facingRight:
            case < 0 when facingRight:
                Flip();
                break;
        }
    }

    #endregion
}