using Descending.Gui;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[AddComponentMenu(SOArchitecture_Utility.EVENT_LISTENER_SUBMENU + "HeroWidget")]
	public sealed class HeroWidgetGameEventListener : BaseGameEventListener<HeroWidget, HeroWidgetGameEvent, HeroWidgetUnityEvent>
	{
	}
}