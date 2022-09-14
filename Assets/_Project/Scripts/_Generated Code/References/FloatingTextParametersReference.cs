using Descending.Gui;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public sealed class FloatingTextParametersReference : BaseReference<FloatingTextParameters, FloatingTextParametersVariable>
	{
	    public FloatingTextParametersReference() : base() { }
	    public FloatingTextParametersReference(FloatingTextParameters value) : base(value) { }
	}
}