using System;
using System.Collections;
using System.Collections.Generic;
using Descending.Characters;
using Descending.Combat;
using Descending.Core;
using Descending.Enemies;
using Descending.Equipment;
using Descending.Gui;
using Descending.Party;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Descending.Player
{
    public class AttackController : MonoBehaviour
    {
        [SerializeField] private PartyManager _partyManager = null;
        [SerializeField] private Transform _projectileSpawnPoint = null;
        [SerializeField] private Transform _attackEffectSpawnPoint = null;

        private Camera _camera = null;
        private bool _canAttack = true;
        private Vector3 _projectileDestination;
        
        public bool CanAttack => _canAttack;
        public Transform ProjectileSpawnPoint => _projectileSpawnPoint;

        private void Awake()
        {
            _camera = Camera.main;
        }

        public void ProcessLeftClick(Vector3 hitPoint, Enemy target)
        {
            // if (_canAttack == false) return;
            // float distance = 0f;
            //
            // if (target.IsAlive() == false)
            // {
            //     distance = Vector3.Distance(transform.position, target.transform.position);
            //
            //     if (distance <= 5f)
            //     {
            //         target.Loot();
            //         return;
            //     }
            // }
            //
            // Hero hero = _partyManager.GetCurrentHero();
            // Item meleeWeapon = hero.Inventory.GetMeleeWeapon();
            // Item rangedWeapon = hero.Inventory.GetRangedWeapon();
            //
            // if (hero == null) return;
            //
            // distance = Vector3.Distance(transform.position, target.transform.position);
            //
            // if(meleeWeapon != null && meleeWeapon.Key != "" && distance <= meleeWeapon.GetWeaponData().Range)
            // {
            //     MeleeAttack(meleeWeapon, hero, target);
            //     StartCoroutine(TriggerDelay(meleeWeapon.GetWeaponData().Delay));
            // }
            // else if (rangedWeapon != null && rangedWeapon.Key != "" && distance <= rangedWeapon.GetWeaponData().Range)
            // {
            //     ShootProjectile(rangedWeapon, hero, target, hitPoint);
            //     StartCoroutine(TriggerDelay(rangedWeapon.GetWeaponData().Delay));
            // }
            // else
            // {
            //     StartCoroutine(TriggerDelay(1f));
            // }
        }

        private IEnumerator TriggerDelay(float delay)
        {
            _canAttack = false;
            yield return new WaitForSeconds(delay);
            _partyManager.SelectNextHero();
            _canAttack = true;
        }

        private void MeleeAttack(Item weapon, Hero hero, Enemy target)
        {
            Utilities.PlayParticleSystem(weapon.GetWeaponData().AttackEffectPrefab, _attackEffectSpawnPoint.position);
            int attackRoll = hero.Attributes.GetStatistic("Attack").Current + Random.Range(0, 100);
            int defenseRoll = target.Attributes.GetStatistic("Dodge").Current + Random.Range(0, 100);

            if (attackRoll > defenseRoll)
            {
                int damage = Random.Range(weapon.GetWeaponData().MinDamage, weapon.GetWeaponData().MaxDamage + 1);
                target.Damage("Life", damage, null);
                MessageHandler.Instance.DisplayMessage(new GameMessage(hero.GetName() + " attacks " + target.name + " with " + weapon.Name + " for " + damage + " damage"));
            }
            else
            {
                MessageHandler.Instance.DisplayMessage(new GameMessage(hero.GetName() + " misses " + target.name));
            }
        }
        
        private void ShootProjectile(Item weapon, Hero hero, Enemy target, Vector3 hitPoint)
        {
            ProjectileDefinition projectileDefinition = weapon.GetWeaponData().Projectile;
            
            GameObject clone = Instantiate(projectileDefinition.Prefab, _projectileSpawnPoint.position, _projectileSpawnPoint.transform.rotation);
            Projectile projectile = clone.GetComponent<Projectile>();
            
            int aimRoll = hero.Attributes.GetStatistic("Aim").Current + Random.Range(0, 100);
            int defenseRoll = target.Attributes.GetStatistic("Dodge").Current + Random.Range(0, 100);
            bool targetHit = aimRoll > defenseRoll;
            projectile.Setup(hero, target.transform, GameEntityTypes.Hero, GameEntityTypes.Enemy, null, targetHit);
            clone.GetComponent<Rigidbody>().velocity = (hitPoint - _projectileSpawnPoint.position).normalized * projectileDefinition.Speed;
            MessageHandler.Instance.DisplayMessage(new GameMessage(hero.GetName() + " fires " + projectileDefinition.name));
        }
    }
}