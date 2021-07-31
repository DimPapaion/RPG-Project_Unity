using System;
using UnityEngine;

namespace Scripts.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1,85)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progress progress = null;
        [SerializeField] GameObject levelupParticleEffect = null;

        public event Action onLevelUp;
        int currentLevel = 0;


        private void Start()
        {
            currentLevel = CalculateLevel();
            Experience experience = GetComponent<Experience>();
            if(experience != null)
            {
                experience.onExperienceGained += UpdateLevel;
            }
        }
        private void UpdateLevel()
        {
            int newLevel = CalculateLevel();
            if(newLevel > currentLevel)
            {
                currentLevel = newLevel;
                LevelUpEffect();
                onLevelUp();

            }
        }

        private void LevelUpEffect()
        {
            Instantiate(levelupParticleEffect, transform);
        }

        public float GetStat(Stat stat)
        {
            return progress.GetStat(stat, characterClass,GetLevel());
        }

        public int GetLevel()
        {
            if(currentLevel < 1)
            {
                currentLevel = CalculateLevel();
            }
            return currentLevel;
        }
        public int CalculateLevel()
        {
            Experience experience = GetComponent<Experience>();

            if (experience == null) return startingLevel;
            float currentExp = experience.GetPoints();

            int penultimateLevel = progress.GetLevels(Stat.ExperienceToLvlUp, characterClass);
            for (int level = 1; level <= penultimateLevel; level++)
            {
                float XPtoLvlup = progress.GetStat(Stat.ExperienceToLvlUp, characterClass, level);
                if (XPtoLvlup > currentExp)
                {
                    return level;
                }
            }
            return penultimateLevel + 1;
        }
    }
}
