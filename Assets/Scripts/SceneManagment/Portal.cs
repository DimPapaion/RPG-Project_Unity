using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;


namespace Scripts.SceneManagment
{
    public class Portal : MonoBehaviour
    {

        enum DestinationID
        {
            A,B,C,D,E
        }

        [SerializeField] int sceneToLoad = -1;
        [SerializeField] Transform spawnPos;
        [SerializeField] DestinationID destination;
        [SerializeField] float fadeOutTime = 1f;
        [SerializeField] float fadeInTime = 2f;
        [SerializeField] float FadeWaithTime = 0.5f;
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            { 
                StartCoroutine(Transition());
                
            }
        }

        private IEnumerator Transition()
        {
            if (sceneToLoad < 0)
            {
                Debug.Log("Scene is not setted.!");
                yield break;
            }

            
            DontDestroyOnLoad(gameObject);
            Fader fader = FindObjectOfType<Fader>();

            yield return fader.FadeOut(fadeOutTime);

            // Saving the level

            SavingWrap wrapper = FindObjectOfType<SavingWrap>();
            wrapper.Save();

            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            //Load current Level
            
            wrapper.Load();

            Portal otherPort = GetOtherPort();
            UpdatePlayer(otherPort);
            yield return new WaitForSeconds(FadeWaithTime);
            yield return fader.FadeIn(fadeInTime);
            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal otherPort)
        {
            GameObject player =  GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(otherPort.spawnPos.position);
            player.transform.rotation = otherPort.spawnPos.rotation;
        }

        private Portal GetOtherPort()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
            if (portal.destination!= destination) continue;
                return portal;
            }
            return null;
        }
    }
}