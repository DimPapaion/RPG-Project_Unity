using UnityEngine;
using UnityEngine.UI;
using System;

namespace Scripts.Resources
{
    public class HealthDisplay : MonoBehaviour
    {
        Health health;
        private void Awake()
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
        }

        private void Update()
        {
            //GetComponent<Text>().text = String.Format("{0:0.0}%" ,health.GetPercentage());
            GetComponent<Text>().text = String.Format("{0:0.0}/{1:0}", health.GetHealthPoints(),health.GetMaxHealthPoints());
        }

    }
}
