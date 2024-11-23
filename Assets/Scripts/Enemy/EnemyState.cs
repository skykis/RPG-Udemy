using UnityEngine;

namespace Enemy
{
    public class EnemyState
    {
        protected EnemyStateMachine StateMachine;
        protected Enemy EnemyBase;
        protected Rigidbody2D Rb;
        
        private readonly string animBoolName;
        
        protected float StateTimer;
        protected bool TriggerCalled;
        
        public EnemyState(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName)
        {
            StateMachine = stateMachine;
            EnemyBase = enemyBase;
            this.animBoolName = animBoolName;
        }

        public virtual void Enter()
        {
            TriggerCalled = false;
            Rb = EnemyBase.Rb;
            EnemyBase.Anim.SetBool(animBoolName, true);
        }

        public virtual void Update()
        {
            StateTimer -= Time.deltaTime;
        }

        public virtual void Exit()
        {
            EnemyBase.Anim.SetBool(animBoolName, false);
        }
        
        public virtual void AnimationFinishedTrigger()
        {
            TriggerCalled = true;
        }
    }
}
