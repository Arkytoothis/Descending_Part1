using System;
using System.Collections;
using System.Collections.Generic;
using Descending.Party;
using UnityEngine;

namespace Descending.Traps
{
    public class SpikeTrap : Trap
    {
        [SerializeField] private Animator _animator = null;
        [SerializeField] private float _openDelay = 1f;
        [SerializeField] private float _closeDelay = 1f;
        [SerializeField] private float _openTimeMult = 1f;
        [SerializeField] private float _closeTimeMult = 1f;

        private bool _isOpen = false;
        
        private void Start()
        {
            _isOpen = false;
            Trigger();
        }

        public void Trigger()
        {
            StartCoroutine(Open_Coroutine());
        }
        
        private IEnumerator Open_Coroutine()
        {
            _isOpen = true;
            _animator.SetFloat("Time", _openTimeMult);
            _animator.SetTrigger("Open");
            yield return new WaitForSeconds(_openDelay);
            StartCoroutine(Close_Coroutine());
        }

        private IEnumerator Close_Coroutine()
        {
            _isOpen = false;
            _animator.SetFloat("Time", _closeTimeMult);
            _animator.SetTrigger("Close");
            yield return new WaitForSeconds(_closeDelay);
            StartCoroutine(Open_Coroutine());
        }
        
        public override void ApplyDamage(PartyData partyData)
        {
            Debug.Log("Applying damage to Party");
        }
    }
}