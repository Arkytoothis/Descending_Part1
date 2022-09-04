using Descending.Gui;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	[CreateAssetMenu(
	    fileName = "HeroWidgetGameEvent.asset",
	    menuName = SOArchitecture_Utility.GAME_EVENT + "HeroWidget Event",
	    order = 120)]
	public sealed class HeroWidgetGameEvent : GameEventBase<HeroWidget>
	{
	}
}