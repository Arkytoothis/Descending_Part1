using Descending.Abilities;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public class UseAbilityDataEvent : UnityEvent<UseAbilityData> { }

	[CreateAssetMenu(
	    fileName = "UseAbilityDataVariable.asset",
	    menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "UseAbilityData Event",
	    order = 120)]
	public class UseAbilityDataVariable : BaseVariable<UseAbilityData, UseAbilityDataEvent>
	{
	}
}