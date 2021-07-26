using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Core;
using Scripts.Saving;

namespace Scripts.Combat
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float healthAmount = 100f;
        bool isDead = false;

        public bool IsDead()
        {
            return isDead;
        }
        public void DamageTaken(float Damage)
        {
            if (healthAmount != 0)
            {
                healthAmount = Mathf.Max(healthAmount - Damage, 0);
                print(healthAmount);
            }
            else
            {
                Death();
            }
        }

        private void Death()
        {
            if (isDead) return;
            isDead = true;
            GetComponent<Animator>().SetTrigger("Die");
            GetComponent<ActionSched>().CancelCurrentAction();
        }

        public object CaptureState()
        {
            return healthAmount;
        }

        public void RestoreState(object state)
        {
            healthAmount = (float)state;
            if (healthAmount == 0)
            {
                Death();
            }
        }
    }
}

