using UnityEngine;

namespace Scripts.Resources
{
    public class Experience : MonoBehaviour
    {
        [SerializeField] float experiencePoints = 0f; 

        public void GainXP(float experience)
        {
            experiencePoints += experience;
        }

    }
}
