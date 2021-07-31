using UnityEngine;

namespace Scripts.Stats
{
    [CreateAssetMenu(fileName = "Progress", menuName = "RPG Project/Stats/New Progress", order = 0)]
    public class Progress : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses = null;


        public float GetHealth(CharacterClass characterClass, int level)
        {
            foreach (ProgressionCharacterClass progressionClass in characterClasses)
            {
                if(progressionClass.characterClass == characterClass)
                {
                    return progressionClass.health[level - 1];
                }
            }
            return 0;
        }
        [System.Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass characterClass;
            public float[] health;
        }
    }
}