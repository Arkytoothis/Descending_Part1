using Descending.Gui;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public sealed class GameMessageReference : BaseReference<GameMessage, GameMessageVariable>
	{
	    public GameMessageReference() : base() { }
	    public GameMessageReference(GameMessage value) : base(value) { }
	}
}