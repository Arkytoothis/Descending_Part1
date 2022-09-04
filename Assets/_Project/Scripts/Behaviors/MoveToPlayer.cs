using Descending.Enemies;
using Descending.Equipment;
using NodeCanvas.BehaviourTrees;
using UnityEngine;
using NodeCanvas.Framework;
using Component = UnityEngine.Component;

namespace Descending.Behaviors
{
    [ParadoxNotion.Design.Category("Descending")]
    [ParadoxNotion.Design.Icon("SomeIcon")]
    [ParadoxNotion.Design.Description("Some description")]
    public class MoveToPlayer : BTNode
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
                if (_enemy.GetDestination() == null)
                {
                    _enemy.SetDestination(target.transform);
                }
                
                Item weapon = _enemy.GetCurrentWeapon();
                float distance = Vector3.Distance(graphAgent.transform.position, target.transform.position);
                float range = 3f;
                if (weapon != null) range = weapon.GetWeaponData().Range;
                
                if (distance <= range)
                {
                    _enemy.SetDestination(null);
                    return Status.Success;
                }
                else
                {
                    return Status.Failure;
                }
            }
            else
            {
                return Status.Failure;
            }
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