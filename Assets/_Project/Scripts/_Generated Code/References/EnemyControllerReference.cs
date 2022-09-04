using Descending.Enemies;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public sealed class EnemyControllerReference : BaseReference<Enemy, EnemyControllerVariable>
	{
	    public EnemyControllerReference() : base() { }
	    public EnemyControllerReference(Enemy value) : base(value) { }
	}
}