using System.Collections;
using System.Collections.Generic;
using Descending.Core;
using Descending.Equipment;
using UnityEngine;

namespace Descending.Interactables
{
    public static class TreasureGenerator
    {
        public static void Setup()
        {
            
        }
        
        public static TreasureData GenerateTreasure(int valueModifier)
        {
            TreasureData data = new TreasureData();

            data.Coins = Random.Range(1, 101);
            data.Gems = Random.Range(0, 4);
            data.Supplies = Random.Range(0, 4);
            data.Materials = Random.Range(0, 11);

            int numItems = Random.Range(0, 6);

            for (int i = 0; i < numItems; i++)
            {
                Item item = ItemGenerator.GenerateRandomItem(Database.instance.Rarities.GetRarity("Legendary"), 10, 10, 10);
                data.AddItem(item);
            }
            
            return data;
        }
    }
}