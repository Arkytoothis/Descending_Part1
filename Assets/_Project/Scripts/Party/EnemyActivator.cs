using System.Collections;
using System.Collections.Generic;
using Descending.Enemies;
using ParadoxNotion;
using UnityEngine;

namespace Descending.Player
{
    public class EnemyActivator : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                Enemy enemy = other.gameObject.GetComponent<Enemy>();
                if (enemy != null)
                {
                    if (enemy.IsActive == false)
                    {
                        enemy.SetActive(true);
                    }
                }
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                Enemy enemy = other.gameObject.GetComponent<Enemy>();
                if (enemy != null)
                {
                    if (enemy.IsActive == true)
                    {
                        enemy.SetActive(false);
                    }
                }
            }
        }
    }
}