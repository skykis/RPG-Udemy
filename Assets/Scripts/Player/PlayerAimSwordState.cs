using UnityEngine;

namespace Player
{
    public class PlayerAimSwordState : PlayerState
    {
        public PlayerAimSwordState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player,
            stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            Player.Skill.Sword.DotsActive(true);
        }

        public override void Update()
        {
            base.Update();

            Player.SetZeroVelocity();
            
            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                StateMachine.ChangeState(Player.IdleState);
            }

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Player.FlipController(mousePosition.x - Player.transform.position.x);
        }

        public override void Exit()
        {
            base.Exit();
            
            Player.StartCoroutine(nameof(global::Player.Player.BusyFor), 0.2f);
        }
    }
}