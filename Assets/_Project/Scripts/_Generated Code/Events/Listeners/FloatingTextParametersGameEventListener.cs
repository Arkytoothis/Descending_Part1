using Descending.Gui;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[AddComponentMenu(SOArchitecture_Utility.EVENT_LISTENER_SUBMENU + "FloatingTextParameters")]
	public sealed class FloatingTextParametersGameEventListener : BaseGameEventListener<FloatingTextParameters, FloatingTextParametersGameEvent, FloatingTextParametersUnityEvent>
	{
	}
}