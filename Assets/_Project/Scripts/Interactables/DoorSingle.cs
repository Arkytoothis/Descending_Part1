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
        [SerializeField, SoundGroupAttribute] private string[] _creakSounds = null;
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
            string openSound = _openSounds[Random.Range(0, _openSounds.Length)];
            MasterAudio.PlaySound3DAtVector3(openSound, transform.position, 1f, 1f);

            string creakSound = _creakSounds[Random.Range(0, _creakSounds.Length)];
            MasterAudio.PlaySound3DAtVector3(creakSound, transform.position, 1f, 1f);
            _player.FeedbacksList[0].Play(transform.position);
            _isOpen = true;
        }

        private void Close()
        {
            //Debug.Log("Closing");
            string creakSound = _creakSounds[Random.Range(0, _creakSounds.Length)];
            MasterAudio.PlaySound3DAtVector3(creakSound, transform.position, 1f, 1f);
            _player.FeedbacksList[1].Play(transform.position);
            _isOpen = false;
        }
    }
}