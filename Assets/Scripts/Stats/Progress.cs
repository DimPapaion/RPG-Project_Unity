using UnityEngine;

namespace Scripts.Stats
{
    [CreateAssetMenu(fileName = "Progress", menuName = "RPG Project/Stats/New Progress", order = 0)]
    public class Progress : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses = null;


        public float GetStat(Stat stat, CharacterClass characterClass, int level)
        {
            foreach (ProgressionCharacterClass progressionClass in characterClasses)
            {
                if (progressionClass.characterClass != characterClass) continue;

                foreach (ProgressionStat progressionStat in progressionClass.stats)
                {
                    if (progressionStat.stat != stat) continue;

                    if (progressionStat.levels.Length < level) continue;

                    return progressionStat.levels[level - 1];
                } 
            }
            return 0;
        }
        [System.Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass characterClass;
            public ProgressionStat[] stats;
            
        }

        [System.Serializable]
        class ProgressionStat
        {
            public Stat stat;
            public float[] levels;
        }
    }
}