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
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (isReturning)
            {
                return;
            }
            
            animator.SetBool(Rotation, false);
        
            canRotate = false;
            cd.enabled = false;
        
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        
            transform.parent = collision.transform;
        }
    }
}
