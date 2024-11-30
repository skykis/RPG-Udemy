using UnityEngine;

namespace Enemy.Skeleton
{
    public class SkeletonStunnedState : EnemyState
    {
        private Skeleton enemy;

        public SkeletonStunnedState(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName, Skeleton enemy) : base(stateMachine, enemyBase, animBoolName)
        {
            this.enemy = enemy;
        }

        public override void Enter()
        {
            base.Enter();

            enemy.FX.InvokeRepeating("RedColorBlink", 0, 0.1f);

            StateTimer = enemy.stunDuration;

            Rb.velocity = new Vector2(-enemy.FacingDirection * enemy.stunDirection.x, enemy.stunDirection.y);
        }

        public override void Update()
        {
            base.Update();

            if (StateTimer < 0)
            {
                StateMachine.ChangeState(enemy.IdleState);
            }
        }

        public override void Exit()
        {
            base.Exit();

            enemy.FX.Invoke("CancelRedColorBlink", 0);
        }
    }
}
