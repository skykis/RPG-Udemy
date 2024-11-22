using UnityEngine;

namespace Enemy.Skeleton
{
    public class SkeletonBattleState : EnemyState
    {
        private Transform player;
        private Skeleton enemy;
        private int moveDirection;

        public SkeletonBattleState(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName, Skeleton enemy) : base(stateMachine, enemyBase, animBoolName)
        {
            this.enemy = enemy;
        }

        public override void Enter()
        {
            base.Enter();
            
            player = GameObject.Find("Player").transform;
        }

        public override void Update()
        {
            base.Update();

            if (enemy.IsPlayerDetected())
            {
                if (enemy.IsPlayerDetected().distance < enemy.attackDistance)
                {
                    Debug.Log("Player detected");
                    enemy.SetZeroVelocity();
                    return;
                }
            }
            
            if (player.position.x > enemy.transform.position.x)
            {
                moveDirection = 1;
            }
            else if (player.position.x < enemy.transform.position.x)
            {
                moveDirection = -1;
            }

            enemy.SetVelocity(enemy.moveSpeed * moveDirection, Rb.velocity.y);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
