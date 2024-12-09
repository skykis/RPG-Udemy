using UnityEngine;

namespace Player
{
    public class PlayerCatchSwordState : PlayerState
    {
        private Transform sword;
        
        public PlayerCatchSwordState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player,
            stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            sword = Player.Sword.transform;
            
            Player.FlipController(sword.position.x - Player.transform.position.x);
            
            Rb.velocity = new Vector2(Player.swordReturnImpact * -Player.FacingDirection, Rb.velocity.y);
        }

        public override void Update()
        {
            base.Update();

            if (TriggerCalled)
            {
                StateMachine.ChangeState(Player.IdleState);
            }
        }

        public override void Exit()
        {
            base.Exit();
            
            Player.StartCoroutine(nameof(global::Player.Player.BusyFor), 0.1f);
        }
    }
}