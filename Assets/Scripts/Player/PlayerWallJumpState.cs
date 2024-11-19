namespace Player
{
    public class PlayerWallJumpState : PlayerState
    {
        public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player,
            stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            StateTimer = 1f;
            Player.SetVelocity(5 * -Player.FacingDirection, Player.jumpForce);
        }

        public override void Update()
        {
            base.Update();
        
            if (Player.IsWallDetected()) StateMachine.ChangeState(Player.WallSlide);
        
            if (StateTimer < 0) StateMachine.ChangeState(Player.Air);

            if (Player.IsGroundDetected()) StateMachine.ChangeState(Player.Idle);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}