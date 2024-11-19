using UnityEngine;

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

        if (!Player.IsWallDetected()) StateMachine.ChangeState(Player.Idle);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StateMachine.ChangeState(Player.WallJump);
            return;
        }

        if (XInput != 0 && !Mathf.Approximately(Player.FacingDirection, XInput)) StateMachine.ChangeState(Player.Idle);

        Rb.velocity = YInput < 0 ? new Vector2(0, Rb.velocity.y) : new Vector2(0, Rb.velocity.y * 0.7f);

        if (Player.IsGroundDetected()) StateMachine.ChangeState(Player.Idle);
    }

    public override void Exit()
    {
        base.Exit();
    }
}