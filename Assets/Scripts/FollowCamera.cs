using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform player;

    public float speed = 3.5f;
    

    private void LateUpdate()
    {
        transform.position = player.position;

        if (Input.GetMouseButtonDown(1))
        {
            CameraRot();
        }
    }

    public void CameraRot()
    {
        
    }
}
