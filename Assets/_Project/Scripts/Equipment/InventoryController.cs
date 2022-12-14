using System.Collections;
using System.Collections.Generic;
using System.Data;
using Descending.Attributes;
using Descending.Characters;
using Descending.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace Descending.Equipment
{
    public class InventoryController : MonoBehaviour
    {
        public const int MAX_ACCESSORY_SLOTS = 6;
        
        [SerializeField] private Item[] _equipment = null;
        [SerializeField] private Item[] _accessories = null;
        
        [SerializeField] private int _accessorySlots = 2;
        [SerializeField] private BodyRenderer _portraitBody = null;

        private Genders _gender = Genders.None;
        private Item _currentWeapon = null;
        
        public Item[] Equipment => _equipment;
        public Item[] Accessories => _accessories;
        public int AccessorySlots => _accessorySlots;

        public void Setup(BodyRenderer portraitBody, Genders gender, RaceDefinition race, ProfessionDefinition profession)
        {
            _portraitBody = portraitBody;
            
            _gender = gender;
            
            _equipment = new Item[(int) EquipmentSlots.Number];
            _accessories = new Item[MAX_ACCESSORY_SLOTS];
            
            for (int i = 0; i < (int)EquipmentSlots.Number; i++)
            {
                _equipment[i] = null;
            }

            for (int i = 0; i < MAX_ACCESSORY_SLOTS; i++)
            {
                _accessories[i] = null;
            }
            
            for (int i = 0; i < profession.StartingItems.Count; i++)
            {
                Item item = ItemGenerator.GenerateItem(profession.StartingItems[i]);
                EquipItem(item);
            }

            if (profession.PrefersRanged == false)
            {
                _portraitBody.EquipWeapon(_equipment[(int) EquipmentSlots.Melee_Weapon]);
                _portraitBody.EquipWeapon(_equipment[(int) EquipmentSlots.Off_Weapon]);
                _currentWeapon = _equipment[(int) EquipmentSlots.Melee_Weapon];
            }
            else
            {
                _portraitBody.EquipWeapon(_equipment[(int) EquipmentSlots.Ranged_Weapon]);
                _currentWeapon = _equipment[(int) EquipmentSlots.Ranged_Weapon];
            }
        }
        
        public void LoadData(BodyRenderer portraitBody, Genders gender, RaceDefinition race, ProfessionDefinition profession, InventorySaveData saveData)
        {
            _portraitBody = portraitBody;
            _gender = gender;
            _equipment = new Item[saveData.EquippedItems.Length];
            _accessories = new Item[saveData.Accessories.Length];
            
            for (int i = 0; i < saveData.EquippedItems.Length; i++)
            {
                _equipment[i] = new Item(saveData.EquippedItems[i]);
            }
        
            for (int i = 0; i < saveData.Accessories.Length; i++)
            {
                _accessories[i] = new Item(saveData.Accessories[i]);
            }
            
            for (int i = 0; i < saveData.EquippedItems.Length; i++)
            {
                if (saveData.EquippedItems[i].Key == "" || saveData.EquippedItems[i].ItemDefinition.Key == "") continue;
                
                EquipItem(saveData.EquippedItems[i]);
            }

            if (profession.PrefersRanged == false)
            {
                _portraitBody.EquipWeapon(_equipment[(int) EquipmentSlots.Melee_Weapon]);
                _portraitBody.EquipWeapon(_equipment[(int) EquipmentSlots.Off_Weapon]);
                _currentWeapon = _equipment[(int) EquipmentSlots.Melee_Weapon];
            }
            else
            {
                _portraitBody.EquipWeapon(_equipment[(int) EquipmentSlots.Ranged_Weapon]);
                _currentWeapon = _equipment[(int) EquipmentSlots.Ranged_Weapon];
            }
        }

        public void EquipItem(Item item)
        {
            if (_equipment[(int) item.ItemDefinition.EquipmentSlot] == null)
            {
                _equipment[(int) item.ItemDefinition.EquipmentSlot] = new Item(item);
                _portraitBody.EquipItem(item);
            }
            else
            {
                _equipment[(int) item.ItemDefinition.EquipmentSlot] = new Item(item);
                _portraitBody.EquipItem(item);
            }
        }

        public void UnequipItem(Item item)
        {
            
        }

        public Item GetCurrentWeapon()
        {
            return _currentWeapon;
        }
        
        public Item GetMeleeWeapon()
        {
            return _equipment[(int) EquipmentSlots.Melee_Weapon];
        }

        public Item GetRangedWeapon()
        {
            return _equipment[(int) EquipmentSlots.Ranged_Weapon];
        }
    }

    [System.Serializable]
    public class InventorySaveData
    {
        [SerializeField] private Item[] _equippedItems = null;
        [SerializeField] private Item[] _accessories = null;
        [SerializeField] private int _accessorySlots = 0;

        public Item[] EquippedItems => _equippedItems;
        public Item[] Accessories => _accessories;
        public int AccessorySlots => _accessorySlots;

        public InventorySaveData(Hero hero)
        {
            _accessorySlots = hero.Inventory.AccessorySlots;
            _equippedItems = new Item[hero.Inventory.Equipment.Length];
            _accessories = new Item[hero.Inventory.Accessories.Length];
            
            for (int i = 0; i < hero.Inventory.Equipment.Length; i++)
            {
                _equippedItems[i] = new Item(hero.Inventory.Equipment[i]);
            }

            for (int i = 0; i < hero.Inventory.Accessories.Length; i++)
            {
                _accessories[i] = new Item(hero.Inventory.Accessories[i]);
            }
        }
    }
}