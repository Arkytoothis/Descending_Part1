using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Descending.Core;
using Descending.World;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Descending.Dungeons
{
    public class DungeonEntrance : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPosition = null;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("Loading Dungeon");
                SaveSpawnPosition();
                SceneManager.LoadScene((int) GameScenes.Underground);
            }
        }

        private void SaveSpawnPosition()
        {
            byte[] bytes = null;
            SceneLoadData loadDate = new SceneLoadData(SceneTransitionTypes.Overworld_Underworld, SceneBuildTypes.Load);
            bytes = SerializationUtility.SerializeValue(loadDate, DataFormat.JSON);
            File.WriteAllBytes(Database.instance.SceneLoadFilePath, bytes);

            PartySpawnData spawnData = new PartySpawnData(_spawnPosition.position);
            bytes = SerializationUtility.SerializeValue(spawnData, DataFormat.JSON);
            File.WriteAllBytes(Database.instance.OverworldSpawnFilePath, bytes);
        }
    }
}
