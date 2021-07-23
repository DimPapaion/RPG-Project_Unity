using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Core;

namespace Scripts.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float healthAmount = 100f;
        bool isDead = false;

        public bool IsDead()
        {
            return isDead;
        }
        public void DamageTaken(float Damage)
        {
            
            if (healthAmount != 0f)
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
    }
}

