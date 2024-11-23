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
    }
}