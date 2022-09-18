using System.Collections;
using System.Collections.Generic;
using DarkTonic.MasterAudio;
using Descending.Traps;
using UnityEngine;

namespace Descending.Interactables
{
    public class Lever : Interactable
    {
        [SerializeField] private Animator _animator = null;
        [SerializeField] private float _onTimeMult = 1f;
        [SerializeField] private float _offTimeMult = 1f;
        [SerializeField, SoundGroupAttribute] private string[] _onSounds = null;
        [SerializeField, SoundGroupAttribute] private string[] _offSounds = null;

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
            string sound = _onSounds[Random.Range(0, _onSounds.Length)];
            MasterAudio.PlaySound3DAtVector3(sound, transform.position, 2f, 1f);

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
            string sound = _offSounds[Random.Range(0, _offSounds.Length)];
            MasterAudio.PlaySound3DAtVector3(sound, transform.position, 2f, 1f);

            if (_traps == null) return;

            for (int i = 0; i < _traps.Count; i++)
            {
                _traps[i].SetActive(true);
            }
        }
    }
}