using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descending.Interactables
{
    public class Lever : Interactable
    {
        [SerializeField] private Animator _animator = null;
            
        private bool _isOn = false;
        
        public override void Interact()
        {
            Debug.Log("Interacting");
        }

        private void TurnOn()
        {
            
        }

        private void TurnOff()
        {
            
        }
    }
}