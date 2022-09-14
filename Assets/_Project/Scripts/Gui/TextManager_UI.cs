using System;
using System.Collections;
using System.Collections.Generic;
using Descending.Core;
using UnityEngine;
using ScriptableObjectArchitecture;

namespace Descending.Gui
{
    public class TextManager_UI : Singleton<TextManager_UI>
    {
        [SerializeField] private GameObject _textPrefab = null;
        [SerializeField] private Transform _textParent = null;

        private void Awake()
        {
            Reload();
        }

        public void DisplayUIText(string text, Vector3 position, int fontSize)
        {
            GameObject clone = Instantiate(_textPrefab, _textParent);
            clone.transform.position = position;

            PopupText popupText = clone.GetComponent<PopupText>();
            popupText.Setup(text, fontSize);
        }
    }
}