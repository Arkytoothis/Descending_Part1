using System;
using System.Collections;
using System.Collections.Generic;
using Descending.Characters;
using Descending.Combat;
using Descending.Core;
using Descending.Equipment;
using Descending.Gui;
using Descending.Party;
using Descending.Player;
using DG.Tweening;
using NodeCanvas.BehaviourTrees;
using Pathfinding;
using Pathfinding.RVO;
using ScriptableObjectArchitecture;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Descending.Enemies
{
    public class Enemy : GameEntity
    {
        [SerializeField] private EnemyDefinition _definition = null;
        [SerializeField] private TargetController _targetController = null;
        [SerializeField] private RichAI _richAI = null;
        [SerializeField] private AIDestinationSetter _destinationSetter = null;
        [SerializeField] private RVOController _rvoController = null;
        [SerializeField] private Collider _collider = null;
        [SerializeField] private Rigidbody _rigidbody = null;
        [SerializeField] private Animator _animator = null;
        [SerializeField] private Transform _modelTransform = null;
        [SerializeField] private AttributesController _attributes = null;
        [SerializeField] private EnemyInventory _inventory = null;
        [SerializeField] private VitalBar _lifeBar = null;
        [SerializeField] private BehaviourTreeOwner _btOwner = null;
        
        [SerializeField] private EnemyControllerEvent onRegisterEnemy = null;
        [SerializeField] private IntEvent onAwardExperience = null;
        [SerializeField] private IntEvent onAwardCoins = null;
        [SerializeField] private IntEvent onAwardGems = null;

        private bool _isActive = false;
        
        public bool IsActive => _isActive;

        public EnemyDefinition Definition => _definition;
        public AttributesController Attributes => _attributes;
        public EnemyInventory Inventory => _inventory;

        public void Setup()
        {
            onRegisterEnemy.Invoke(this);
            _isAlive = true;
            _attributes.Setup(_definition);
            _inventory.Setup(_definition);
            SetActive(false);
            SyncData();
            _lifeBar.Hide();
            _btOwner.StopBehaviour();

            _richAI.maxSpeed = _attributes.GetStatistic("Speed").Current;
        }

        private void Update()
        {
            if (_isActive == false) return;

            float velocity = _rigidbody.velocity.magnitude;
            _animator.SetFloat("Blend", velocity);
        }

        public void SetTarget(GameObject target)
        {
            _targetController.SetTarget(target);
        }

        public void SetDestination(Transform target)
        {
            if (target != null)
            {
                _destinationSetter.target = target;
                _richAI.canMove = true;
                _richAI.canSearch = true;
            }
            else
            {
                _destinationSetter.target = null;
                _richAI.canMove = false;
                _richAI.canSearch = false;
            }
        }

        public Transform GetDestination()
        {
            return _destinationSetter.target;
        }

        public override string GetName()
        {
            return _definition.Name;
        }

        public override void Damage(string attribute, int amount, DamageTypeDefinition damageType)
        {
            if (_isAlive == false) return;
            
            _attributes.Vitals[attribute].Damage(amount);
            SyncData();

            //Debug.Log("Damage " + amount + " " + attribute + " " + _attributes.GetVital(attribute).Current);
            if (_attributes.GetVital(attribute).Current <= 0)
            {
                Death();
            }
            else
            {
                _animator.SetTrigger("Hit");
            }
        }

        public override void UseActions(int amount)
        {
            
        }

        public override void Restore(string attribute, int amount)
        {
            
        }

        public override void Death()
        {
            //Debug.Log("Die");
            _isAlive = false;
            _animator.SetTrigger("Die");
            _richAI.enabled = false;
            _destinationSetter.enabled = false;
            _rvoController.enabled = false;
            _collider.isTrigger = true;
            
            _lifeBar.Hide();
            _btOwner.StopBehaviour();
            
            onAwardExperience.Invoke(_definition.ExpValue);
        }

        public override bool IsAlive()
        {
            return _isAlive;
        }

        public override void UseItem(Item item)
        {
        }

        public override void UseAccessory(int index)
        {
        }

        public override int RollDamage()
        {
            return 0;
        }

        public override void MeleeAttack()
        {
            _animator.SetTrigger("Melee Attack");
        }

        public override void RangedAttack(Transform target)
        {
            _animator.SetTrigger("Ranged Attack");

            Item rangedWeapon = _inventory.GetRangedWeapon();

            if (rangedWeapon.GetWeaponData().Projectile != null)
            {
                ProjectileDefinition projectileDefinition = rangedWeapon.GetWeaponData().Projectile;
                
                GameObject clone = Instantiate(projectileDefinition.Prefab, _projectileSpawnPoint.position, _projectileSpawnPoint.transform.rotation);
                Projectile projectile = clone.GetComponent<Projectile>();
                int aimRoll = _attributes.GetStatistic("Aim").Current + Random.Range(0, 100);
                int defenseRoll = _attributes.GetStatistic("Dodge").Current + Random.Range(0, 100);
                bool targetHit = aimRoll > defenseRoll;
                projectile.Setup(this, target, GameEntityTypes.Enemy, GameEntityTypes.Hero, null, targetHit);
                clone.GetComponent<Rigidbody>().velocity = (target.position - _projectileSpawnPoint.position).normalized * projectileDefinition.Speed;
            }
        }
        
        public override void SetListIndex(int index)
        {
        }

        public override void SetInitiativeIndex(int index)
        {
        }

        public void SetActive(bool active)
        {
            _isActive = active;
            _modelTransform.gameObject.SetActive(_isActive);
            //_richAI.enabled = _isActive;
            _animator.enabled = _isActive;
            //_destinationSetter.enabled = _isActive;

            if (_isActive == true)
            {
                _lifeBar.Show();
                //_btOwner.StartBehaviour();
            }
            else
            {
                _lifeBar.Hide();
                //_btOwner.StopBehaviour();
            }
        }
        

        public void AttackPlayer(GameObject playerObject)
        {
            int rnd = Random.Range(0, 100);
            int attackIndex = -1;
            
            if (rnd < 50)
            {
                attackIndex = Random.Range(0, 100) < 50 ? 0 : 1;
            }
            else if (rnd < 80)
            {
                attackIndex = Random.Range(0, 100) < 50 ? 2 : 3;
            }
            else
            {
                attackIndex = Random.Range(0, 100) < 50 ? 4 : 5;
            }

            transform.DOLookAt(playerObject.transform.position, 0.1f);
            PartyData partyData = playerObject.GetComponent<PartyManagerHolder>().PartyManager.PartyData;
            Hero hero = partyData.Heroes[attackIndex];
            Item meleeWeapon = _inventory.GetMeleeWeapon();
            Item rangedWeapon = _inventory.GetRangedWeapon();

            if (meleeWeapon != null)
            {
                MeleeAttack();
                int damage = Random.Range(1, 10);
                hero.Damage("Life", damage, null);
            }
            else if (rangedWeapon != null)
            {
                ProjectileTarget projectileTarget = playerObject.GetComponentInChildren<ProjectileTarget>();
                RangedAttack(projectileTarget.transform);
            }
        }

        public override void SyncData()
        {
            _lifeBar.SetValues(_attributes.GetVital("Life").Current, _attributes.GetVital("Life").Maximum, false);
        }

        public void Loot()
        {
            onAwardCoins.Invoke(_definition.LootCoins());
            onAwardGems.Invoke(_definition.LootGems());
            Destroy(gameObject);
        }

        public Item GetCurrentWeapon()
        {
            if (_inventory.GetMeleeWeapon() != null) return _inventory.GetMeleeWeapon();
            else if (_inventory.GetRangedWeapon() != null) return _inventory.GetRangedWeapon();
            else return null;
        }
    }
}