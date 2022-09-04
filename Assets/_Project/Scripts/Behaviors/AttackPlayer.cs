using Descending.Enemies;
using NodeCanvas.BehaviourTrees;
using UnityEngine;
using NodeCanvas.Framework;
using Component = UnityEngine.Component;

namespace Descending.Behaviors
{
	[ParadoxNotion.Design.Category("Descending")]
    [ParadoxNotion.Design.Icon("SomeIcon")]
    [ParadoxNotion.Design.Description("Some description")]
    public class AttackPlayer : BTNode
    {
	    private Enemy _enemy = null;
	    
	    //When the BT starts
        public override void OnGraphStarted()
        {
	        _enemy = graphAgent.GetComponent<Enemy>();
        }
 
        //When the BT stops
        public override void OnGraphStoped()
        {
 
        }
 
        //When the BT pauses
        public override void OnGraphPaused()
        {
 
        }
 
        //When the node is Ticked
        protected override Status OnExecute(Component agent, IBlackboard blackboard)
        {
	        GameObject target = blackboard.GetVariable<GameObject>("Target").value;

	        if (target != null)
	        {
		        _enemy.AttackPlayer(target);
		        return Status.Failure;
	        }

	        return Status.Success;
        }
 
        //When the node resets (start of graph, interrupted, new tree traversal).
        protected override void OnReset()
        {
 
        }
 
#if UNITY_EDITOR
 
		//This GUI is shown IN the node IF you want
		protected override void OnNodeGUI()
		{
 
		}
 
		//This GUI is shown when the node is selected IF you want
		protected override void OnNodeInspectorGUI()
		{
 
			DrawDefaultInspector(); //this is done when you dont override this method anyway.
		}
 
#endif
    }
}