using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descending
{
    [System.Serializable]
    public class EnemyShort
    {
        [SerializeField] private EnemyDefinition _enemyDefinition = null;
        [SerializeField] private int _level = 0;

        public EnemyDefinition EnemyDefinition => _enemyDefinition;
        public int Level => _level;

        public EnemyShort(EnemyDefinition enemyDefinition, int level)
        {
            _enemyDefinition = enemyDefinition;
            _level = level;
        }
    }
}
