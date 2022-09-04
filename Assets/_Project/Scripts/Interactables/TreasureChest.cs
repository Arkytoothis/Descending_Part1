using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using ScriptableObjectArchitecture;
using UnityEngine;

namespace Descending.Interactables
{
    public class TreasureChest : Interactable
    {
        [SerializeField] private MMF_Player _player = null;
        [SerializeField] private TreasureData _treasureData = null;

        [SerializeField] private TreasureDataEvent onInteractWithTreasure = null;
        [SerializeField] private ItemEvent onLootItem = null;
        [SerializeField] private IntEvent onLootCoins = null;
        [SerializeField] private IntEvent onLootGems = null;
        [SerializeField] private IntEvent onLootSupplies = null;
        [SerializeField] private IntEvent onLootMaterials = null;
        
        private bool _isOpen = false;
        
        public override void Interact()
        {
            if (_player.IsPlaying == true) return;
            
            if (_isOpen == false)
            {
                Open();
            }
            else
            {
                Close();
            }
        }

        private void Start()
        {
            Generate();
        }

        public void Generate()
        {
            _treasureData = TreasureGenerator.GenerateTreasure(0);
            _treasureData.SetTreasureChest(this);
        }

        private void Open()
        {
            //Debug.Log("Opening");
            _player.FeedbacksList[0].Play(transform.position);
            _isOpen = true;

            StartCoroutine(DelayedInteraction(0.5f));
        }

        private void Close()
        {
            //Debug.Log("Closing");
            _player.FeedbacksList[1].Play(transform.position);
            _isOpen = false;
        }

        private IEnumerator DelayedInteraction(float delay)
        {
            yield return new WaitForSeconds(delay);
            
            onInteractWithTreasure.Invoke(_treasureData);
        }

        public void Loot()
        {
            onLootCoins.Invoke(_treasureData.Coins);
            onLootGems.Invoke(_treasureData.Gems);
            onLootSupplies.Invoke(_treasureData.Supplies);
            onLootMaterials.Invoke(_treasureData.Materials);

            for (int i = 0; i < _treasureData.Items.Count; i++)
            {
                onLootItem.Invoke(_treasureData.Items[i]);
            }
            
            Destroy(gameObject);
        }
    }
}