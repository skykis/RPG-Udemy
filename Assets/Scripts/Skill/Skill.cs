using System;
using UnityEngine;

namespace Skill
{
    public class Skill : MonoBehaviour
    {
        [SerializeField] protected float cooldown;
        private float cooldownTimer;

        protected Player.Player Player;

        protected virtual void Start()
        {
            Player = PlayerManager.instance.player;
        }

        protected virtual void Update()
        {
            cooldownTimer -= Time.deltaTime;
        }

        public virtual bool CanUseSkill()
        {
            return cooldownTimer < 0;
        }

        public virtual void UseSkill()
        {
            if (CanUseSkill())
            {
                cooldownTimer = cooldown;
            }
        }
    }
}
