using Descending.Enemies;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public class EnemyControllerEvent : UnityEvent<Enemy> { }

	[CreateAssetMenu(
	    fileName = "EnemyControllerVariable.asset",
	    menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "EnemyController Event",
	    order = 120)]
	public class EnemyControllerVariable : BaseVariable<Enemy, EnemyControllerEvent>
	{
	}
}