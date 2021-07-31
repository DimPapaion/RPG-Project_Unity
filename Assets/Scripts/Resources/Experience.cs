using UnityEngine;
using Scripts.Saving;
using System;

namespace Scripts.Resources
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] float experiencePoints = 0f;

        

        public void GainXP(float experience)
        {
            experiencePoints += experience;

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