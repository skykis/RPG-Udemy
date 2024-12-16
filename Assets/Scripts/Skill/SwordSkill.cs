using Skill.SkillController;
using UnityEngine;

namespace Skill
{
    public enum SwordType
    {
        Regular,
        Bounce,
        Pierce,
        Spin
    }
    
    public class SwordSkill : Skill
    {
        public SwordType swordType = SwordType.Regular;
        
        [Header("Bounce info")]
        [SerializeField] private int amountOfBounces;
        [SerializeField] private float bounceGravity;
        
        [Header("Sword info")] 
        [SerializeField] private GameObject swordPrefab;
        [SerializeField] private Vector2 launchForce;
        [SerializeField] private float swordGravity;
        
        private Vector2 finalDirection;

        [Header("Aim dots")]
        [SerializeField] private int numberOfDots;
        [SerializeField] private float spaceBetweenDots;
        [SerializeField] private GameObject dotPrefab;
        [SerializeField] private Transform dotsParent;
        
        private GameObject[] dots;

        protected override void Start()
        {
            base.Start();
            
            GenerateDots();
        }

        protected override void Update()
        {
            base.Update();

            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                var aimDirection = AimDirection().normalized;
                finalDirection = new Vector2(aimDirection.x * launchForce.x, aimDirection.y * launchForce.y);
            }

            if (Input.GetKey(KeyCode.Mouse1))
            {
                for (int i = 0; i < dots.Length; i++)
                {
                    dots[i].transform.position = DotsPosition(i * spaceBetweenDots);
                }
            }
        }
        
        public void CreateSword()
        {
            var newSword = Instantiate(swordPrefab, Player.transform.position, transform.rotation);
            var newSwordScript = newSword.GetComponent<SwordSkillController>();

            if (swordType == SwordType.Bounce)
            {
                swordGravity = bounceGravity;
                newSwordScript.SetupBounce(true, amountOfBounces);
            }
            
            
            newSwordScript.SetupSword(finalDirection, swordGravity, Player);
            
            Player.AssignNewSword(newSword);
            
            DotsActive(false);
        }

        #region Aim reigin
        public Vector2 AimDirection()
        {
            Vector2 playerPosition = Player.transform.position;
            Vector2 mousePosition = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
            var direction = mousePosition - playerPosition;

            return direction;
        }

        public void DotsActive(bool isActive)
        {
            foreach (var t in dots)
            {
                t.SetActive(isActive);
            }
        }
        
        private void GenerateDots()
        {
            dots = new GameObject[numberOfDots];
            for (var i = 0; i < numberOfDots; i++)
            {
                dots[i] = Instantiate(dotPrefab, Player.transform.position, Quaternion.identity, dotsParent);
                dots[i].SetActive(false);
            }
        }

        private Vector2 DotsPosition(float t)
        {
            var aimDirection = AimDirection().normalized;
            var position = (Vector2)Player.transform.position + new Vector2(
                aimDirection.x * launchForce.x,
                aimDirection.y * launchForce.y) * t + Physics2D.gravity * (0.5f * swordGravity * (t * t));

            return position;
        }
        #endregion
        
    }
}