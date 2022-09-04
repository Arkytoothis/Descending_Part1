using System.Collections;
using System.Collections.Generic;
using Descending.Enemies;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Descending.Gui
{
    public class EnemyEncounterWidget : MonoBehaviour
    {
        [SerializeField] private Image _icon = null;
        [SerializeField] private TMP_Text _nameLabel = null;
        [SerializeField] private TMP_Text _detailsLabel = null;

        public void Setup(Enemy enemy)
        {
            _icon.sprite = enemy.Definition.Icon;
            _nameLabel.SetText(enemy.Definition.Name);
            _detailsLabel.SetText("Lvl " +  " " + enemy.Definition.Name);
        }
    }
}
