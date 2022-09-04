using System.Collections;
using System.Collections.Generic;
using Descending.Equipment;
using UnityEngine;

namespace Descending.Interactables
{
    [System.Serializable]
    public class TreasureData
    {
        [SerializeField] private TreasureChest _treasureChest = null;
        [SerializeField] private int _coins = 0;
        [SerializeField] private int _gems = 0;
        [SerializeField] private int _supplies = 0;
        [SerializeField] private int _materials = 0;
        [SerializeField] private List<Item> _items = null;
    
        public int Coins
        {
            get => _coins;
            set => _coins = value;
        }

        public int Gems
        {
            get => _gems;
            set => _gems = value;
        }

        public int Supplies
        {
            get => _supplies;
            set => _supplies = value;
        }

        public int Materials
        {
            get => _materials;
            set => _materials = value;
        }

        public List<Item> Items => _items;
        public TreasureChest TreasureChest => _treasureChest;

        public TreasureData()
        {
            _coins = 0;
            _gems = 0;
            _supplies = 0;
            _materials = 0;
            _items = new List<Item>();
        }
        
        public void AddItem(Item item)
        {
            _items.Add(item);
        }

        public void SetTreasureChest(TreasureChest treasureChest)
        {
            _treasureChest = treasureChest;
        }
    }
}