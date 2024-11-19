using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player Player => GetComponentInParent<Player>();

    private void AnimationTrigger()
    {
        Player.AnimationTrigger();
    }
}