using UnityEngine;
using Scripts.Saving;
using System;

namespace Scripts.Stats
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] float experiencePoints = 0f;

        
        public event Action onExperienceGained;

        public void GainXP(float experience)
        {
            experiencePoints += experience;
            onExperienceGained();

        }

        public float GetPoints()
        {
            return experiencePoints;
        }
        public object CaptureState()
        {
            return experiencePoints;

        }
        public void RestoreState(object state)
        {
            experiencePoints = (float)state;
        }

        
    }
}