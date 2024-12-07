using UnityEngine;

namespace Player
{
    public class PlayerAnimationTriggers : MonoBehaviour
    {
        private Player Player => GetComponentInParent<Player>();

        private void AnimationTrigger()
        {
            Player.AnimationTrigger();
        }

        private void AttackTrigger()
        {
            var colliders = Physics2D.OverlapCircleAll(Player.attackCheck.position, Player.attackCheckRadius);

            foreach (var hit in colliders)
            {
                var enemy = hit.GetComponent<Enemy.Enemy>();
                if (enemy)
                {
                    enemy.Damage();
                }
            }
        }

        private void ThrowSword()
        {
            SkillManager.instance.Sword.CreateSword();
        }
    }
}