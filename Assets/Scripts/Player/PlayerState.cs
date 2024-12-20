using UnityEngine;

namespace Player
{
    public class PlayerState
    {
        private static readonly int YVelocity = Animator.StringToHash("YVelocity");

        private readonly string animBoolName;
        protected readonly Player Player;
        protected readonly PlayerStateMachine StateMachine;

        protected Rigidbody2D Rb;
        protected float StateTimer;
        protected bool TriggerCalled;
        protected float XInput;
        protected float YInput;

        public PlayerState(Player player, PlayerStateMachine stateMachine, string animBoolName)
        {
            Player = player;
            StateMachine = stateMachine;
            this.animBoolName = animBoolName;
        }

        public virtual void Enter()
        {
            Player.Anim.SetBool(animBoolName, true);
            Rb = Player.Rb;
            TriggerCalled = false;
        }

        public virtual void Update()
        {
            StateTimer -= Time.deltaTime;
            XInput = Input.GetAxisRaw("Horizontal");
            YInput = Input.GetAxisRaw("Vertical");

            Player.Anim.SetFloat(YVelocity, Rb.velocity.y);
        }

        public virtual void Exit()
        {
            Player.Anim.SetBool(animBoolName, false);
        }

        public virtual void AnimationFinishedTrigger()
        {
            TriggerCalled = true;
        }
    }
}