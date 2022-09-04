using System;
using System.Collections;
using System.Collections.Generic;
using Descending.Core;
using Descending.Equipment;
using DG.Tweening;
using ScriptableObjectArchitecture;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Descending.Gui
{
    public class TreasureItemWidget : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private Image _icon = null;
        [SerializeField] private Image _border = null;
        [SerializeField] private TMP_Text _stackSizeLabel = null;
        [SerializeField] private Sprite _blankIcon = null;

        [SerializeField] private ItemEvent onDisplayItemTooltip = null;

        private RectTransform _rectTransform = null;
        private Item _item = null;
        private int _index = -1;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public void Setup()
        {
            Clear();
        }

        public void SetItem(Item item, int index)
        {
            _index = index;
            _item = item;
            _icon.sprite = _item.Icon;
            _stackSizeLabel.text = item.StackSize.ToString();

            _border.color = item.GetRarityColor();
        }

        public void Clear()
        {
            _item = null;
            _icon.sprite = _blankIcon;
            _stackSizeLabel.text = "";
            _border.color = Database.instance.Rarities.GetRarity(0).Color;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _rectTransform.DOScale(1.1f, 0.1f);
            onDisplayItemTooltip.Invoke(_item);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _rectTransform.DOScale(1f, 0.1f);
            onDisplayItemTooltip.Invoke(null);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(_item == null) return;

            if (eventData.button == PointerEventData.InputButton.Right)
            {
                
            }
        }
    }
}