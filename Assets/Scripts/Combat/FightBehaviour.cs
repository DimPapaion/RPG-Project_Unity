using UnityEngine;
using Scripts.Movement;
using Scripts.Core;
using Scripts.Saving;
using Scripts.Resources;
using Scripts.Stats;
using System.Collections.Generic;
using Scripts.Utils;

namespace Scripts.Combat
{
    public class FightBehaviour : MonoBehaviour, IAction, ISaveable, IModifierProvider
    {
        
        [SerializeField] float AttackDelay = 1.9f;
        [SerializeField] Weapon defaultWeapon = null;
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;


        Health target;
        float lastAttackTime = Mathf.Infinity;
        LazyValue<Weapon> currentWeapon;

        private void Awake()
        {
            currentWeapon = new LazyValue<Weapon>(SetupDefaultWeapon);
        }

        private Weapon SetupDefaultWeapon()
        {
            AttachWeapon(defaultWeapon);
            return defaultWeapon;
        }

        private void Start()
        {
            currentWeapon.ForceInit();
        }


        private void Update()
        {
            lastAttackTime += Time.deltaTime;

            if (target == null) return;
            if (target.IsDead() == true) return;
            if (!GetIsInRange())
            {
                GetComponent<Move>().MoveTo(target.transform.position, 1f);
            }
            else
            {
                GetComponent<Move>().Cancel();
                AttackBehaviour();
            }
        }
        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon.value = weapon;
            AttachWeapon(weapon);

        }

        private void AttachWeapon(Weapon weapon)
        {
            Animator animator = GetComponent<Animator>();
            weapon.Spawn(rightHandTransform, leftHandTransform, animator);
        }

        public Health GetTarget()
        {
            return target;
        }
        public void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (lastAttackTime > AttackDelay)
            {
                TriggeredAttack();
                lastAttackTime = 0;
                Hit();
            }
        }
        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.value.GetWeapRange();
        }

        public void Attack(GameObject enemyTarget)
        {
            GetComponent<ActionSched>().StartAction(this);
            target = enemyTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            StopAttack();
            target = null;
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("Attack");
            GetComponent<Animator>().SetTrigger("StopAttack");
        }

        public IEnumerable<float> GetAdditiveModifiers(Stat stat)
        {
            if(stat == Stat.Damage)
            {
                yield return currentWeapon.value.GetWeapDamage();
            }
        }

        public IEnumerable<float> GetPercentageModifiers(Stat stat)
        {
            if (stat == Stat.Damage)
            {
                yield return currentWeapon.value.GetPercentageBonus();
            }
        }


        public bool CanAttack(GameObject enemyTarget)
        {
            if(enemyTarget == null) { return false; }
            Health targetHealth = enemyTarget.GetComponent<Health>();
            return targetHealth != null && !targetHealth.IsDead();
        }
        //For the Animation Event
        

        private void TriggeredAttack()
        {
            GetComponent<Animator>().ResetTrigger("StopAttack");
            GetComponent<Animator>().SetTrigger("Attack");
        }

        private void Hit()
        {
            if(target == null) return;
            float damage = GetComponent<BaseStats>().GetStat(Stat.Damage);
            if (currentWeapon.value.hasProjectile())
            { 
                currentWeapon.value.LaunchProjectile(rightHandTransform, leftHandTransform, target, gameObject, damage);
            }
            else
            {
                target.DamageTaken(gameObject, damage);
            }
        }
        private void Shoot()
        {
            Hit();
        }

        public object CaptureState()
        {
            return currentWeapon.value.name;
        }

        public void RestoreState(object state)
        {
            string weaponName = (string)state;
            Weapon weapon = UnityEngine.Resources.Load<Weapon>(weaponName);
            EquipWeapon(weapon);
        }
    }
}
