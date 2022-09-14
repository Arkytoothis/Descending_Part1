using System.Collections;
using System.Collections.Generic;
using Descending.Traps;
using UnityEngine;

namespace Descending.Interactables
{
    public class Lever : Interactable
    {
        [SerializeField] private Animator _animator = null;
        [SerializeField] private float _onTimeMult = 1f;
        [SerializeField] private float _offTimeMult = 1f;

        [SerializeField] private List<Trap> _traps = null;
        
        private bool _isOn = false;
        
        public override void Interact()
        {
            if (_isOn == true)
            {
                 TurnOff();
            }
            else
            {
                TurnOn();
            }
        }
        
        private void TurnOn()
        {
            _isOn = true;
            _animator.SetFloat("Time", _onTimeMult);
            _animator.SetTrigger("On");

            if (_traps == null) return;

            for (int i = 0; i < _traps.Count; i++)
            {
                _traps[i].SetActive(false);
            }
        }

        private void TurnOff()
        {
            _isOn = false;
            _animator.SetFloat("Time", _offTimeMult);
            _animator.SetTrigger("Off");

            if (_traps == null) return;

            for (int i = 0; i < _traps.Count; i++)
            {
                _traps[i].SetActive(true);
            }
        }
    }
}