using UnityEngine;

namespace Scripts.Combat
{

    [CreateAssetMenu(fileName ="Weapon", menuName ="Weapons/Make New Weapon", order =0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] GameObject weaponPref = null;
        [SerializeField] float WeaponDamage = 20f;
        [SerializeField] float WeaponRange = 2f;
        [SerializeField] bool isRightHanded = true;
        [SerializeField] Projectile projectile = null;
        

        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            if (weaponPref != null)
            {
                Transform handTransform = GetTransform(rightHand, leftHand);
                Instantiate(weaponPref, handTransform);
            }
            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
        }

        private Transform GetTransform(Transform rightHand, Transform leftHand)
        {
            Transform handTransform;
            if (isRightHanded) handTransform = rightHand;
            else handTransform = leftHand;
            return handTransform;
        }

        public bool hasProjectile()
        {
            return projectile != null;
        }

        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target)
        {
            Projectile projectileInstance = Instantiate(projectile, GetTransform(rightHand, leftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(target);
        }
        public float GetWeapDamage()
        {
            return WeaponDamage;
        }

        public float GetWeapRange()
        {
            return WeaponRange;
        }
    }
}