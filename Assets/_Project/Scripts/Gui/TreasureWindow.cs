using System.Collections;
using System.Collections.Generic;
using Descending.Core;
using Descending.Interactables;
using ScriptableObjectArchitecture;
using TMPro;
using UnityEngine;

namespace Descending.Gui
{
    public class TreasureWindow : GameWindow
    {
        [SerializeField] private TMP_Text _coinsLabel = null;
        [SerializeField] private TMP_Text _gemsLabel = null;
        [SerializeField] private TMP_Text _suppliesLabel = null;
        [SerializeField] private TMP_Text _materialsLabel = null;
        [SerializeField] private GameObject _itemWidgetPrefab = null;
        [SerializeField] private Transform _itemWidgetsParent = null;
        
        [SerializeField] private LookModesEvent onSetLookMode = null;
        [SerializeField] private BoolEvent onPartyLookEnabled = null;
        [SerializeField] private BoolEvent onPartyMovementEnabled = null;
        
        private List<TreasureItemWidget> _widgets = null;
        [SerializeField] private TreasureData _treasureData = null;

        public override void Setup()
        {
            Close();
            _widgets = new List<TreasureItemWidget>();
        }

        public override void Open()
        {
            _isOpen = true;
            _container.SetActive(true);
            onSetLookMode.Invoke(LookModes.Cursor);
            onPartyLookEnabled.Invoke(false);
            onPartyMovementEnabled.Invoke(false);

            if (_treasureData != null)
            {
                _coinsLabel.SetText(_treasureData.Coins.ToString());
                _gemsLabel.SetText(_treasureData.Gems.ToString());
                _suppliesLabel.SetText(_treasureData.Supplies.ToString());
                _materialsLabel.SetText(_treasureData.Materials.ToString());

                _widgets.Clear();
                _itemWidgetsParent.ClearTransform();
                
                for (int i = 0; i < _treasureData.Items.Count; i++)
                {
                    GameObject clone = Instantiate(_itemWidgetPrefab, _itemWidgetsParent);
                    TreasureItemWidget widget = clone.GetComponent<TreasureItemWidget>();
                    widget.SetItem(_treasureData.Items[i], i);
                }
            }
        }

        public override void Close()
        {
            _isOpen = false;
            _container.SetActive(false);
            onSetLookMode.Invoke(LookModes.Look);
            onPartyLookEnabled.Invoke(true);
            onPartyMovementEnabled.Invoke(true);
        }

        public void OnLootButtonClick()
        {
            LootAll();
            Close();
        }

        private void LootAll()
        {
            _treasureData.TreasureChest.Loot();
        }
        
        public void ExitButtonClick()
        {
            Close();
        }

        public void OnSetTreasureData(TreasureData treasureData)
        {
            _treasureData = treasureData;
        }
    }
}
