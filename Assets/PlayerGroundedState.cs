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

        if (Input.GetKey(KeyCode.Mouse0))
        {
            StateMachine.ChangeState(Player.PrimaryAttack);
        }
        
        if (Player.IsGroundDetected())
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StateMachine.ChangeState(Player.Jump);
            }
        }
        else
        {
            StateMachine.ChangeState(Player.Air);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}