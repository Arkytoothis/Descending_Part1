using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Descending.Core;
using Descending.Equipment;
using ScriptableObjectArchitecture;
using Sirenix.Serialization;
using UnityEngine;

namespace Descending.Party
{
    public class StockpileController : MonoBehaviour
    {
        [SerializeField] private int _stockpileSlots = 72;
        [SerializeField] private List<Item> _items = null;

        [SerializeField] private StockpileControllerEvent onSyncStockpile = null;

        public int StockpileSlots => _stockpileSlots;
        public List<Item> Items => _items;

        public void SyncStockpile()
        {
            onSyncStockpile.Invoke(this);    
        }
        
        public void PickupItem(Item item)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i] == null || _items[i].Name == "")
                {
                    _items[i] = new Item(item);
                    break;
                }
                else
                {
                    if (_items[i].ItemDefinition.Key == item.ItemDefinition.Key && item.ItemDefinition.Stackable == true)
                    {
                        _items[i].AddToStack(1);
                        break;
                    }
                }
            }
            
            SyncStockpile();
        }

        public bool HasItem(ItemDefinition item, int amount)
        {
            bool hasItem = false;

            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i] != null && _items[i].ItemDefinition == item && _items[i].StackSize >= amount)
                {
                    hasItem = true;
                    break;
                }
            }

            return hasItem;
        }

        public void RemoveItem(ItemDefinition item, int amount)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i] != null && _items[i].ItemDefinition == item && _items[i].StackSize >= amount)
                {
                    _items[i].StackSize -= amount;

                    if (_items[i].StackSize <= 0)
                    {
                        _items[i] = null;
                    }
                    
                    break;
                }
            }
            
            SyncStockpile();
        }

        public void ClearStockpileSlot(int index)
        {
            _items[index] = null;
            SyncStockpile();
        }

        public void OnLootItem(Item item)
        {
            PickupItem(item);
        }

        public void LoadData()
        {
            if (File.Exists(Database.instance.StockpileFilePath))
            {
                byte[] bytes = File.ReadAllBytes(Database.instance.StockpileFilePath);
                var saveData = SerializationUtility.DeserializeValue<StockpileSaveData>(bytes, DataFormat.JSON);

                _items = saveData.Items;
                SyncStockpile();
            }
        }
        
        public void SaveData()
        {
            StockpileSaveData saveData = new StockpileSaveData(_items);
            byte[] bytes = SerializationUtility.SerializeValue(saveData, DataFormat.JSON);
            File.WriteAllBytes(Database.instance.StockpileFilePath, bytes);
        }
    }

    [System.Serializable]
    public class StockpileSaveData
    {
        [SerializeField] private List<Item> _items = null;

        public List<Item> Items => _items;
        
        public StockpileSaveData(List<Item> items)
        {
            _items = items;
        }
        
        public StockpileSaveData()
        {
        }
        
    }
}