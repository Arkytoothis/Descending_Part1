using Descending.Interactables;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public class TreasureDataEvent : UnityEvent<TreasureData> { }

	[CreateAssetMenu(
	    fileName = "TreasureDataVariable.asset",
	    menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "TreasureData Event",
	    order = 120)]
	public class TreasureDataVariable : BaseVariable<TreasureData, TreasureDataEvent>
	{
	}
}