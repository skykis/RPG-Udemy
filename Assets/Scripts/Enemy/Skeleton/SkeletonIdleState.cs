namespace Enemy.Skeleton
{
    public class SkeletonIdleState : SkeletonGroundState
    {
        public SkeletonIdleState(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName, Skeleton enemy) : base(stateMachine, enemyBase, animBoolName, enemy)
        {
        }

        public override void Enter()
        {
            base.Enter();

            StateTimer = Enemy.idleTime;
        }

        public override void Update()
        {
            base.Update();

            if (StateTimer < 0)
            {
                StateMachine.ChangeState(Enemy.MoveState);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
