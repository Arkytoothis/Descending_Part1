using System.Collections;
using System.Collections.Generic;
using Descending.Enemies;
using UnityEngine;

namespace Descending.Player
{
    public class EnemyDetector : MonoBehaviour
    {
        [SerializeField] private GameObject _playerObject = null;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                Enemy enemy = other.gameObject.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.SetTarget(_playerObject);
                }
            }
        }
    }
}