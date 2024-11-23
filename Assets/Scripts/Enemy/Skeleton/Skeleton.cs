namespace Enemy.Skeleton
{
    public class Skeleton : Enemy
    {

        #region state

        public SkeletonIdleState IdleState { get; private set; }
        public SkeletonMoveState MoveState { get; private set; }
        public SkeletonBattleState BattleState { get; private set; }
        public SkeletonAttackState AttackState { get; private set; }
        #endregion
        
        protected override void Awake()
        {
            base.Awake();
            
            IdleState = new SkeletonIdleState(StateMachine, this, "Idle", this);
            MoveState = new SkeletonMoveState(StateMachine, this, "Move", this);
            BattleState = new SkeletonBattleState(StateMachine, this, "Move", this);
            AttackState = new SkeletonAttackState(StateMachine, this, "Attack", this);
        }

        protected override void Start()
        {
            base.Start();
            
            StateMachine.Initialize(IdleState);
        }

        protected override void Update()
        {
            base.Update();
        }
    }
}
