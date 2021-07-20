using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Move : MonoBehaviour
{
    [SerializeField] Transform target;
    public NavMeshAgent agent;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoveToCursor();
        }
        UpdateAnimMove();
    }
    private void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;
        bool hasHit =  Physics.Raycast(ray, out rayHit);
        if (hasHit == true)
        {
            agent.destination = rayHit.point;
        } 
    }

    private void UpdateAnimMove()
    {
        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
        Vector3 velocityLocal = transform.InverseTransformDirection(velocity);
        float speed = velocityLocal.z;
        GetComponent<Animator>().SetFloat("Speed", speed);
    }
}
