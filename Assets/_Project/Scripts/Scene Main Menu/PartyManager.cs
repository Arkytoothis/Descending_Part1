using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Descending.Attributes;
using Descending.Characters;
using Descending.Core;
using Descending.Equipment;
using Descending.Party;
using Sirenix.Serialization;
using UnityEngine;

namespace Descending.Scene_MainMenu
{
    public class PartyManager : MonoBehaviour
    {
        [SerializeField] private PartyData _partyData = null;
        [SerializeField] private List<Transform> _partyMounts = null;
        public PartyData PartyData => _partyData;
        
        public void Setup()
        {
            _partyData = new PartyData();
        }

        public void GenerateParty()
        {
            _partyData.Heroes.Clear();
            _partyData.AddHero(SpawnHero(0, Utilities.GetRandomGender(), Utilities.GetRandomRace(), Database.instance.Profession.GetProfession("Soldier")), _partyMounts[0]);  
            _partyData.AddHero(SpawnHero(1, Utilities.GetRandomGender(), Utilities.GetRandomRace(), Database.instance.Profession.GetProfession("Mercenary")), _partyMounts[1]); 
            _partyData.AddHero(SpawnHero(2, Utilities.GetRandomGender(), Utilities.GetRandomRace(), Database.instance.Profession.GetProfession("Thief")), _partyMounts[2]); 
            _partyData.AddHero(SpawnHero(3, Utilities.GetRandomGender(), Utilities.GetRandomRace(), Database.instance.Profession.GetProfession("Scout")), _partyMounts[3]); 
            _partyData.AddHero(SpawnHero(4, Utilities.GetRandomGender(), Utilities.GetRandomRace(), Database.instance.Profession.GetProfession("Acolyte")), _partyMounts[4]); 
            _partyData.AddHero(SpawnHero(5, Utilities.GetRandomGender(), Utilities.GetRandomRace(), Database.instance.Profession.GetProfession("Apprentice")), _partyMounts[5]);   
        }

        public void GenerateFavoriteParty()
        {
            _partyData.Heroes.Clear();
            _partyData.AddHero(SpawnHero(0, Utilities.GetRandomGender(), Database.instance.Races.GetRace("Half Orc"), Database.instance.Profession.GetProfession("Soldier")), _partyMounts[0]);  
            _partyData.AddHero(SpawnHero(1, Utilities.GetRandomGender(), Database.instance.Races.GetRace("Imperial"), Database.instance.Profession.GetProfession("Mercenary")), _partyMounts[1]); 
            _partyData.AddHero(SpawnHero(2, Utilities.GetRandomGender(), Database.instance.Races.GetRace("Halfling"), Database.instance.Profession.GetProfession("Thief")), _partyMounts[2]); 
            _partyData.AddHero(SpawnHero(3, Utilities.GetRandomGender(), Database.instance.Races.GetRace("Wild Elf"), Database.instance.Profession.GetProfession("Scout")), _partyMounts[3]); 
            _partyData.AddHero(SpawnHero(4, Utilities.GetRandomGender(), Database.instance.Races.GetRace("Mountain Dwarf"), Database.instance.Profession.GetProfession("Acolyte")), _partyMounts[4]); 
            _partyData.AddHero(SpawnHero(5, Utilities.GetRandomGender(), Database.instance.Races.GetRace("Valarian"), Database.instance.Profession.GetProfession("Apprentice")), _partyMounts[5]);   
        }

        private Hero SpawnHero(int index, Genders gender, RaceDefinition race, ProfessionDefinition profession)
        {
            _partyMounts[index].ClearTransform();
                
            Hero hero = HeroBuilder.BuildHero(null, gender, race, profession, index);
            hero.transform.SetParent(_partyMounts[index], false);

            return hero;
        }

        public void Save()
        {
            SaveResources();
            SaveStockpile();
            SaveData();
        }

        private void SaveResources()
        {
            ResourceSaveData saveData = new ResourceSaveData(100, 0, 0, 10);
            byte[] bytes = SerializationUtility.SerializeValue(saveData, DataFormat.JSON);
            File.WriteAllBytes(Database.instance.ResourceDataFilePath, bytes);
        }
        
        private void SaveStockpile()
        {
            List<Item> items = new List<Item>();
            for (int i = 0; i < 72; i++)
            {
                items.Add(null);
            }
            StockpileSaveData saveData = new StockpileSaveData(items);
            byte[] bytes = SerializationUtility.SerializeValue(saveData, DataFormat.JSON);
            File.WriteAllBytes(Database.instance.StockpileFilePath, bytes);
        }
        
        private void SaveData()
        {
            _partyData.SavePartyData();

            SceneLoadData loadData = new SceneLoadData(SceneTransitionTypes.Main_Menu_Play, SceneBuildTypes.Generate);
            byte[] bytes = SerializationUtility.SerializeValue(loadData, DataFormat.JSON);
            File.WriteAllBytes(Database.instance.SceneLoadFilePath, bytes);
        }
        
        public void Load()
        {
            _partyData = new PartyData();
	
            LoadResources();
            LoadParty();
        }

        private void LoadResources()
        {
            if (!File.Exists(Database.instance.ResourceDataFilePath)) return;
            
            byte[] bytes = File.ReadAllBytes(Database.instance.ResourceDataFilePath);
            var saveData = SerializationUtility.DeserializeValue<ResourceSaveData>(bytes, DataFormat.Binary);
        }

        private void LoadParty()
        {
            if (!File.Exists(Database.instance.PartyDataFilePath)) return;

            byte[] bytes = File.ReadAllBytes(Database.instance.PartyDataFilePath);
            List<HeroSaveData> saveData = SerializationUtility.DeserializeValue<List<HeroSaveData>>(bytes, DataFormat.JSON);
        }
    } 
}