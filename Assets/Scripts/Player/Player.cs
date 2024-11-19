using System.Collections;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        [Header("Attack detail")] 
        public Vector2[] attackMovement;
        public bool IsBusy { get; private set; }
        [Header("Move info")] 
        public float moveSpeed;
        public float jumpForce;
        [Header("Dash info")] 
        [SerializeField] private float dashCooldown;
        private float dashUsageTimer;
        public float dashSpeed;
        public float dashDuration;
        public float dashDirection;

        [Header("Collision info")] [SerializeField]
        private Transform groundCheck;

        [SerializeField] private float groundCheckDistance;
        [SerializeField] private Transform wallCheck;
        [SerializeField] private float wallCheckDistance;
        [SerializeField] private LayerMask whatIsGround;

        public int FacingDirection { get; private set; } = 1;
        private bool facingRight = true;

        #region Components

        public Animator Anim { get; private set; }
        public Rigidbody2D Rb { get; private set; }

        #endregion

        #region States

        public PlayerStateMachine StateMachine { get; private set; }
        public PlayerIdleState Idle { get; private set; }
        public PlayerMoveState Move { get; private set; }
        public PlayerJumpState Jump { get; private set; }
        public PlayerAirState Air { get; private set; }
        public PlayerDashState Dash { get; private set; }
        public PlayerWallSlideState WallSlide { get; private set; }
        public PlayerWallJumpState WallJump { get; private set; }
        public PlayerPrimaryAttackState PrimaryAttack { get; private set; }

        #endregion

        private void Awake()
        {
            StateMachine = new PlayerStateMachine();

            Idle = new PlayerIdleState(this, StateMachine, "Idle");
            Move = new PlayerMoveState(this, StateMachine, "Move");
            Jump = new PlayerJumpState(this, StateMachine, "Jump");
            Air = new PlayerAirState(this, StateMachine, "Jump");
            Dash = new PlayerDashState(this, StateMachine, "Dash");
            WallSlide = new PlayerWallSlideState(this, StateMachine, "WallSlide");
            WallJump = new PlayerWallJumpState(this, StateMachine, "Jump");
            PrimaryAttack = new PlayerPrimaryAttackState(this, StateMachine, "Attack");
        }

        private void Start()
        {
            Anim = GetComponentInChildren<Animator>();
            Rb = GetComponent<Rigidbody2D>();

            StateMachine.Initialize(Idle);
        }

        private void Update()
        {
            StateMachine.CurrentState.Update();

            CheckForDashInput();
        }

        public IEnumerator BusyFor(float seconds)
        {
            IsBusy = true;
            yield return new WaitForSeconds(seconds);
            IsBusy = false;
        }
        public void AnimationTrigger() => StateMachine.CurrentState.AnimationFinishedTrigger();
    
        #region Velocity

        public void SetZeroVelocity() => Rb.velocity = new Vector2(0, 0);

        public void SetVelocity(float xVelocity, float yVelocity)
        {
            Rb.velocity = new Vector2(xVelocity, yVelocity);
            FlipController(xVelocity);
        }

        #endregion
    
        #region Collision

        public bool IsGroundDetected() =>
            Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

        public bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection,
            wallCheckDistance, whatIsGround);

        private void OnDrawGizmos()
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
    

        private void CheckForDashInput()
        {
            if (!IsGroundDetected() && IsWallDetected())
            {
                return;
            }

            dashUsageTimer -= Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.LeftShift) && dashUsageTimer < 0)
            {
                dashUsageTimer = dashCooldown;
                dashDirection = Input.GetAxisRaw("Horizontal");
                if (dashDirection == 0)
                {
                    dashDirection = FacingDirection;
                }

                StateMachine.ChangeState(Dash);
            }
        }
    }
}