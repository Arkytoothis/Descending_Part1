using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using MoreMountains.Feedbacks;

namespace Descending.Interactables
{
    public class DoorSingle : Interactable
    {
        [SerializeField] private MMF_Player _player = null;
        
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
            _player.FeedbacksList[0].Play(transform.position);
            _isOpen = true;
        }

        private void Close()
        {
            //Debug.Log("Closing");
            _player.FeedbacksList[1].Play(transform.position);
            _isOpen = false;
        }
    }
}