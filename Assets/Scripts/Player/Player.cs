using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Player.Movement;
using Scripts.Combat;
using System;

namespace Scripts.Player
{
    public class Player : MonoBehaviour
    {
        private void Update()
        {
            if (ApplyCombat()) return;
            if (ApplyMovement()) return;
            print("nothing to do.!");
        }

        private bool ApplyCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetRayMouse());
            foreach (RaycastHit hit in hits)
            {
                TargetEnemy target = hit.transform.GetComponent<TargetEnemy>();
                if (target == null) continue;

                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<FightBehaviour>().Attack(target);
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
                    GetComponent<Move>().StartMoveBehaviour(rayHit.point);
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