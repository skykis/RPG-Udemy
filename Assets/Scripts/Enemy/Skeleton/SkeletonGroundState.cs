using UnityEngine;

namespace Enemy.Skeleton
{
    public class SkeletonGroundState : EnemyState
    {
        protected Skeleton Enemy;
        private Transform player;
        
        public SkeletonGroundState(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName, Skeleton enemy) : base(stateMachine, enemyBase, animBoolName)
        {
            Enemy = enemy;
        }

        public override void Enter()
        {
            base.Enter();
            
            player = PlayerManager.instance.player.transform;
        }

        public override void Update()
        {
            base.Update();

            if (Enemy.IsPlayerDetected() || Vector2.Distance(Enemy.transform.position, player.transform.position) < 2)
            {
                StateMachine.ChangeState(Enemy.BattleState);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
