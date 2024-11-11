using UnityEngine;

public class PlayerState
{
    private readonly string animBoolName;
    protected readonly Player Player;
    protected readonly PlayerStateMachine StateMachine;

    protected float XInput;

    protected PlayerState(Player player, PlayerStateMachine stateMachine, string animBoolName)
    {
        Player = player;
        StateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        Player.anim.SetBool(animBoolName, true);
    }

    public virtual void Update()
    {
        XInput = Input.GetAxisRaw("Horizontal");
    }

    public virtual void Exit()
    {
        Player.anim.SetBool(animBoolName, false);
    }
}