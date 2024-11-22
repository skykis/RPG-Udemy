namespace Enemy.Skeleton
{
    public class SkeletonMoveState : SkeletonGroundState
    {
        public SkeletonMoveState(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName, Skeleton enemy) : base(stateMachine, enemyBase, animBoolName, enemy)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();

            Enemy.SetVelocity(Enemy.moveSpeed * Enemy.FacingDirection, Rb.velocity.y);

            if (Enemy.IsWallDetected() || !Enemy.IsGroundDetected())
            {
                Enemy.Flip();

                StateMachine.ChangeState(Enemy.IdleState);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
