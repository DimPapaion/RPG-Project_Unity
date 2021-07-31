using UnityEngine;

namespace Scripts.Core
{
    public class DestroyEffect : MonoBehaviour
    {
        [SerializeField] GameObject targetToDestroy = null;
        private void Update()
        {
            if (!GetComponent<ParticleSystem>().IsAlive())
            {
                if (targetToDestroy != null)
                {
                    Destroy(targetToDestroy);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
