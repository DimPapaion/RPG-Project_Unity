using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Saving;

namespace Scripts.SceneManagment
{
    public class SavingWrap : MonoBehaviour
    {
        const string defaultSaveFile = "Save05";
        [SerializeField] float fadeInTime = 0.2f;

        IEnumerator Start()
        {
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediate();
            yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile);
            yield return fader.FadeIn(fadeInTime);
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }

        public void Load()
        {
            print("Savving....");
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }
    }
}
