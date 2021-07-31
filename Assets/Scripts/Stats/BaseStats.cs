using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1,85)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progress progress = null;

        public float GetHealth()
        {
            return progress.GetHealth(characterClass,startingLevel);
        }

        public float GetXPReward()
        {
            return 10;
        }
      
    }
}
