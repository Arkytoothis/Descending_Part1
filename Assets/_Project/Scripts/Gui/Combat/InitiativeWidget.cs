using System.Collections;
using System.Collections.Generic;
using Descending.Characters;
using Descending.Enemies;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Descending.Gui.Combat
{
    public enum InitiativeWidgetsTypes { Hero, Enemy, Number, None }
    
    public abstract class InitiativeWidget : MonoBehaviour
    {
        [SerializeField] protected Image _background = null;
        [SerializeField] protected Image _border = null;
        [SerializeField] protected TMP_Text _nameLabel = null;
        [SerializeField] protected TMP_Text _initiativeLabel = null;
        [SerializeField] protected int _index = -1;
        [SerializeField] protected Color _baseColor = Color.white;
        [SerializeField] protected Color _hoverColor = Color.white;
        [SerializeField] protected VitalBar _lifeBar = null;
        [SerializeField] protected InitiativeWidgetsTypes _type = InitiativeWidgetsTypes.None;

        protected bool _selected = false;

        public InitiativeWidgetsTypes Type => _type;

        public abstract void Select();
        public abstract void Deselect();
        public abstract void Highlight();
        public abstract void Unhighlight();
    }
}
