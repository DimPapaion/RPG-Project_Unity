using UnityEngine;
using Scripts.Player.Movement;
using Scripts.Combat;
using Scripts.Resources;

namespace Scripts.Player
{
    public class PlayerControl : MonoBehaviour
    {
        Health health;
        private void Awake()
        {
            health = GetComponent<Health>();
        }
        private void Update()
        {
            if (health.IsDead()) return;
            if (ApplyCombat()) return;
            if (ApplyMovement()) return;
            
        }

        private bool ApplyCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetRayMouse());
            foreach (RaycastHit hit in hits)
            {
                TargetEnemy target = hit.transform.GetComponent<TargetEnemy>();
                if (target == null) continue;


                if (!GetComponent<FightBehaviour>().CanAttack(target.gameObject)) continue;

                if (Input.GetMouseButton(0))
                {
                    GetComponent<FightBehaviour>().Attack(target.gameObject);
                }
                return true;
            }
            return false;
        }

        private bool ApplyMovement()
        {
            RaycastHit rayHit;
            bool hasHit = Physics.Raycast(GetRayMouse(), out rayHit);
            if (hasHit == true)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Move>().StartMoveBehaviour(rayHit.point, 1f);
                }
                return true;
            }
            return false;
        }

        private static Ray GetRayMouse()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

}