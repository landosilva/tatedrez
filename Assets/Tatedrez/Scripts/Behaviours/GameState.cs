using Lando.Plugins.Debugger;
using UnityEngine;
using UnityEngine.Animations;

namespace Tatedrez.Behaviours
{
    public abstract class GameState : StateMachineBehaviour
    {
        protected Animator _stateMachine;
        protected Blackboard _blackboard;
        public sealed override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex,
            AnimatorControllerPlayable controller)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex, controller);
            
            _stateMachine = animator;
            _blackboard = animator.GetComponent<Blackboard>();
            
            OnEnter();
            
            Debugger.Log("Entered state: " + GetType().Name);
        }
        
        public sealed override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex,
            AnimatorControllerPlayable controller)
        {
            base.OnStateExit(animator, stateInfo, layerIndex, controller);
            
            OnExit();
            
            Debugger.Log("Exited state: " + GetType().Name);
        }

        protected virtual void OnEnter() { }
        protected virtual void OnExit() { }
    }
}