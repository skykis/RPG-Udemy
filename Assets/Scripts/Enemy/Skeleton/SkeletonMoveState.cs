namespace Enemy.Skeleton
{
    public class SkeletonMoveState : EnemyState
    {
        private Skeleton enemy;

        public SkeletonMoveState(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName, Skeleton enemy) : base(stateMachine, enemyBase, animBoolName)
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

            enemy.SetVelocity(enemy.moveSpeed * enemy.FacingDirection, enemy.Rb.velocity.y);

            if (enemy.IsWallDetected() || !enemy.IsGroundDetected())
            {
                enemy.Flip();

                StateMachine.ChangeState(enemy.Idle);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
