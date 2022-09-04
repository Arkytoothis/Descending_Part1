using Descending.Abilities;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public sealed class UseAbilityDataReference : BaseReference<UseAbilityData, UseAbilityDataVariable>
	{
	    public UseAbilityDataReference() : base() { }
	    public UseAbilityDataReference(UseAbilityData value) : base(value) { }
	}
}