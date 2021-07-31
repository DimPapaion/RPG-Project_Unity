using UnityEngine;
using UnityEngine.Playables;
using Scripts.Core;
using Scripts.Player;

namespace Scripts.Cinematics
{
    public class CinematicsRemoveCtrl : MonoBehaviour
    {
        GameObject player;
        private void Start()
        {
            GetComponent<PlayableDirector>().played += DisableCtrl;
            GetComponent<PlayableDirector>().stopped += EnableCtrl;
            player = GameObject.FindWithTag("Player");
        }
        void DisableCtrl(PlayableDirector pd)
        {
            
            player.GetComponent<ActionSched>().CancelCurrentAction();
            player.GetComponent<PlayerControl>().enabled = false;
            
        }

        void EnableCtrl(PlayableDirector pd)
        {
            player.GetComponent<PlayerControl>().enabled = true;
        }
    }
}
