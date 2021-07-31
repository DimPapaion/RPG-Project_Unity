using UnityEngine;

namespace Scripts.Core
{
    public class PersistentOBJSpawn : MonoBehaviour
    {
        [SerializeField] GameObject persistOBJPrefab;
        static bool hasSpawned = false;
       
        private void Awake()
        {
            if (hasSpawned) return;
            SpawnedPersistOBJ();
            hasSpawned = true;
        }

        private void SpawnedPersistOBJ()
        {
            GameObject persistOBJ = Instantiate(persistOBJPrefab);
            DontDestroyOnLoad(persistOBJ);
        }
    }
}
