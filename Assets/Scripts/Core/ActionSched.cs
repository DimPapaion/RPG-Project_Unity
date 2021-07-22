using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Core
{
    public class ActionSched : MonoBehaviour
    {
        MonoBehaviour currentAction;

        public void StartAction(MonoBehaviour action)
        {
            if (currentAction == action) return;
            if(currentAction != null)
            {
                print("Cancelling Action" + currentAction);
            }
            
            currentAction = action;
        }
    }
    
}
