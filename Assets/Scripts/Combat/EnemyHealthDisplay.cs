using Scripts.Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Scripts.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        FightBehaviour enemy;
        private void Awake()
        {
            enemy = GameObject.FindWithTag("Player").GetComponent<FightBehaviour>();
        }

        private void Update()
        {
            if(enemy.GetTarget() == null)
            {
                GetComponent<Text>().text = "N/A";
                return;
            }
            Health healthEnemy = enemy.GetTarget();
            
            GetComponent<Text>().text = String.Format("{0:0.0}%", healthEnemy.GetPercentage());
        }
    }
}