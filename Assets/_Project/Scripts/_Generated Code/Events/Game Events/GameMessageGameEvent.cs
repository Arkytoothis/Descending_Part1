using Descending.Gui;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	[CreateAssetMenu(
	    fileName = "GameMessageGameEvent.asset",
	    menuName = SOArchitecture_Utility.GAME_EVENT + "GameMessage Event",
	    order = 120)]
	public sealed class GameMessageGameEvent : GameEventBase<GameMessage>
	{
	}
}