using UnityEngine;

namespace Player
{
    public class PlayerCounterAttackState : PlayerState
    {
        private static readonly int SuccessfulCounterAttack = Animator.StringToHash("SuccessfulCounterAttack");

        public PlayerCounterAttackState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            StateTimer = Player.counterAttackDuration;
            
            Player.Anim.SetBool(SuccessfulCounterAttack, false);
        }

        public override void Update()
        {
            base.Update();
            
            var colliders = Physics2D.OverlapCircleAll(Player.attackCheck.position, Player.attackCheckRadius);

            foreach (var hit in colliders)
            {
                var enemy = hit.GetComponent<Enemy.Enemy>();
                if (enemy)
                {
                    if (enemy.CanBeStunned())
                    {
                        StateTimer = 10;
                        Player.Anim.SetBool(SuccessfulCounterAttack, true);
                    }
                }
            }

            if (StateTimer < 0 || TriggerCalled)
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
