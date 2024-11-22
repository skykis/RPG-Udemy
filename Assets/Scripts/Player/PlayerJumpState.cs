namespace Player
{
    public class PlayerJumpState : PlayerState
    {
        public PlayerJumpState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player,
            stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            Player.SetVelocity(Rb.velocity.x, Player.jumpForce);
        }

        public override void Update()
        {
            base.Update();

            if (Rb.velocity.y < 0) StateMachine.ChangeState(Player.AirState);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}