using System;
using System.Collections;
using System.Collections.Generic;
using Descending.Characters;
using Descending.Core;
using Descending.Gui;
using Descending.Party;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Descending.Gui
{
    public class PartyPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _container = null;
        [SerializeField] private GameObject _heroWidgetPrefab = null;
        //[SerializeField] private GameObject _actionWidgetPrefab = null;
        [SerializeField] private Transform _heroWidgetsParent = null;
        //[SerializeField] private Transform _actionWidgetsParent = null;
        //[SerializeField] private Transform _accessoryWidgetsParent = null;
        [SerializeField] private TMP_Text _messageLabel = null;
        [SerializeField] private List<ActionWidget> _actionWidgets = null;
        //[SerializeField] private List<ActionWidget> _accessoryWidgets = null;
        
        private PartyData _partyData = null;
        private List<HeroWidget> _heroWidgets = null;
        
        public void Setup()
        {
            _heroWidgets = new List<HeroWidget>();
        }

        public void Show()
        {
            _container.SetActive(true);
        }

        public void Hide()
        {
            _container.SetActive(false);
        }

        public void OnSyncPartyData(PartyData partyData)
        {
            //Debug.Log("Party Synced - PartyPanel");
            _partyData = partyData;

            if (_partyData == null) return;

            _heroWidgets.Clear();
            _heroWidgetsParent.ClearTransform();
            
            for (int i = 0; i < _partyData.Heroes.Count; i++)
            {
                GameObject clone = Instantiate(_heroWidgetPrefab, _heroWidgetsParent);
                HeroWidget widget = clone.GetComponent<HeroWidget>();
                widget.SetHero(this, _partyData.Heroes[i], i);
                
                _heroWidgets.Add(widget);
            }
            
            SelectHero(0);
        }

        public void OnSyncHero(int index)
        {
            _heroWidgets[index].SyncData();
        }

        public void SelectHero(int index)
        {
            for (int i = 0; i < _heroWidgets.Count; i++)
            {
                _heroWidgets[i].Deselect();
            }
            
            _heroWidgets[index].Select();
            
            LoadActions(_partyData.Heroes[index]);
            LoadAccessories(_partyData.Heroes[index]);
        }

        public void OnSetLeader(int index)
        {
            SelectHero(index);
        }

        public void OnSelectHero(int index)
        {
            SelectHero(index);
        }

        private void LoadActions(Hero hero)
        {
            for (int i = 0; i < 12; i++)
            {
                if (i < hero.Abilities.ActionConfigs.Count && hero.Abilities.ActionConfigs[i] != null)
                {
                    _actionWidgets[i].SetAbility(hero, hero.Abilities.ActionConfigs[i].Ability);
                }
                else
                {
                    _actionWidgets[i].Clear();
                }
            }
        }

        private void LoadAccessories(Hero hero)
        {
            
        }

        public void OnDisplayMessage(GameMessage message)
        {
            _messageLabel.DOFade(1f, 0f);
            _messageLabel.SetText(message.Text);
            _messageLabel.DOFade(0f, 3f);
        }

        public void OnDisplayDamageText(FloatingTextParameters parameters)
        {
            TextManager_UI.instance.DisplayUIText(parameters.Text, _heroWidgets[parameters.Index].TextTransform.position, parameters.FontSize);
        }
    }
}
