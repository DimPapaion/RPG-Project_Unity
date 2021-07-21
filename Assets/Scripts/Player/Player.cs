using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Player.Movement;
namespace Assets.Scripts.Player
{
    public class Player : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                MoveToCursor();
            }
        }
        private void MoveToCursor()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            bool hasHit = Physics.Raycast(ray, out rayHit);
            if (hasHit == true)
            {
                GetComponent<Move>().MoveTo(rayHit.point);
            }
        }
    }

}