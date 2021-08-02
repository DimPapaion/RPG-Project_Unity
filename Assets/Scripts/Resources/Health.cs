using UnityEngine;
using Scripts.Core;
using Scripts.Stats;
using Scripts.Saving;
using System;
using Scripts.Utils;
using UnityEngine.Events;

namespace Scripts.Resources
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float regenerationPercentage = 70;
        [SerializeField] UnityEvent takeDamge;

        LazyValue<float> healthAmount;

        bool isDead = false;

        private void Awake()
        {
            healthAmount = new LazyValue<float>(GetInitialHealth);
        }

        private float GetInitialHealth()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }
        private void Start()
        {
            healthAmount.ForceInit();
            
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
            healthAmount.value = Mathf.Max(healthAmount.value- Damage, 0);
           

            if (healthAmount.value == 0)
            {
                Death();
                AwardExperiece(instigator);
            }
            else
            {
                takeDamge.Invoke();
            }
        }

        public float GetHealthPoints()
        {
            return healthAmount.value;
        }
        public float GetMaxHealthPoints()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }
        public float GetPercentage()
        {
            return (healthAmount.value / GetComponent<BaseStats>().GetStat(Stat.Health)) * 100;
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
            healthAmount.value = Mathf.Max(healthAmount.value, regenHealthPoints);
        }
        public object CaptureState()
        {
            return healthAmount.value;
        }

        public void RestoreState(object state)
        {
            healthAmount.value = (float)state;
            if (healthAmount.value == 0)
            {
                Death();
            }
        }
    }
}

