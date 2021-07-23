using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Combat.EnemyControl
{
    public class EnemyPath : MonoBehaviour
    {
        const float gizmosWaypointRadious = 0.3f;
        private void OnDrawGizmos()
        {
            for (int i=0; i < transform.childCount; i++)
            {
                int j = GetNextWaypoint(i);
                Gizmos.DrawSphere(GetWaypoint(i), gizmosWaypointRadious);
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
            }
        }

        public int GetNextWaypoint(int i)
        {
            if (i+1 == transform.childCount)
            {
                return 0;
            }
            return i + 1;
        }

        public Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }
    }
}
