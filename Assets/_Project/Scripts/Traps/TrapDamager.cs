using System;
using System.Collections;
using System.Collections.Generic;
using Descending.Party;
using UnityEngine;

namespace Descending.Traps
{
    public class TrapDamager : MonoBehaviour
    {
        [SerializeField] private Trap _trap = null;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                PartyData partyData = other.gameObject.GetComponent<PartyManagerHolder>().PartyManager.PartyData;

                if (partyData != null)
                {
                    _trap.ApplyDamage(partyData);
                }
            }
        }
    }
}