using Descending.Gui;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public class FloatingTextParametersEvent : UnityEvent<FloatingTextParameters> { }

	[CreateAssetMenu(
	    fileName = "FloatingTextParametersVariable.asset",
	    menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "FloatingTextParameters Event",
	    order = 120)]
	public class FloatingTextParametersVariable : BaseVariable<FloatingTextParameters, FloatingTextParametersEvent>
	{
	}
}