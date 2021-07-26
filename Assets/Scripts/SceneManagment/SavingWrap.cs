using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Saving;

namespace Scripts.SceneManagment
{
    public class SavingWrap : MonoBehaviour
    {
        const string defaultSaveFile = "save";
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

        private void Save()
        {
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }

        private void Load()
        {
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }
    }
}
