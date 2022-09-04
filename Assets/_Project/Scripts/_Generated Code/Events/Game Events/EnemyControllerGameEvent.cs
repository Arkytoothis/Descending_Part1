using Descending.Enemies;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	[CreateAssetMenu(
	    fileName = "EnemyControllerGameEvent.asset",
	    menuName = SOArchitecture_Utility.GAME_EVENT + "EnemyController Event",
	    order = 120)]
	public sealed class EnemyControllerGameEvent : GameEventBase<Enemy>
	{
	}
}