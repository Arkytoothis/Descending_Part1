using Descending.Interactables;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	[CreateAssetMenu(
	    fileName = "TreasureDataGameEvent.asset",
	    menuName = SOArchitecture_Utility.GAME_EVENT + "TreasureData Event",
	    order = 120)]
	public sealed class TreasureDataGameEvent : GameEventBase<TreasureData>
	{
	}
}