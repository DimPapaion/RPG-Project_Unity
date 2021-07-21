using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Player.Movement
{
    public class Move : MonoBehaviour
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


        public void MoveTo(Vector3 destination)
        {
            agent.destination = destination;
        }

        private void UpdateAnimMove()
        {
            Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
            Vector3 velocityLocal = transform.InverseTransformDirection(velocity);
            float speed = velocityLocal.z;
            GetComponent<Animator>().SetFloat("Speed", speed);
        }
    }
}
