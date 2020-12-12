using UnityEngine;

namespace ShimmerNote
{
    public class RunState : StateBase
    {
        public override void Enter()
        {
            Debug.Log("Run I am Enter");
        }

        public override void UpDate()
        {
            Debug.Log("Run I anm Update");
        }

        public override void Exit()
        {
            Debug.Log("Run I am Exit");
        }

    }
}
