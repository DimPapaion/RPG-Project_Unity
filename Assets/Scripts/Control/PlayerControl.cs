using UnityEngine;
using Scripts.Movement;
using Scripts.Combat;
using Scripts.Resources;
using System;
using UnityEngine.EventSystems;
using UnityEngine.AI;

namespace Scripts.Control
{
    public class PlayerControl : MonoBehaviour
    {
        Health health;


        [System.Serializable]
        struct CursorMapping
        {
            public CursorType type;
            public Texture2D texture;
            public Vector2 hotspot;
        }
        [SerializeField] CursorMapping[] cursorMappings = null;
        [SerializeField] float maxNavMeshProjectionDistance = 1f;
        [SerializeField] float maxNavPathLength = 40f;
        private void Awake()
        {
            health = GetComponent<Health>();
        }
        private void Update()
        {
            if (InteractWithUI()) return;
            if (health.IsDead()) 
            {
                SetCursor(CursorType.None);
                return; 
            }

            if (InteractWithComponent()) return;
            if (ApplyMovement()) return;

            SetCursor(CursorType.None);

        }

        private bool InteractWithComponent()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetRayMouse());
            foreach (RaycastHit hit in hits)
            {
                IRaycastable[] raycastables = hit.transform.GetComponents<IRaycastable>();
                foreach (IRaycastable raycastable in raycastables)
                {
                    if (raycastable.HandleRaycast(this))
                    {
                        SetCursor(raycastable.GetCursorType());
                        return true;
                    }
                }
            }
            return false;
         }

        RaycastHit[] RaycastAllSorted()
        {
            //Get the hits
            RaycastHit[] hits = Physics.RaycastAll(GetRayMouse());
            //Sort by distance
            float[] distances = new float[hits.Length];
            for (int i=0; i < hits.Length; i++)
            {
                distances[i] = hits[i].distance;
            }
            Array.Sort(distances, hits);
            return hits;
        }
        private bool InteractWithUI()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                SetCursor(CursorType.UI);
                return true;
            }
            return false;
        }

        private bool ApplyMovement()
        {
            Vector3 target;
            bool hasHit = RaycastNavMesh(out target);
            if (hasHit == true)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Move>().StartMoveBehaviour(target, 1f);
                }
                SetCursor(CursorType.Movement);
                return true;
            }
            return false;
        }

        private bool RaycastNavMesh(out Vector3 target)
        {
            target = new Vector3();
            RaycastHit rayHit;
            bool hasHit = Physics.Raycast(GetRayMouse(), out rayHit);
            if (!hasHit) return false;


            NavMeshHit navMeshHit;
            bool hasCastToNavMesh = NavMesh.SamplePosition(rayHit.point, out navMeshHit, maxNavMeshProjectionDistance, NavMesh.AllAreas);
            if (!hasCastToNavMesh) return false;

            target = navMeshHit.position;

            NavMeshPath path = new NavMeshPath();
            bool hasPath = NavMesh.CalculatePath(transform.position, target, NavMesh.AllAreas, path);
            if (!hasPath) return false;
            if (path.status != NavMeshPathStatus.PathComplete) return false;
            if (GetPathLength(path) > maxNavPathLength) return false;
            return true;
        }

        private float GetPathLength(NavMeshPath path)
        {
            float total = 0;

            if (path.corners.Length < 2) return total;
            for (int i=0; i < path.corners.Length - 1; i++)
            {
                total += Vector3.Distance(path.corners[i], path.corners[i + 1]);
            }
            return total;
        }

        private void SetCursor(CursorType type)
        {
            CursorMapping mapping = GetCursorMapping(type);
            Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto);
        }

        private CursorMapping GetCursorMapping(CursorType type)
        {
            foreach (CursorMapping mapping in cursorMappings)
            {
                if(mapping.type == type)
                {
                    return mapping;
                }
            }
            return cursorMappings[0];
        }
        private static Ray GetRayMouse()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

}