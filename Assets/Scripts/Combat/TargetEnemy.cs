using UnityEngine;
using Scripts.Attributes;
using Scripts.Control;

namespace Scripts.Combat
{

    [RequireComponent(typeof(Health))]
    public class TargetEnemy : MonoBehaviour, IRaycastable
    {
        public CursorType GetCursorType()
        {
            return CursorType.Combat;
        }

        public bool HandleRaycast(PlayerControl callingController)
        {

            if (!callingController.GetComponent<FightBehaviour>().CanAttack(gameObject)) 
            {
                return false;
            }

            if (Input.GetMouseButton(0))
            {
                callingController.GetComponent<FightBehaviour>().Attack(gameObject);
            }
            return true;
        }
    }
}
