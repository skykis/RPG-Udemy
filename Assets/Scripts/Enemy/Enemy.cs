using UnityEngine;

namespace Enemy
{
    public class Enemy : Entity
    {
        [Header("Move info")] 
        public float moveSpeed;
        public float idleTime;
        
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
    }
}
