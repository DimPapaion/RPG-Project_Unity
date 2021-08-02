using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Attributes
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Health healthCombonent = null;
        [SerializeField] RectTransform foreground = null;
        [SerializeField] Canvas RootCanvas = null;


        void Update()
        {
            if (Mathf.Approximately(healthCombonent.GetFraction(), 0) 
                || Mathf.Approximately(healthCombonent.GetFraction(), 1))
            {
                RootCanvas.enabled = false;
                return;
            }
            RootCanvas.enabled = true;

            foreground.localScale = new Vector3(healthCombonent.GetFraction(), 1, 1);
        }
    }
}
