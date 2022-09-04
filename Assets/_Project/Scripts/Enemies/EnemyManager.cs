using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Descending.Enemies
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private List<Enemy> _enemies = new List<Enemy>();

        public void OnRegisterEnemy(Enemy enemy)
        {
            _enemies.Add(enemy);
        }
    }
}