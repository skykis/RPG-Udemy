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
    }
}