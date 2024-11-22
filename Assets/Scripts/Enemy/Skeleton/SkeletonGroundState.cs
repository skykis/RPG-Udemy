namespace Enemy.Skeleton
{
    public class SkeletonGroundState : EnemyState
    {
        protected Skeleton Enemy;

        public SkeletonGroundState(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName, Skeleton enemy) : base(stateMachine, enemyBase, animBoolName)
        {
            Enemy = enemy;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();

            if (Enemy.IsPlayerDetected())
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
