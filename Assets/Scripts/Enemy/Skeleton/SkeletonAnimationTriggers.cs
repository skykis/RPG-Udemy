using UnityEngine;

namespace Enemy.Skeleton
{
    public class SkeletonAnimationTriggers : MonoBehaviour
    {
        private Skeleton Enemy => GetComponentInParent<Skeleton>();

        private void AnimationTrigger()
        {
            Enemy.AnimationTrigger();
        }

        private void AttackTrigger()
        {
            var colliders = Physics2D.OverlapCircleAll(Enemy.attackCheck.position, Enemy.attackCheckRadius);

            foreach (var hit in colliders)
            {
                var player = hit.GetComponent<Player.Player>();
                if (player)
                {
                    player.Damage();
                }
            }
        }

        private void OpenCounterWindow() => Enemy.OpenCounterAttackWindow();
        private void CloseCounterWindow() => Enemy.CloseCounterAttackWindow();
    }
}