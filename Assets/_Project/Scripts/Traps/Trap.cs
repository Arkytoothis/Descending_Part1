using System.Collections;
using System.Collections.Generic;
using Descending.Party;
using UnityEngine;

namespace Descending.Traps
{
    public abstract class Trap : MonoBehaviour
    {
        protected bool _isActive = true;

        public bool IsActive => _isActive;

        public abstract void ApplyDamage(PartyData partyData);
        public abstract void SetActive(bool isActive);
    }
}