using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Combat.EnemyControl
{
    public class AIControl : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        FightBehaviour fighter;
        GameObject player;

        private void Start()
        {
            fighter = GetComponent<FightBehaviour>();
            player = GameObject.FindWithTag("Player");
        }


        private void Update()
        {
            if (IsInAttackRange(player) && fighter.CanAttack(player))
            {
                GetComponent<FightBehaviour>().Attack(player);
            }
            else
            {
                fighter.Cancel();
            }
        }

        private bool IsInAttackRange(GameObject player)
        {
            float checker = Vector3.Distance(player.transform.position, this.gameObject.transform.position);
            return checker < chaseDistance;
        }
    }

}