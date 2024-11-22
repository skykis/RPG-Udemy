namespace Player
{
    public class PlayerAirState : PlayerState
    {
        public PlayerAirState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player,
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
        
            if (Player.IsWallDetected()) StateMachine.ChangeState(Player.WallSlideState);
            if (Player.IsGroundDetected()) StateMachine.ChangeState(Player.IdleState);
            if (XInput != 0) Player.SetVelocity(Player.moveSpeed * 0.8f * XInput, Rb.velocity.y);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}