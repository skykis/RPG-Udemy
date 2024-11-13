public class PlayerMoveState : PlayerGroundedState
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
        
        Player.SetVelocity(XInput * Player.moveSpeed, Rb.velocity.y);

        //  Player.IsWallDetected() 暂时不加，允许顶墙跑动
        if (XInput == 0)
            StateMachine.ChangeState(Player.IdleState);
    }

    public override void Exit()
    {
        base.Exit();
    }
}