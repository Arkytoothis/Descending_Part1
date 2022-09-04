using Descending.Abilities;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[AddComponentMenu(SOArchitecture_Utility.EVENT_LISTENER_SUBMENU + "UseAbilityData")]
	public sealed class UseAbilityDataGameEventListener : BaseGameEventListener<UseAbilityData, UseAbilityDataGameEvent, UseAbilityDataUnityEvent>
	{
	}
}