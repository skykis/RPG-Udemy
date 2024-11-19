namespace Enemy.Skeleton
{
    public class SkeletonIdleState : EnemyState
    {
        private Skeleton enemy;

        public SkeletonIdleState(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName, Skeleton enemy) : base(stateMachine, enemyBase, animBoolName)
        {
            this.enemy = enemy;
        }

        public override void Enter()
        {
            base.Enter();

            StateTimer = enemy.idleTime;
        }

        public override void Update()
        {
            base.Update();

            if (StateTimer < 0)
            {
                StateMachine.ChangeState(enemy.Move);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
