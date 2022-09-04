using System.Collections;
using System.Collections.Generic;
using DarkTonic.MasterAudio;
using Descending.Combat;
using Descending.Enemies;
using UnityEngine;

namespace Descending.Characters
{
    public class AnimationEvents : MonoBehaviour
    {
        private Hero _hero = null;
        private Enemy _enemyTarget = null;
        
        public void Setup(Hero hero)
        {
            _hero = hero;
            _enemyTarget = null;
        }

        public void SetTarget(Enemy enemyTarget)
        {
            _enemyTarget = enemyTarget;
        }
        
        public void Shoot()
        {
        }

        public void FootR()
        {
            //string sound = _hero.Inventory.GetRandomWalkSound();
            //MasterAudio.PlaySound3DAtTransform(sound, transform, 0.5f, 1f);
        }

        public void FootL()
        {
            //string sound = _hero.Inventory.GetRandomWalkSound();
            //MasterAudio.PlaySound3DAtTransform(sound, transform, 0.5f, 1f);
        }

        public void Land()
        {
        }

        public void WeaponSwitch()
        {
        }

        public void Hit()
        {
            if (_enemyTarget != null)
            {
                //AttackCalculator.ProcessAttack(_hero, _enemyTarget);
            }
        }

        public void Strike()
        {
        }
    }
}