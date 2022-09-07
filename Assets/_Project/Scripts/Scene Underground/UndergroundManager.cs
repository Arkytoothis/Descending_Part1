using System;
using System.Collections;
using System.Collections.Generic;
using Descending.Core;
using Descending.Encounters;
using Descending.Equipment;
using Descending.Gui;
using Descending.Interactables;
using Descending.Party;
using DunGen;
using UnityEngine;

namespace Descending.Scene_Underground
{
    public class UndergroundManager : MonoBehaviour
    {
        [SerializeField] private Database _database = null;
        [SerializeField] private GameObject _guiPrefab = null;
        [SerializeField] private PartyManager _partyManager = null;
        [SerializeField] private PortraitRoom _portraitRoom = null;
        [SerializeField] private RuntimeDungeon _runtimeDungeon = null;
        [SerializeField] private EncounterManager _encounterManager = null;
        
        private GuiManager _gui = null;
        
        private void Awake()
        {
            _database.Setup();
            ItemGenerator.Setup();
            EncounterGenerator.Setup();
            TreasureGenerator.Setup();
            _encounterManager.Setup();
        }

        private void Start()
        {
            SetupGui();
            _runtimeDungeon.Generate();
            SetupParty();
        }

        public void SetupParty()
        {
            _partyManager.Setup();
            _portraitRoom.Setup(_partyManager.PartyData);
            _partyManager.SyncPartyData();
        }
        
        private void SetupGui()
        {
            GameObject clone = Instantiate(_guiPrefab, null);
            _gui = clone.GetComponent<GuiManager>();
            _gui.Setup();
        }
    }
}