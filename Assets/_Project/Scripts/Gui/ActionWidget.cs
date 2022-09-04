using System;
using System.Collections;
using System.Collections.Generic;
using Descending.Abilities;
using Descending.Characters;
using Descending.Core;
using Descending.Equipment;
using ScriptableObjectArchitecture;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Descending.Gui
{
    public class ActionWidget : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private Image _icon = null;
        [SerializeField] private Image _border = null;
        [SerializeField] private TMP_Text _stackSizeLabel = null;
        [SerializeField] private Sprite _blankIcon = null;
        [SerializeField] private KeyCode _inputKey = KeyCode.None;

        [SerializeField] private AbilityEvent onDisplayAbilityTooltip = null;
        [SerializeField] private AbilityEvent onSelectAbility = null;
        [SerializeField] private AbilityEvent onUseAbility = null;
        [SerializeField] private ItemEvent onDisplayItemTooltip = null;
        
        private Ability _ability = null;
        private Item _item = null;
        private GameEntity _user = null;
        
        public void Setup()
        {
            Clear();
        }

        private void Update()
        {
            if (Input.GetKeyDown(_inputKey))
            {
                TryUseAbility();
            }
        }

        public void SetAbility(GameEntity user, Ability ability)
        {
            if (ability != null)
            {
                _user = user;
                _ability = ability;
                _item = null;
                _icon.sprite = ability.Definition.Details.Icon;
                //_stackSizeLabel.text = key;

                _border.color = Color.white;
            }
            else
            {
                Clear();
            }
        }

        public void Clear()
        {
            _ability = null;
            _item = null;
            _icon.sprite = _blankIcon;
            _stackSizeLabel.text = "";
            _border.color = Database.instance.Rarities.GetRarity(0).Color;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_ability != null)
            {
                onDisplayAbilityTooltip.Invoke(_ability);
            }
            else if (_item != null)
            {
                onDisplayItemTooltip.Invoke(_item);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            onDisplayAbilityTooltip.Invoke(null);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            TryUseAbility();
        }

        private bool TryUseAbility()
        {
            if (_ability == null) return false;
            
            if (_user.GetType() == typeof(Hero))
            {
                if (((Hero) _user).Attributes.GetVital(_ability.Definition.Details.ResourceAttribute.Key).Current <= _ability.Definition.Details.ResourceAmount)
                {
                    Debug.Log("Not Enough " + _ability.Definition.Details.ResourceAttribute.Key + " to use");
                    return false;
                }
            }

            if (_ability.Definition.Details.TargetType == TargetTypes.Self)
            {
                onUseAbility.Invoke(_ability);
            }
            else if (_ability.Definition.Details.TargetType == TargetTypes.Friend ||
                     _ability.Definition.Details.TargetType == TargetTypes.Enemy ||
                     _ability.Definition.Details.TargetType == TargetTypes.Ground)
            {
                onSelectAbility.Invoke(_ability);
            }

            return true;
        }
    }
}