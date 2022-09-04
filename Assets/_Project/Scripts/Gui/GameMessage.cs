using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descending.Gui
{
    [System.Serializable]
    public class GameMessage
    {
        [SerializeField] private string _text = "";

        public string Text => _text;

        public GameMessage(string text)
        {
            _text = text;
        }
    }
}