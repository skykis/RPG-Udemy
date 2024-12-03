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
            
            player = PlayerManager.instance.player.transform;
        }

        public override void Update()
        {
            base.Update();

            if (enemy.IsPlayerDetected())
            {
                StateTimer = enemy.battleTime;
                
                if (enemy.IsPlayerDetected().distance < enemy.attackDistance)
                {
                    if (CanAttack())
                    {
                        StateMachine.ChangeState(enemy.AttackState);
                    }
                }
            }
            else
            {
                if (StateTimer < 0 || Vector2.Distance(player.transform.position, enemy.transform.position) > 10)
                {
                    StateMachine.ChangeState(enemy.IdleState);
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

        private bool CanAttack()
        {
            if (Time.time >= enemy.lastTimeAttacked + enemy.attackCooldown)
            {
                enemy.lastTimeAttacked = Time.time;
                return true;
            }
            return false;
        }
    }
}
