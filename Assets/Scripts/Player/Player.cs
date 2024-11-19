using System.Collections;
using UnityEngine;

namespace Player
{
    public class Player : Entity
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

        #region States

        private PlayerStateMachine StateMachine { get; set; }
        public PlayerIdleState Idle { get; private set; }
        public PlayerMoveState Move { get; private set; }
        public PlayerJumpState Jump { get; private set; }
        public PlayerAirState Air { get; private set; }
        public PlayerDashState Dash { get; private set; }
        public PlayerWallSlideState WallSlide { get; private set; }
        public PlayerWallJumpState WallJump { get; private set; }
        public PlayerPrimaryAttackState PrimaryAttack { get; private set; }

        #endregion

        protected override void Awake()
        {
            base.Awake();
            
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

        protected override void Start()
        {
            base.Start();
            
            StateMachine.Initialize(Idle);
        }

        protected override void Update()
        {
            base.Update();
            
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