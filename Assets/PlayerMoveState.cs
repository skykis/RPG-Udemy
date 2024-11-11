public class PlayerMoveState : PlayerState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player,
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

        Player.SetVelocity(XInput * Player.moveSpeed, Player.rb.velocity.y);

        if (XInput == 0) StateMachine.ChangeState(Player.IdleState);
    }

    public override void Exit()
    {
        base.Exit();
    }
}