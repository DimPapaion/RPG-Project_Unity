using UnityEngine;
using Scripts.Core;
using Scripts.Stats;
using Scripts.Saving;
using System;
using Scripts.Utils;
using UnityEngine.Events;

namespace Scripts.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float regenerationPercentage = 70;
        [SerializeField] TakeDamageEvent takeDamge;
        [SerializeField] UnityEvent onDie;

        [System.Serializable]
        public class TakeDamageEvent : UnityEvent<float>
        {

        }

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
                onDie.Invoke();
                Death();
                AwardExperiece(instigator);
            }
            else
            {
                takeDamge.Invoke(Damage);
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
            return GetFraction() * 100;
        }

        public float GetFraction()
        {
            return healthAmount.value / GetComponent<BaseStats>().GetStat(Stat.Health);
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

