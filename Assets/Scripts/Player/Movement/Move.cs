using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Scripts.Core;
using Scripts.Combat;


namespace Scripts.Player.Movement
{
    public class Move : MonoBehaviour, IAction
    {
        [SerializeField] Transform target;
        [SerializeField] float maxSpeed = 6f;
        public NavMeshAgent agent;
        Health health;

        void Start()
        {
            health = GetComponent<Health>();
            agent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        private void Update()
        {
            agent.enabled = !health.IsDead();
            UpdateAnimMove();
        }

        public void StartMoveBehaviour(Vector3 destination, float enemySpeed)
        {
            GetComponent<ActionSched>().StartAction(this);
            MoveTo(destination, enemySpeed);
        }
        public void MoveTo(Vector3 destination, float enemySpeed)
        {
            agent.destination = destination;
            agent.speed = maxSpeed * Mathf.Clamp01(enemySpeed);
            agent.isStopped = false;
        }
      
        public void Cancel()
        {
            agent.isStopped = true;
        }
        private void UpdateAnimMove()
        {
            Vector3 velocity = agent.velocity;
            Vector3 velocityLocal = transform.InverseTransformDirection(velocity);
            float speed = velocityLocal.z;
            GetComponent<Animator>().SetFloat("Speed", speed);
        }
    }
}
