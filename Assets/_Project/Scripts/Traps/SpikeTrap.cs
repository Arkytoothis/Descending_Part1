using System;
using System.Collections;
using System.Collections.Generic;
using DarkTonic.MasterAudio;
using Descending.Core;
using Descending.Party;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Descending.Traps
{
    public class SpikeTrap : Trap
    {
        [SerializeField] private Animator _animator = null;
        [SerializeField] private float _openDelay = 1f;
        [SerializeField] private float _closeDelay = 1f;
        [SerializeField] private float _openTimeMult = 1f;
        [SerializeField] private float _closeTimeMult = 1f;
        [SerializeField] private DamageTypeDefinition _damageType = null;
        [SerializeField] private int _minDamage = 0;
        [SerializeField] private int _maxDamage = 0;
        [SerializeField, SoundGroupAttribute] private string[] _openSounds = null;
        [SerializeField, SoundGroupAttribute] private string[] _closeSounds = null;

        private bool _isOpen = false;
        
        private void Start()
        {
            _isOpen = false;
            Trigger();
        }

        public void Trigger()
        {
            if (_isActive == false) return;
            
            StartCoroutine(Open_Coroutine());
        }
        
        private IEnumerator Open_Coroutine()
        {
            Open();
            yield return new WaitForSeconds(_openDelay);
            StartCoroutine(Close_Coroutine());
        }

        private void Open()
        {
            _isOpen = true;
            _animator.SetFloat("Time", _openTimeMult);
            _animator.SetTrigger("Open");
            
            for (int i = 0; i < 3; i++)
            {
                PlayOpenSound();
            }
        }

        private void PlayOpenSound()
        {
            string sound = _openSounds[Random.Range(0, _openSounds.Length)];
            MasterAudio.PlaySound3DAtVector3(sound, transform.position, 0.5f, 0.8f);
        }
        
        private IEnumerator Close_Coroutine()
        {
            Close();
            yield return new WaitForSeconds(_closeDelay);
            StartCoroutine(Open_Coroutine());
        }

        private void Close()
        {
            _isOpen = false;
            _animator.SetFloat("Time", _closeTimeMult);
            _animator.SetTrigger("Close");
            
            for (int i = 0; i < 3; i++)
            {
                PlayCloseSound();
            }
        }

        private void PlayCloseSound()
        {
            string sound = _closeSounds[Random.Range(0, _closeSounds.Length)];
            MasterAudio.PlaySound3DAtVector3(sound, transform.position, 0.5f, 0.8f);
        }
        
        public override void ApplyDamage(PartyData partyData)
        {
            for (int i = 0; i < partyData.Heroes.Count; i++)
            {
                int damage = Random.Range(_minDamage, _maxDamage + 1);
                if (Random.Range(0, 100) > 50)
                {
                    partyData.Heroes[i].Damage("Life", damage, _damageType);
                }
                else
                {
                    partyData.Heroes[i].DodgedAttack();
                }
            }
        }

        public override void SetActive(bool isActive)
        {
            _isActive = isActive;

            if (_isActive == false)
            {
                Close();
                StopAllCoroutines();
            }
            else
            {
                Trigger();
            }
        }
    }
}