using UnityEngine;
using Scripts.Movement;
using Scripts.Core;
using Scripts.Resources;
using Scripts.Utils;
using Scripts.Combat;

namespace Scripts.Control
{
    public class AIControl : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspiciousTime = 2f;
        [SerializeField] EnemyPath enemyPath;
        [SerializeField] float waypointTollerance = 1f;
        [SerializeField] float waypointDwellingTime = 1f;
        [Range(0,1)]
        [SerializeField] float enemySpeedWander = 0.2f;

        FightBehaviour fighter;
        Health health; 
        GameObject player;
        Move move;

        LazyValue<Vector3> protectedLocation;
        float timeSinceLostPlayer = Mathf.Infinity;
        float timeSinceNextWaypoint = Mathf.Infinity;
        int currentWaypoint = 0;

        private void Awake()
        {
            move = GetComponent<Move>();
            fighter = GetComponent<FightBehaviour>();
            health = GetComponent<Health>();
            player = GameObject.FindWithTag("Player");

            protectedLocation = new LazyValue<Vector3>(GetProtectedLoc);
        }

        private Vector3 GetProtectedLoc()
        {
            return transform.position;
        }

        private void Start()
        {
            protectedLocation.ForceInit();
        }


        private void Update()
        {
            if (health.IsDead()) return;

            if (IsInAttackRange(player) && fighter.CanAttack(player))
            {
                
                AttackBehaviour();
                
            }
            else if (timeSinceLostPlayer < suspiciousTime)
            {
                SuspiciousBehaviour();
            }
            else
            {
                EnemyMoveBehaviour();
            }
            UpdateTimers();
        }

        private void UpdateTimers()
        {
            timeSinceLostPlayer += Time.deltaTime;
            timeSinceNextWaypoint += Time.deltaTime;
        }

        private void EnemyMoveBehaviour()
        {
            Vector3 nextPos = protectedLocation.value;
            if (enemyPath !=null)
            {
                if (AtWaypoint())
                {
                    timeSinceNextWaypoint = 0;
                    CycleWaypoint();
                }
                nextPos = GetCurrentWaypoint();              
            }
            if(timeSinceNextWaypoint > waypointDwellingTime)
            {
                move.StartMoveBehaviour(nextPos, enemySpeedWander);
            }  
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTollerance;
        }
        private Vector3 GetCurrentWaypoint()
        {
            return enemyPath.GetWaypoint(currentWaypoint);
        }

        private void CycleWaypoint()
        {
            currentWaypoint = enemyPath.GetNextWaypoint(currentWaypoint);
        }

        

        private void SuspiciousBehaviour()
        {
            GetComponent<ActionSched>().CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
            timeSinceLostPlayer = 0;
            GetComponent<FightBehaviour>().Attack(player);
        }

        private bool IsInAttackRange(GameObject player)
        {
            float checker = Vector3.Distance(player.transform.position, this.gameObject.transform.position);
            return checker < chaseDistance;
        }

        //Hnadled by UnityUI
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}