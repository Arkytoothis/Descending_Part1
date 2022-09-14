using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descending.Gui
{
    [System.Serializable]
    public class FloatingTextParameters
    {
        public string Text = "";
        public int Index = -1;
        public int FontSize = 0;

        public FloatingTextParameters(string text, int index, int fontSize)
        {
            Text = text;
            Index = index;
            FontSize = fontSize;
        }
    }
}