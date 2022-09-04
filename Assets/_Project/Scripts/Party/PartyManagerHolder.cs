using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descending.Party
{
    public class PartyManagerHolder : MonoBehaviour
    {
        [SerializeField] private PartyManager _partyManager = null;

        public PartyManager PartyManager => _partyManager;
    }
}