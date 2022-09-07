using System.Collections;
using System.Collections.Generic;
using System.IO;
using Descending.Core;
using Descending.Encounters;
using Descending.Party;
using ScriptableObjectArchitecture;
using Sirenix.Serialization;
using UnityEngine;

namespace Descending.World
{
    public class WorldGenerator : MonoBehaviour
    {
        private List<WorldSpawner> _worldSpawners = new List<WorldSpawner>();
        
        public void Generate()
        {
        }
        
        public void OnRegisterSpawner(WorldSpawner spawner)
        {
            _worldSpawners.Add(spawner);
        }
    }
}
