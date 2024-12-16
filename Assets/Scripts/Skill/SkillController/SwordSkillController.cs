using System.Collections.Generic;
using UnityEngine;

namespace Skill.SkillController
{
    public class SwordSkillController : MonoBehaviour
    {
        private static readonly int Rotation = Animator.StringToHash("Rotation");
        private Animator animator;
        private Rigidbody2D rb;
        private CircleCollider2D cd;
        private Player.Player player;

        private bool canRotate = true;
        private bool isReturning;
        [SerializeField] private float returnSpeed;

        [Header("Bounce info")]
        [SerializeField] private float bounceSpeed;
        private bool isBouncing;
        private int amountOfBounce;
        private List<Transform> enemyTarget;
        private int targetIndex;
        
        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            rb = GetComponent<Rigidbody2D>();
            cd = GetComponent<CircleCollider2D>();
        }

        public void SetupSword(Vector2 direction, float gravityScale, Player.Player player)
        {
            rb.velocity = direction;
            rb.gravityScale = gravityScale;
            this.player = player;

            animator.SetBool(Rotation, true);
        }

        public void SetupBounce(bool isBouncing, int amountOfBounce)
        {
            this.isBouncing = isBouncing;
            this.amountOfBounce = amountOfBounce;
            
            enemyTarget = new List<Transform>();
        }
        
        public void ReturnSword()
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            // rb.isKinematic = false;
            transform.parent = null;
            isReturning = true;
        }
    
        private void Update()
        {
            if (canRotate)
            {
                transform.right = rb.velocity;
            }

            if (isReturning)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position,
                    returnSpeed * Time.deltaTime);

                if (Vector2.Distance(transform.position, player.transform.position) < 1)
                {
                    player.ClearTheSword();
                }
            }

            BounceLogic();
        }

        private void BounceLogic()
        {
            if (isBouncing && enemyTarget.Count > 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, enemyTarget[targetIndex].position,
                    bounceSpeed * Time.deltaTime);

                if (Vector2.Distance(transform.position, enemyTarget[targetIndex].position) < 0.1f)
                {
                    targetIndex++;
                    amountOfBounce--;

                    if (amountOfBounce <=0)
                    {
                        isBouncing = false;
                        isReturning = true;
                    }
                    
                    if (targetIndex >= enemyTarget.Count)
                    {
                        targetIndex = 0;
                    }
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (isReturning)
            {
                return;
            }

            if (collision.GetComponent<Enemy.Enemy>() != null)
            {
                if (isBouncing && enemyTarget.Count <= 0)
                {
                    var circleAll = Physics2D.OverlapCircleAll(transform.position, 10);

                    foreach (var hit in circleAll)
                    {
                        if (hit.GetComponent<Enemy.Enemy>() != null)
                        {
                            enemyTarget.Add(hit.transform);
                        }
                    }
                }
            }
            
            StuckInto(collision);
        }

        private void StuckInto(Collider2D collision)
        {
            canRotate = false;
            cd.enabled = false;
        
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;

            if (isBouncing && enemyTarget.Count > 1)
            {
                return;
            }
            
            animator.SetBool(Rotation, false);
            transform.parent = collision.transform;
        }
    }
}
