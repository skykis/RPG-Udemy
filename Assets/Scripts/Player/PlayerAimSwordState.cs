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

            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                StateMachine.ChangeState(Player.IdleState);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}