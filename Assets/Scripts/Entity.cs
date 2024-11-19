using UnityEngine;

public class Entity : MonoBehaviour
{
    #region Components

    public Animator Anim { get; private set; }
    public Rigidbody2D Rb { get; private set; }

    #endregion
    
    [Header("Collision info")] 
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
        Anim = GetComponentInChildren<Animator>();
        Rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
    }

    #region Velocity

    public void SetZeroVelocity() => Rb.velocity = new Vector2(0, 0);

    public void SetVelocity(float xVelocity, float yVelocity)
    {
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
    }

    #endregion

    #region Flip

    private void Flip()
    {
        FacingDirection = FacingDirection * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void FlipController(float x)
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