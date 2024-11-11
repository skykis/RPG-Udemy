using UnityEngine;

public class Player : MonoBehaviour
{
    #region Components
    public Animator anim { get; private set; }
    #endregion
    
    #region States
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    #endregion
    
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, "Idle");
        MoveState = new PlayerMoveState(this, StateMachine, "Move");
    }

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        StateMachine.CurrentState.Update();
    }
}