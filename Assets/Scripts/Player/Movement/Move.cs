using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Scripts.Core;

namespace Scripts.Player.Movement
{
    public class Move : MonoBehaviour, IAction
    {
        [SerializeField] Transform target;
        public NavMeshAgent agent;


        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        private void Update()
        {
            UpdateAnimMove();
        }

        public void StartMoveBehaviour(Vector3 destination)
        {
            GetComponent<ActionSched>().StartAction(this);
            MoveTo(destination);
        }
        public void MoveTo(Vector3 destination)
        {
            agent.destination = destination;
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
