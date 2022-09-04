using Descending.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descending.Abilities
{
    [System.Serializable]
    public class UseAbilityData
    {
        [SerializeField] private GameEntity _user = null;
        [SerializeField] private List<GameEntity> _targets = null;
        [SerializeField] private Ability _ability = null;

        public Ability Ability => _ability;
        public GameEntity User => _user;
        public List<GameEntity> Targets => _targets;

        public UseAbilityData(GameEntity user, List<GameEntity> targets, Ability ability)
        {
            _user = user;
            _targets = targets;
            _ability = ability;
        }

        public void SetTargets(List<GameEntity> targets)
        {
            _targets = new List<GameEntity>();
            for (int i = 0; i < targets.Count; i++)
            {
                _targets.Add(targets[i]);
            }
        }
    }
}