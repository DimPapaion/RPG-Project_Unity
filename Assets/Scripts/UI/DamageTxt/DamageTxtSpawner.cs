using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.UI.DamageTxt
{
    public class DamageTxtSpawner : MonoBehaviour
    {
        [SerializeField] DamageText damageTextPrefab = null;
        private void Start()
        {
            Spawn(10);
        }
        public void Spawn(float Damage)
        {
            DamageText instance = Instantiate<DamageText>(damageTextPrefab, transform);
        }
    }
}
