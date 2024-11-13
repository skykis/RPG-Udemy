using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player,
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

        if (Player.IsGroundDetected())
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StateMachine.ChangeState(Player.JumpState);
            }
        }
        else
        {
            StateMachine.ChangeState(Player.AirState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}