namespace Player
{
    public class PlayerIdleState : PlayerGroundedState
    {
        public PlayerIdleState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player,
            stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            Player.SetVelocity(0, 0);
        }

        public override void Update()
        {
            base.Update();

            if (XInput != 0 && !Player.IsBusy) StateMachine.ChangeState(Player.Move);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}