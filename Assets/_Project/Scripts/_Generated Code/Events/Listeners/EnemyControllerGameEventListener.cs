using Descending.Enemies;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[AddComponentMenu(SOArchitecture_Utility.EVENT_LISTENER_SUBMENU + "EnemyController")]
	public sealed class EnemyControllerGameEventListener : BaseGameEventListener<Enemy, EnemyControllerGameEvent, EnemyControllerUnityEvent>
	{
	}
}