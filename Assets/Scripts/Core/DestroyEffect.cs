using UnityEngine;

namespace Scripts.Core
{
    public class DestroyEffect : MonoBehaviour
    {

        private void Update()
        {
            if (!GetComponent<ParticleSystem>().IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }
}
