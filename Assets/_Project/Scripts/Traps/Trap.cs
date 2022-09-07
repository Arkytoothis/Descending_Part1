using System.Collections;
using System.Collections.Generic;
using Descending.Party;
using UnityEngine;

namespace Descending.Traps
{
    public abstract class Trap : MonoBehaviour
    {
        public abstract void ApplyDamage(PartyData partyData);
    }
}