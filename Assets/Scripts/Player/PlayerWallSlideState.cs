using UnityEngine;

namespace Player
{
    public class PlayerWallSlideState : PlayerState
    {
        public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player,
            stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();

            if (!Player.IsWallDetected()) StateMachine.ChangeState(Player.IdleState);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                StateMachine.ChangeState(Player.WallJumpState);
                return;
            }

            if (XInput != 0 && !Mathf.Approximately(Player.FacingDirection, XInput)) StateMachine.ChangeState(Player.IdleState);

            Rb.velocity = YInput < 0 ? new Vector2(0, Rb.velocity.y) : new Vector2(0, Rb.velocity.y * 0.7f);

            if (Player.IsGroundDetected()) StateMachine.ChangeState(Player.IdleState);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}