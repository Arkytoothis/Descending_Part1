using System.Collections;
using System.Collections.Generic;
using Descending.Combat;
using Descending.Encounters;
using Descending.Gui;
using Descending.Interactables;
using Descending.Party;
using ScriptableObjectArchitecture;
using UnityEngine;

namespace Descending.Gui
{
    public class GuiManager : MonoBehaviour
    {
        [SerializeField] private WindowManager _windowManager = null;
        [SerializeField] private GameObject _partyPanelPrefab = null;
        [SerializeField] private GameObject _resourcesPanelPrefab = null;
        [SerializeField] private GameObject _timePanelPrefab = null;
        [SerializeField] private GameObject _combatPanelPrefab = null;
        [SerializeField] private GameObject _minimapPanelPrefab = null;
        [SerializeField] private GameObject _tooltipPrefab = null;
        
        [SerializeField] private TextManager_UI _textManager_UI = null;

        [SerializeField] private BoolEvent onEndCombat = null;

        //private PartyManager _partyManager = null;
        private PartyPanel _partyPanel = null;
        private ResourcesPanel _resourcesPanel = null;
        private TimePanel _timePanel = null;
        private CombatPanel _combatPanel = null;
        private MinimapPanel _minimapPanel = null;
        private Tooltip _tooltip = null;
        
        public void Setup()
        {
            SetupPartyPanel();
            SetupResourcesPanel();
            SetupTimePanel();
            SetupCombatPanel();
            SetupMinimapPanel();
            SetupTooltip();
            
            _windowManager.Setup();
            _windowManager.transform.SetAsLastSibling();
            _textManager_UI.transform.SetAsLastSibling();
            
            WorldMode();
        }

        private void SetupPartyPanel()
        {
            GameObject clone = Instantiate(_partyPanelPrefab, transform);
            _partyPanel = clone.GetComponent<PartyPanel>();
            _partyPanel.Setup();
        }

        private void SetupResourcesPanel()
        {
            GameObject clone = Instantiate(_resourcesPanelPrefab, transform);
            _resourcesPanel = clone.GetComponent<ResourcesPanel>();
            _resourcesPanel.Setup();
        }

        private void SetupTimePanel()
        {
            GameObject clone = Instantiate(_timePanelPrefab, transform);
            _timePanel = clone.GetComponent<TimePanel>();
            _timePanel.Setup();
        }

        private void SetupCombatPanel()
        {
            GameObject clone = Instantiate(_combatPanelPrefab, transform);
            _combatPanel = clone.GetComponent<CombatPanel>();
            _combatPanel.Setup(this);
        }

        private void SetupMinimapPanel()
        {
            GameObject clone = Instantiate(_minimapPanelPrefab, transform);
            _minimapPanel = clone.GetComponent<MinimapPanel>();
            _minimapPanel.Setup();
        }

        private void SetupTooltip()
        {
            GameObject clone = Instantiate(_tooltipPrefab, null);
            _tooltip = clone.GetComponentInChildren<Tooltip>();
            _tooltip.Setup();
        }

        public void OnCombatStarted(CombatParameters parameters)
        {
            _combatPanel.StartCombat(parameters.PartyManager, parameters.Encounter);
            CombatMode();
        }

        public void OnInteractWithTreasure(TreasureData data)
        {
            _windowManager.OpenWindow((int)GameWindows.Treasure);
        }

        public void EndCombat()
        {
            WorldMode();
            onEndCombat.Invoke(true);
        }

        private void WorldMode()
        {
            _partyPanel.Show();
            _timePanel.Show();
            _resourcesPanel.Show();
            _combatPanel.Hide();
        }
        
        private void CombatMode()
        {
            _partyPanel.Hide();
            _timePanel.Hide();
            _resourcesPanel.Hide();
            _combatPanel.Show();
        }
    }
}