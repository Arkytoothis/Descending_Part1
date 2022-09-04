using Descending.Gui;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public sealed class HeroWidgetUnityEvent : UnityEvent<HeroWidget>
	{
	}
}