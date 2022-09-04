using Descending.Gui;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[AddComponentMenu(SOArchitecture_Utility.EVENT_LISTENER_SUBMENU + "GameMessage")]
	public sealed class GameMessageGameEventListener : BaseGameEventListener<GameMessage, GameMessageGameEvent, GameMessageUnityEvent>
	{
	}
}