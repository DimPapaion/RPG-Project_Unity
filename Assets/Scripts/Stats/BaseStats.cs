using UnityEngine;

namespace Scripts.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1,85)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progress progress = null;

        public float GetStat(Stat stat)
        {
            return progress.GetStat(stat, characterClass,startingLevel);
        }
    }
}
