using System.Collections;
using System.Collections.Generic;
using DarkTonic.MasterAudio;
using DG.Tweening;
using UnityEngine;
using MoreMountains.Feedbacks;

namespace Descending.Interactables
{
    public class DoorSingle : Interactable
    {
        [SerializeField] private MMF_Player _player = null;
        [SerializeField, SoundGroupAttribute] private string[] _openSounds = null;
        [SerializeField, SoundGroupAttribute] private string[] _closeSounds = null;
        
        private bool _isOpen = false;
        
        public override void Interact()
        {
            if (_player.IsPlaying == true) return;
            
            if (_isOpen == false)
            {
                Open();
            }
            else
            {
                Close();
            }
        }

        private void Open()
        {
            //Debug.Log("Opening");
            string sound = _openSounds[Random.Range(0, _openSounds.Length)];
            MasterAudio.PlaySound3DAtVector3(sound, transform.position, 1f, 1f);
            _player.FeedbacksList[0].Play(transform.position);
            _isOpen = true;
        }

        private void Close()
        {
            //Debug.Log("Closing");
            string sound = _closeSounds[Random.Range(0, _closeSounds.Length)];
            MasterAudio.PlaySound3DAtVector3(sound, transform.position, 1f, 1f);
            _player.FeedbacksList[1].Play(transform.position);
            _isOpen = false;
        }
    }
}