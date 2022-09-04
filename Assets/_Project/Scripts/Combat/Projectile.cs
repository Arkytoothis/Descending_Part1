using System;
using System.Collections;
using System.Collections.Generic;
using Descending.Abilities;
using Descending.Characters;
using Descending.Core;
using Descending.Enemies;
using Descending.Equipment;
using Descending.Gui;
using Descending.Party;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Descending.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private ProjectileDefinition _definition = null;
        [SerializeField] private GameEntityTypes _ownerType = GameEntityTypes.None;
        [SerializeField] private GameEntityTypes _targetType = GameEntityTypes.None;
        [SerializeField] private Collider _collider = null;

        private GameEntity _user = null;
        private bool _targetHit = true;
        private bool _hasCollided = false;
        private Transform _target = null;
        private Ability _ability = null;
        
        private void Awake()
        {
            Destroy(gameObject, 2f);
        }

        public void Setup(GameEntity user, Transform target, GameEntityTypes ownerType, GameEntityTypes targetType, Ability ability, bool targetHit)
        {
            _user = user;
            _target = target;
            _ownerType = ownerType;
            _targetType = targetType;
            _ability = ability;
            _targetHit = targetHit;

            if (_targetHit == false)
                _collider.enabled = false;
            
            transform.LookAt(_target, Vector3.up);
        }
        
        private void OnCollisionEnter(Collision other)
        {
            if (_hasCollided == true) return;

            if (_targetType == GameEntityTypes.Enemy && other.gameObject.CompareTag("Enemy"))
            {
                Enemy enemy = other.gameObject.GetComponent<Enemy>();
                if (enemy != null && enemy.IsAlive())
                {
                    EnemyHit(enemy);
                }
            }
            else if (_targetType == GameEntityTypes.Hero && other.gameObject.CompareTag("Projectile Detector"))
            {
                PartyHit(other);
            }
        }

        private void EnemyHit(Enemy enemy)
        {
            _hasCollided = true;

            if (_ability == null)
            {
                int damage = Random.Range(_definition.MinDamage, _definition.MaxDamage + 1);
                MessageHandler.Instance.DisplayMessage(new GameMessage(enemy.GetName() + " hit by " + _definition.name + " for " + damage + " damage"));
                enemy.Damage("Life", damage, _definition.DamageType);
            }
            else
            {
                _ability.Use(_user, new List<GameEntity> { enemy });
            }

            if (gameObject != null)
            {
                Destroy(gameObject);
            }
        }

        private void PartyHit(Collision other)
        {
            _hasCollided = true;
            PartyData partyData = other.gameObject.GetComponentInParent<PartyManagerHolder>().PartyManager.PartyData;
            if (partyData == null) return;

            if (_ability == null)
            {
                int attackIndex = Random.Range(0, partyData.Heroes.Count);
                Hero hero = partyData.Heroes[attackIndex];
                int damage = Random.Range(_definition.MinDamage, _definition.MaxDamage + 1);
                MessageHandler.Instance.DisplayMessage(new GameMessage(hero.GetName() + " hit by " + _definition.name + " for " + damage + " damage"));
                hero.Damage("Life", damage, _definition.DamageType);
            }
            else
            {
                
            }

            if (gameObject != null)
            {
                Destroy(gameObject);
            }
        }
    }
}