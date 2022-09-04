using Descending.Characters;
using Descending.Core;
using System.Collections.Generic;
using System.Text;
using Descending.Attributes;
using Descending.Enemies;
using Descending.Gui;
using UnityEngine;

namespace Descending.Abilities
{
    [System.Serializable]
    public class DamageEffect : AbilityEffect
    {
        [SerializeField] private DamageTypeDefinition _damageType = null;
        [SerializeField] private AttributeDefinition _attribute = null;
        [SerializeField] private int _minimumValue = 0;
        [SerializeField] private int _maximumValue = 0;

        public DamageTypeDefinition DamageType => _damageType;
        public AttributeDefinition Attribute => _attribute;
        public int MinimumValue => _minimumValue;
        public int MaximumValue => _maximumValue;

        public override string GetTooltipText()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Causes ").Append(_minimumValue).Append(" - ").Append(_maximumValue).Append(" ").Append(_damageType.Name).Append(" damage\n");

            return sb.ToString();
        }

        public override void Process(GameEntity user, List<GameEntity> targets)
        {
            if (_affects == AbilityEffectAffects.User)
            {
                // if (user.GetType() == typeof(PlayerCharacter))
                // {
                //     PlayerCharacter pc = (PlayerCharacter)user;
                //
                //     if (pc != null)
                //     {
                //         int amount = Random.Range(_minimumValue, _maximumValue + 1);
                //         pc.TakeDamage(_attribute, _damageType, amount, false);
                //     }
                // }
                // else if (user.GetType() == typeof(Enemy))
                // {
                //     Enemy enemy = (Enemy)user;
                //
                //     if (enemy != null)
                //     {
                //         int amount = Random.Range(_minimumValue, _maximumValue + 1);
                //         enemy.TakeDamage(_attribute, _damageType, amount, false);
                //     }
                // }
            }
            else if (_affects == AbilityEffectAffects.Target)
            {
                foreach (GameEntity entity in targets)
                {
                    if (entity.GetType() == typeof(Hero))
                    {
                        Hero hero = (Hero)entity;
                
                        if (hero != null)
                        {
                            int amount = Random.Range(_minimumValue, _maximumValue + 1);
                            if(MessageHandler.Instance == null) Debug.Log("MessageHandler.instance == null");
                            MessageHandler.Instance.DisplayMessage(new GameMessage(hero.GetName() + " takes " + amount + " damage"));
                            hero.Damage(_attribute.Key, amount, _damageType);
                        }
                    }
                    else if (entity.GetType() == typeof(Enemy))
                    {
                        Enemy enemy = (Enemy)entity;
                
                        if (enemy != null)
                        {
                            int amount = Random.Range(_minimumValue, _maximumValue + 1);
                            if(MessageHandler.Instance == null) Debug.Log("MessageHandler.instance == null");
                            MessageHandler.Instance.DisplayMessage(new GameMessage(enemy.GetName() + " takes " + amount + " damage"));
                            enemy.Damage(_attribute.Key, amount, _damageType);
                        }
                    }
                }
            }
        }
    }
}