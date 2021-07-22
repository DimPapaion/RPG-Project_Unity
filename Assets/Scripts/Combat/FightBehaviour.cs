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
        private Transform target;

        private void Update()
        {
            if (target == null) return;
            if (!GetIsInRange())
            {
                GetComponent<Move>().MoveTo(target.position);
            }
            else
            {
                GetComponent<Move>().Cancel();
            }
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < WeaponMeleRange;
        }

        public void Attack(TargetEnemy enemyTarget)
        {
            GetComponent<ActionSched>().StartAction(this);
            target = enemyTarget.transform;
        }

        public void Cancel()
        {
            target = null;
        }
    }
}
