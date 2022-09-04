using Descending.Gui;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public sealed class HeroWidgetReference : BaseReference<HeroWidget, HeroWidgetVariable>
	{
	    public HeroWidgetReference() : base() { }
	    public HeroWidgetReference(HeroWidget value) : base(value) { }
	}
}