using System.Collections;
using System.Collections.Generic;
using Descending.Core;
using Descending.Equipment;
using UnityEngine;

namespace Descending.Enemies
{
    public class EnemyInventory : MonoBehaviour
    {
        [SerializeField] private Item[] _equipment = null;
        //[SerializeField] private Item[] _accessories = null;

        [SerializeField] private Transform _rightHandMount = null;
        [SerializeField] private Transform _leftHandMount = null;
        //[SerializeField] private Transform _backMount = null;
        
        public void Setup(EnemyDefinition definition)
        {
            _equipment = new Item[(int) EquipmentSlots.Number];
            
            for (int i = 0; i < definition.Equipment.Count; i++)
            {
                _equipment[i] = null;
                EquipItem(ItemGenerator.GenerateItem(definition.Equipment[i]));
            }
        }
        
        public void EquipItem(Item item)
        {
            _equipment[(int) item.ItemDefinition.EquipmentSlot] = new Item(item);
            if (item.ItemDefinition.Hands == Hands.Right)
            {
                _rightHandMount.ClearTransform();
                GameObject clone = item.SpawnItemModel(_rightHandMount, 0);
                //_animationController.SetOverride(item.GetWeaponData().AnimatorOverride);
            }
            else if (item.ItemDefinition.Hands == Hands.Left)
            {
                _leftHandMount.ClearTransform();
                GameObject clone = item.SpawnItemModel(_leftHandMount, 0);
                //_animationController.SetOverride(item.GetWeaponData().AnimatorOverride);
            }
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
}