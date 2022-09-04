using Descending.Gui;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public class HeroWidgetEvent : UnityEvent<HeroWidget> { }

	[CreateAssetMenu(
	    fileName = "HeroWidgetVariable.asset",
	    menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "HeroWidget Event",
	    order = 120)]
	public class HeroWidgetVariable : BaseVariable<HeroWidget, HeroWidgetEvent>
	{
	}
}