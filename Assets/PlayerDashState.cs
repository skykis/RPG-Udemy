public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        StateTimer = Player.dashDuration;
    }

    public override void Update()
    {
        base.Update();
        
        Player.SetVelocity(Player.dashSpeed * Player.dashDirection, 0);
        
        if (StateTimer < 0)
        {
            StateMachine.ChangeState(Player.IdleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        
        Player.SetVelocity(0, Rb.velocity.y);
    }
}
