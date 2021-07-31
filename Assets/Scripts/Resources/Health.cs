using UnityEngine;
using Scripts.Core;
using Scripts.Stats;
using Scripts.Saving;
using System;

namespace Scripts.Resources
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float healthAmount = 100f;
        bool isDead = false;

        private void Start()
        {
            healthAmount = GetComponent<BaseStats>().GetHealth();
        }
        public bool IsDead()
        {
            return isDead;
        }
        public void DamageTaken(GameObject instigator,  float Damage)
        {
            healthAmount = Mathf.Max(healthAmount- Damage, 0);
            if (healthAmount == 0)
            {
                Death();
                AwardExperiece(instigator);
            }
        }

       

        public float GetPercentage()
        {
            return (healthAmount / GetComponent<BaseStats>().GetHealth()) * 100;
        }
        private void Death()
        {
            if (isDead) return;
            isDead = true;
            GetComponent<Animator>().SetTrigger("Die");
            GetComponent<ActionSched>().CancelCurrentAction();
        }

        private void AwardExperiece(GameObject instigator)
        {
            Experience experience =  instigator.GetComponent<Experience>();
            if (experience == null) return;
            experience.GainXP(GetComponent<BaseStats>().GetXPReward());
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

