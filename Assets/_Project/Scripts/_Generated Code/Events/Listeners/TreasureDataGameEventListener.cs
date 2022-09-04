using Descending.Interactables;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[AddComponentMenu(SOArchitecture_Utility.EVENT_LISTENER_SUBMENU + "TreasureData")]
	public sealed class TreasureDataGameEventListener : BaseGameEventListener<TreasureData, TreasureDataGameEvent, TreasureDataUnityEvent>
	{
	}
}