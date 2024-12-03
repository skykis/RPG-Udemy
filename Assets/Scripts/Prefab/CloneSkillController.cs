using UnityEngine;

namespace Prefab
{
    public class CloneSkillController : MonoBehaviour
    {
        private static readonly int AttackNumber = Animator.StringToHash("AttackNumber");
        private SpriteRenderer sr;
        private Animator anim;
        [SerializeField] private float colorLosingSpeed;
        private float cloneTimer;
        [SerializeField] private Transform attackCheck;
        [SerializeField] private float attackCheckRadius = 0.8f;
        private Transform closestEnemy;
        
        private void Awake()
        {       
            sr = GetComponent<SpriteRenderer>();
            anim = GetComponent<Animator>();
        }

        private void Update()
        {
            cloneTimer -= Time.deltaTime;

            if (cloneTimer < 0)
            {
                sr.color = new Color(1, 1, 1, sr.color.a - Time.deltaTime * colorLosingSpeed);

                if (sr.color.a <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }

        public void SetupClone(Transform newTransform, float cloneDuration, bool canAttack)
        {
            if (canAttack)
            {
                anim.SetInteger(AttackNumber, Random.Range(1, 4));
            }
            transform.position = newTransform.position;
            cloneTimer = cloneDuration;

            FaceClosestTarget();
        }
        
        private void AnimationTrigger()
        {
            cloneTimer = -0.1f;
        }

        private void AttackTrigger()
        {
            var colliders = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);

            foreach (var hit in colliders)
            {
                var enemy = hit.GetComponent<Enemy.Enemy>();
                if (enemy)
                {
                    enemy.Damage();
                }
            }
        }

        private void FaceClosestTarget()
        {
            var colliders = Physics2D.OverlapCircleAll(transform.position, 25);

            var closestDistance = Mathf.Infinity;

            foreach (var hit in colliders)
            {
                if (hit.GetComponent<Enemy.Enemy>() != null)
                {
                    var distanceToEnemy = Vector2.Distance(transform.position, hit.transform.position);

                    if (distanceToEnemy < closestDistance)
                    {
                        closestDistance = distanceToEnemy;
                        closestEnemy = hit.transform;
                    }
                }
            }

            if (closestEnemy != null)
            {
                if (transform.position.x > closestEnemy.position.x)
                {
                    transform.Rotate(0, 180, 0);
                }
            }
        }
    }
}