using Descending.Interactables;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public sealed class TreasureDataReference : BaseReference<TreasureData, TreasureDataVariable>
	{
	    public TreasureDataReference() : base() { }
	    public TreasureDataReference(TreasureData value) : base(value) { }
	}
}