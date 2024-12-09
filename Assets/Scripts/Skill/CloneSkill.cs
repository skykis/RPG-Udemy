using Skill.SkillController;
using UnityEngine;

namespace Skill
{
    public class CloneSkill : Skill
    {
        [Header("Clone info")] [SerializeField]
        private GameObject clonePrefab;
        [SerializeField] private float clonedDuration;
        [Space]
        [SerializeField] private bool canAttack;


        public void CreateClone(Transform clonePosition)
        {
            var newClone = Instantiate(clonePrefab);

            newClone.GetComponent<CloneSkillController>().SetupClone(clonePosition, clonedDuration, canAttack);
        }
    }
}