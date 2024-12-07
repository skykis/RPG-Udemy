using System.Collections;
using UnityEngine;

namespace Player
{
    public class Player : Entity
    {
        [Header("Attack detail")] 
        public Vector2[] attackMovement;
        public float counterAttackDuration = 0.2f;
        
        public bool IsBusy { get; private set; }
        [Header("Move info")] 
        public float moveSpeed;
        public float jumpForce;
        [Header("Dash info")] 
        public float dashSpeed;
        public float dashDuration;
        public float dashDirection;

        
        public SkillManager Skill {get; private set;}
        public GameObject Sword; //{get; private set;}
        
        #region States

        private PlayerStateMachine StateMachine { get; set; }
        public PlayerIdleState IdleState { get; private set; }
        public PlayerMoveState MoveState { get; private set; }
        public PlayerJumpState JumpState { get; private set; }
        public PlayerAirState AirState { get; private set; }
        public PlayerDashState DashState { get; private set; }
        public PlayerWallSlideState WallSlideState { get; private set; }
        public PlayerWallJumpState WallJumpState { get; private set; }
        public PlayerPrimaryAttackState PrimaryAttackState { get; private set; }
        public PlayerCounterAttackState CounterAttackState { get; private set; }
        public PlayerAimSwordState AimSwordState { get; private set; }
        public PlayerCatchSwordState CatchSwordState { get; private set; }

        #endregion

        protected override void Awake()
        {
            base.Awake();
            
            StateMachine = new PlayerStateMachine();

            IdleState = new PlayerIdleState(this, StateMachine, "Idle");
            MoveState = new PlayerMoveState(this, StateMachine, "Move");
            JumpState = new PlayerJumpState(this, StateMachine, "Jump");
            AirState = new PlayerAirState(this, StateMachine, "Jump");
            DashState = new PlayerDashState(this, StateMachine, "Dash");
            WallSlideState = new PlayerWallSlideState(this, StateMachine, "WallSlide");
            WallJumpState = new PlayerWallJumpState(this, StateMachine, "Jump");
            
            PrimaryAttackState = new PlayerPrimaryAttackState(this, StateMachine, "Attack");
            CounterAttackState = new PlayerCounterAttackState(this, StateMachine, "CounterAttack");
            
            AimSwordState = new PlayerAimSwordState(this, StateMachine, "AimSword");
            CatchSwordState = new PlayerCatchSwordState(this, StateMachine, "CatchSword");
        }

        protected override void Start()
        {
            base.Start();

            Skill = SkillManager.instance;
            
            StateMachine.Initialize(IdleState);
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

            if (Input.GetKeyDown(KeyCode.LeftShift) && SkillManager.instance.Dash.CanUseSkill())
            {
                dashDirection = Input.GetAxisRaw("Horizontal");
                if (dashDirection == 0)
                {
                    dashDirection = FacingDirection;
                }

                StateMachine.ChangeState(DashState);
            }
        }

        public void AssignNewSword(GameObject newSword)
        {
            Sword = newSword;
        }

        public void ClearTheSword()
        {
            Destroy(Sword);
        }
    }
}