using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] Weapon weapon = null;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                other.GetComponent<FightBehaviour>().EquipWeapon(weapon);
                Destroy(gameObject);
            }
        }
    }
}
