using UnityEngine;
using Scripts.Core;
using Scripts.Stats;
using Scripts.Saving;
using System;

namespace Scripts.Resources
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float regenerationPercentage = 70;

        float healthAmount = -1f;

        bool isDead = false;

        private void Start()
        {
            if (healthAmount < 0)
            {
                healthAmount = GetComponent<BaseStats>().GetStat(Stat.Health);
            }
        }
        private void OnEnable()
        {
            GetComponent<BaseStats>().onLevelUp += RegenHealth;
        }

        private void OnDisable()
        {
            GetComponent<BaseStats>().onLevelUp -= RegenHealth;
        }
        public bool IsDead()
        {
            return isDead;
        }
        public void DamageTaken(GameObject instigator,  float Damage)
        {
            print(gameObject.name + "Took damage: " + Damage);
            healthAmount = Mathf.Max(healthAmount- Damage, 0);
            if (healthAmount == 0)
            {
                Death();
                AwardExperiece(instigator);
            }
        }

        public float GetHealthPoints()
        {
            return healthAmount;
        }
        public float GetMaxHealthPoints()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }
        public float GetPercentage()
        {
            return (healthAmount / GetComponent<BaseStats>().GetStat(Stat.Health)) * 100;
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
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) return;
            experience.GainXP(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
        }

        private void RegenHealth()
        {
            float regenHealthPoints = GetComponent<BaseStats>().GetStat(Stat.Health) * (regenerationPercentage / 100);
            healthAmount = Mathf.Max(healthAmount, regenHealthPoints);
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

