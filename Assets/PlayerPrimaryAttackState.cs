using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    private static readonly int ComboCounter = Animator.StringToHash("ComboCounter");

    private int comboCounter;
    private float lastTimeAttacked;
    private float comboWindow = 2;
    
    public PlayerPrimaryAttackState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if (CheckComboWindow())
        {
            comboCounter = 0;
        }

        Player.Anim.SetInteger(ComboCounter, comboCounter);

        Player.SetVelocity(Player.attackMovement[comboCounter].x * Player.FacingDirection,
            Player.attackMovement[comboCounter].y);

        StateTimer = 0.1f;
    }

    public override void Update()
    {
        base.Update();

        if (StateTimer < 0)
        {
            Player.SetZeroVelocity();
        }
        
        if (TriggerCalled)
        {
            StateMachine.ChangeState(Player.Idle);
        }
    }

    public override void Exit()
    {
        base.Exit();

        Player.StartCoroutine(nameof(global::Player.BusyFor), 0.2f);
        
        comboCounter++;
        lastTimeAttacked = Time.time;
    }

    private bool CheckComboWindow()
    {
        return comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow;
    }
}
