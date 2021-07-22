using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Player.Movement;
using Scripts.Core;


namespace Scripts.Combat
{
    public class FightBehaviour : MonoBehaviour, IAction
    {
        [SerializeField] float WeaponMeleRange = 2f;
        [SerializeField] float AttackDelay = 1.9f;
        [SerializeField] float WeaponDamage = 20f;

        Health target;
        float lastAttackTime;

        private void Update()
        {
            lastAttackTime += Time.deltaTime;

            if (target == null) return;
            if (target.IsDead() == true) return;
            if (!GetIsInRange())
            {
                GetComponent<Move>().MoveTo(target.transform.position);
            }
            else
            {
                GetComponent<Move>().Cancel();
                AttackBehaviour();
            }
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < WeaponMeleRange;
        }

        public void Attack(TargetEnemy enemyTarget)
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
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("StopAttack");
        }

        public bool CanAttack(TargetEnemy enemyTarget)
        {
            if(enemyTarget == null) { return false; }
            Health targetHealth = enemyTarget.GetComponent<Health>();
            return targetHealth != null && !targetHealth.IsDead();
        }
        //For the Animation Event
        public void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if ( lastAttackTime > AttackDelay)
            {
                TriggeredAttack();
                lastAttackTime = 0;
                Hit();
            }
        }

        private void TriggeredAttack()
        {
            GetComponent<Animator>().ResetTrigger("StopAttack");
            GetComponent<Animator>().SetTrigger("Attack");
        }

        private void Hit()
        {
            if(target == null) return; 
            target.DamageTaken(WeaponDamage);
        }
    }
}
