using Descending.Abilities;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	[CreateAssetMenu(
	    fileName = "UseAbilityDataGameEvent.asset",
	    menuName = SOArchitecture_Utility.GAME_EVENT + "UseAbilityData Event",
	    order = 120)]
	public sealed class UseAbilityDataGameEvent : GameEventBase<UseAbilityData>
	{
	}
}