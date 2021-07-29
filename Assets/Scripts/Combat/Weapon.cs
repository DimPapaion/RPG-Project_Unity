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
        

        public void Spawn(Transform handTransform, Animator animator)
        {
            if (weaponPref != null)
            {
                Instantiate(weaponPref, handTransform);
            }
            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
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