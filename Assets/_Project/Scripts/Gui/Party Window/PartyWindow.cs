using System.Collections;
using System.Collections.Generic;
using DarkTonic.MasterAudio;
using Descending.Characters;
using Descending.Core;
using Descending.Party;
using ScriptableObjectArchitecture;
using UnityEngine;

namespace Descending.Gui.Party_Window
{
    public class PartyWindow : GameWindow
    {
        [SerializeField] private GameObject _partyWidgetPrefab = null;
        [SerializeField] private Transform _partyWidgetsParent = null;
        [SerializeField] private DetailsPanel _detailsPanel = null;
        [SerializeField] private AttributesPanel _attributesPanel = null;
        [SerializeField] private SkillsPanel _skillsPanel = null;
        [SerializeField] private PortraitPanel _portraitPanel = null;
        [SerializeField] private EquipmentPanel _equipmentPanel = null;
        [SerializeField] private AbilitiesPanel _abilitiesPanel = null;
        [SerializeField] private ActionsPanel _actionsPanel = null;

        [SerializeField] private ItemEvent onHideTooltip = null;
        [SerializeField] private LookModesEvent onSetLookMode = null;
        [SerializeField] private BoolEvent onPartyLookEnabled = null;
        [SerializeField] private BoolEvent onPartyMovementEnabled = null;
        [SerializeField] private BoolEvent onSetPartyWindowOpenState = null;
        
        private List<PartyWidget> _partyWidgets = null;
        private PartyData _partyData = null;
        
        public override void Setup()
        {
            Close();
        }

        public override void Open()
        {
            SelectHero(_partyData.Heroes[0]);
            _isOpen = true;
            onSetPartyWindowOpenState.Invoke(_isOpen);
            _container.SetActive(true);
            onSetLookMode.Invoke(LookModes.Cursor);
            onPartyLookEnabled.Invoke(false);
            onPartyMovementEnabled.Invoke(false);
            MasterAudio.PlaySound(_openSound, 1f);
        }

        public override void Close()
        {
            _isOpen = false;
            onSetPartyWindowOpenState.Invoke(_isOpen);
            _container.SetActive(false);
            onHideTooltip.Invoke(null);
            onSetLookMode.Invoke(LookModes.Look);
            onPartyLookEnabled.Invoke(true);
            onPartyMovementEnabled.Invoke(true);
            MasterAudio.PlaySound(_closeSound, 1f);
        }

        public void CloseButtonClick()
        {
            Close();
        }
        
        public void OnSyncPartyData(PartyData partyData)
        {
            _partyData = partyData;
            if (_partyData == null) return;

            _partyWidgets = new List<PartyWidget>();
            _partyWidgetsParent.ClearTransform();

            for (int i = 0; i < _partyData.Heroes.Count; i++)
            {
                GameObject clone = Instantiate(_partyWidgetPrefab, _partyWidgetsParent);
                PartyWidget widget = clone.GetComponent<PartyWidget>();
                widget.DisplayHero(this, _partyData.Heroes[i]);
                _partyWidgets.Add(widget);
            }
        }

        public void SelectHero(Hero hero)
        {
            for (int i = 0; i < _partyWidgets.Count; i++)
            {
                _partyWidgets[i].Deselect();
            }
            
            _partyWidgets[hero.HeroData.ListIndex].Select();
            
            _detailsPanel.SelectHero(hero);
            _attributesPanel.SelectHero(hero);
            _skillsPanel.SelectHero(hero);
            _portraitPanel.SelectHero(hero);
            _equipmentPanel.SelectHero(hero);
            _abilitiesPanel.SelectHero(hero);
            _actionsPanel.SelectHero(hero);
        }
    }
}
