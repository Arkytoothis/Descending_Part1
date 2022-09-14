using System.Collections;
using System.Collections.Generic;
using Descending.Scene_Underground;
using DunGen;
using DunGen.Adapters;
using UnityEngine;

namespace Descending.Dungeons
{
    public class DungeonFinisher : BaseAdapter
    {
        [SerializeField] private AstarPath _pathfinder = null;
        [SerializeField] private UndergroundManager _undergroundManager = null;

        protected override void Run(DungeonGenerator generator)
        {
            //Debug.Log("Finishing Dungeon");
            _undergroundManager.SetupParty();

            StartCoroutine(AstarScan_Coroutine());
        }

        private IEnumerator AstarScan_Coroutine()
        {
            yield return 0;
            
            _pathfinder.Scan();
        }
    }
}
