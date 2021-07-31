using UnityEngine;

namespace Scripts.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1,85)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progress progress = null;

        private void Update()
        {
            if (gameObject.tag == "Player")
            {
                print(GetLevel());
            }
        }
        public float GetStat(Stat stat)
        {
            return progress.GetStat(stat, characterClass,startingLevel);
        }

        public int GetLevel()
        {
            float currentExp = GetComponent<Experience>().GetPoints();
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
