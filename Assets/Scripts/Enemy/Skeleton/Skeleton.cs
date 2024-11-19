namespace Enemy.Skeleton
{
    public class Skeleton : Enemy
    {

        #region state

        public SkeletonIdleState Idle { get; private set; }
        public SkeletonMoveState Move { get; private set; }

        #endregion
        
        protected override void Awake()
        {
            base.Awake();
            
            Idle = new SkeletonIdleState(StateMachine, this, "Idle", this);
            Move = new SkeletonMoveState(StateMachine, this, "Move", this);
        }

        protected override void Start()
        {
            base.Start();
            
            StateMachine.Initialize(Idle);
        }

        protected override void Update()
        {
            base.Update();
        }
    }
}
