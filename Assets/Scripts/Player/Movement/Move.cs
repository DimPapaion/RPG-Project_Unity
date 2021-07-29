using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Scripts.Core;
using Scripts.Combat;
using Scripts.Saving;


namespace Scripts.Player.Movement
{
    public class Move : MonoBehaviour, IAction ,ISaveable
    {
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
        
        public object CaptureState()
        {
            return new SerializableVector3( transform.position);
        }

        public void RestoreState(object state)
        {
            SerializableVector3 position = (SerializableVector3)state;
            agent.enabled = false;
            transform.position = position.ToVector();
            agent.enabled = true;
        }
    }
}
