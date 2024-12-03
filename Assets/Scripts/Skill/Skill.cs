using UnityEngine;

namespace Skill
{
    public class Skill : MonoBehaviour
    {
        [SerializeField] protected float cooldown;
        protected float CooldownTimer;

        protected virtual void Update()
        {
            CooldownTimer -= Time.deltaTime;
        }

        public virtual bool CanUseSkill()
        {
            return CooldownTimer < 0;
        }

        public virtual void UseSkill()
        {
            if (CanUseSkill())
            {
                CooldownTimer = cooldown;
            }
        }
    }
}
