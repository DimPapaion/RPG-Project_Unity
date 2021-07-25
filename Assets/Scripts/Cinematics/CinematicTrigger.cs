using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace Scripts.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        bool isTriggeredOnce = false; 
        private void OnTriggerEnter(Collider other)
        {
            if (!isTriggeredOnce && other.gameObject.tag == "Player")
            {
                isTriggeredOnce = true;
                GetComponent<PlayableDirector>().Play();
            }
        }
    }

}