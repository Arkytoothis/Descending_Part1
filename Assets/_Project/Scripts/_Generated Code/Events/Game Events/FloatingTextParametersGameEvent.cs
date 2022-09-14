using Descending.Gui;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	[CreateAssetMenu(
	    fileName = "FloatingTextParametersGameEvent.asset",
	    menuName = SOArchitecture_Utility.GAME_EVENT + "FloatingTextParameters Event",
	    order = 120)]
	public sealed class FloatingTextParametersGameEvent : GameEventBase<FloatingTextParameters>
	{
	}
}