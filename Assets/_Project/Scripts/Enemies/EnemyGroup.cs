using System;
using System.Collections;
using System.Collections.Generic;
using Descending.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Descending.Enemies
{
    public class EnemyGroup : MonoBehaviour
    {
        [SerializeField] private int _minimumMinions = 1;
        [SerializeField] private int _maximumMinions = 4;
        [SerializeField] private int _minimumElites = 1;
        [SerializeField] private int _maximumElites = 4;
        [SerializeField] private int _uniqueChance = 0;
        [SerializeField] private float _spawnRange = 10f;
        [SerializeField] private Transform _enemiesParent = null;
        [SerializeField] private GameObject _visual = null;
        [SerializeField] private List<GameObject> _minionPrefabs = null;
        [SerializeField] private List<GameObject> _elitePrefabs = null;
        [SerializeField] private List<GameObject> _uniquePrefabs = null;

        private void Start()
        {
            SpawnEnemies();
            _visual.SetActive(false);
        }

        private void SpawnEnemies()
        {
            int numMinions = Random.Range(_minimumMinions, _maximumMinions + 1);
            for (int i = 0; i < numMinions; i++)
            {
                int prefabIndex = Random.Range(0, _minionPrefabs.Count);
                GameObject clone = Instantiate(_minionPrefabs[prefabIndex], _enemiesParent);
                clone.transform.position = transform.position + new Vector3(Random.Range(-_spawnRange, _spawnRange), 0, Random.Range(-_spawnRange, _spawnRange));
                Enemy enemy = clone.GetComponent<Enemy>();
                enemy.Setup();
            }
            
            int numElites = Random.Range(_minimumElites, _maximumElites + 1);
            for (int i = 0; i < numElites; i++)
            {
                int prefabIndex = Random.Range(0, _elitePrefabs.Count);
                GameObject clone = Instantiate(_elitePrefabs[prefabIndex], _enemiesParent);
                clone.transform.position = transform.position + new Vector3(Random.Range(-_spawnRange, _spawnRange), 0, Random.Range(-_spawnRange, _spawnRange));
                Enemy enemy = clone.GetComponent<Enemy>();
                enemy.Setup();
            }

            for (int i = 0; i < _uniquePrefabs.Count; i++)
            {
                if (Random.Range(0, 100) <= _uniqueChance)
                {
                    GameObject clone = Instantiate(_uniquePrefabs[i], _enemiesParent);
                    clone.transform.position = transform.position + new Vector3(Random.Range(-_spawnRange, _spawnRange), 0, Random.Range(-_spawnRange, _spawnRange));
                    Enemy enemy = clone.GetComponent<Enemy>();
                    enemy.Setup();
                }
            }
        }
    }
}