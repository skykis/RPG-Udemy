using UnityEngine;

namespace Enemy
{
    public class Enemy : Entity
    {
        [SerializeField] protected LayerMask whatIsPlayer;
        [Header("Move info")] public float moveSpeed;
        public float idleTime;
        [Header("Attack info")] public float attackDistance;

        #region States

        public EnemyStateMachine StateMachine { get; private set; }

        #endregion

        protected override void Awake()
        {
            base.Awake();
            StateMachine = new EnemyStateMachine();
        }

        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            base.Update();

            StateMachine.CurrentState.Update();
        }

        public virtual RaycastHit2D IsPlayerDetected() =>
            Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, 50, whatIsPlayer);

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position,
                new Vector3(transform.position.x + attackDistance * FacingDirection, transform.position.y));
        }
    }
}