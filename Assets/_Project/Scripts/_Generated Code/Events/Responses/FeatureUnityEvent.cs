using Descending.World;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public sealed class FeatureUnityEvent : UnityEvent<Feature>
	{
	}
}