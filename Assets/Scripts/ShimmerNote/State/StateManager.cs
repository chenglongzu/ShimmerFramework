using System.Collections.Generic;
using ShimmerFramework;

namespace ShimmerNote
{
    public enum StateMachine
    {
        None,
        Walk,
        Run
    }

    /// <summary>
    /// 有限状态机管理器类
    /// </summary>
    public class StateManager : BaseManager<StateManager>
    {
        private StateMachine currtenState;

        public Dictionary<StateMachine, StateBase> state = new Dictionary<StateMachine, StateBase>();

        public void Init()
        {
#if FSM
            state.Add(StateMachine.None, new FreeState());
            state.Add(StateMachine.Walk, new WalkState());
            state.Add(StateMachine.Run, new RunState());

            MonoManager.GetInstance().AddUpdateAction(Update);
#endif
        }


        private void Update()
        {
            state[currtenState].UpDate();
        }

        public void ChangeNextState(StateMachine nextState)
        {
            state[currtenState].Exit();

            state[nextState].Enter();

            currtenState = nextState;
        }

    }
}