using Descending.Abilities;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public sealed class UseAbilityDataUnityEvent : UnityEvent<UseAbilityData>
	{
	}
}