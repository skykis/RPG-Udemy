using System;
using UnityEngine;

public class SwordSkillController : MonoBehaviour
{
    private static readonly int Retation = Animator.StringToHash("Retation");
    private Animator animator;
    private Rigidbody2D rb;
    private CircleCollider2D cd;
    private Player.Player player;

    private bool canRotate = true;
    private bool isReturning;
    [SerializeField] private float returnSpeed = 12;
    
    
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

        animator.SetBool(Retation, true);
    }

    public void ReturnSword()
    {
        rb.isKinematic = false;
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
        
        animator.SetBool(Retation, false);
        
        canRotate = false;
        cd.enabled = false;
        
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        
        transform.parent = collision.transform;
    }
}
