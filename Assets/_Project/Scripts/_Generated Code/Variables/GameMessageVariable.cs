using Descending.Gui;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public class GameMessageEvent : UnityEvent<GameMessage> { }

	[CreateAssetMenu(
	    fileName = "GameMessageVariable.asset",
	    menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "GameMessage Event",
	    order = 120)]
	public class GameMessageVariable : BaseVariable<GameMessage, GameMessageEvent>
	{
	}
}