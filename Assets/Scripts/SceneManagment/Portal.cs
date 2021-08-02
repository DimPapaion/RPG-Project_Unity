using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using Scripts.Control;

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
            SavingWrap savingWrapper = FindObjectOfType<SavingWrap>();
            PlayerControl playerController = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
            playerController.enabled = false;

            yield return fader.FadeOut(fadeOutTime);

            savingWrapper.Save();

            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            PlayerControl newPlayerController = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
            newPlayerController.enabled = false;

            savingWrapper.Load();

            Portal otherPort = GetOtherPort();
            UpdatePlayer(otherPort);

            savingWrapper.Save();

            yield return new WaitForSeconds(FadeWaithTime);
            fader.FadeIn(fadeInTime);

            newPlayerController.enabled = true;
            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal otherPort)
        {
            GameObject player =  GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = otherPort.spawnPos.position;
            player.transform.rotation = otherPort.spawnPos.rotation;
            player.GetComponent<NavMeshAgent>().enabled = true;
        }

        private Portal GetOtherPort()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
            if (portal.destination != destination) continue;

                return portal;
            }

            return null;
        }
    }
}