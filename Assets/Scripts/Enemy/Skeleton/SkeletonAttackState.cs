using UnityEngine;

namespace Enemy.Skeleton
{
    public class SkeletonAttackState : EnemyState
    {
        private Skeleton enemy;

        public SkeletonAttackState(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName, Skeleton enemy) : base(stateMachine, enemyBase, animBoolName)
        {
            this.enemy = enemy;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
            
            enemy.SetZeroVelocity();

            if (TriggerCalled)
            {
                StateMachine.ChangeState(enemy.BattleState);
            }
        }

        public override void Exit()
        {
            base.Exit();

            enemy.lastTimeAttacked = Time.time;
        }
    }
}
