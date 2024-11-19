using UnityEngine;

namespace Player
{
    public class PlayerPrimaryAttackState : PlayerState
    {
        private static readonly int ComboCounter = Animator.StringToHash("ComboCounter");

        private int comboCounter;
        private readonly float comboWindow = 2;
        private float lastTimeAttacked;

        public PlayerPrimaryAttackState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player,
            stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            if (CheckComboWindow()) comboCounter = 0;

            Player.Anim.SetInteger(ComboCounter, comboCounter);

            float attackDirection = Player.FacingDirection;
            if (XInput != 0) attackDirection = XInput;

            Player.SetVelocity(Player.attackMovement[comboCounter].x * attackDirection,
                Player.attackMovement[comboCounter].y);

            StateTimer = 0.1f;
        }

        public override void Update()
        {
            base.Update();

            if (StateTimer < 0) Player.SetZeroVelocity();

            if (TriggerCalled) StateMachine.ChangeState(Player.Idle);
        }

        public override void Exit()
        {
            base.Exit();

            Player.StartCoroutine(nameof(global::Player.Player.BusyFor), 0.2f);

            comboCounter++;
            lastTimeAttacked = Time.time;
        }

        private bool CheckComboWindow()
        {
            return comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow;
        }
    }
}