using UnityEngine;

public class PlayerState
{
    private readonly string animBoolName;
    protected Player Player;
    protected PlayerStateMachine StateMachine;

    protected PlayerState(Player player, PlayerStateMachine stateMachine, string animBoolName)
    {
        Player = player;
        StateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        Debug.Log("I enter " + animBoolName);
    }

    public virtual void Update()
    {
        Debug.Log("I am in " + animBoolName);
    }

    public virtual void Exit()
    {
        Debug.Log("I exit " + animBoolName);
    }
}