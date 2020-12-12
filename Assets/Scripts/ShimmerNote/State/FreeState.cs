using UnityEngine;

namespace ShimmerNote
{
    public class FreeState : StateBase
    {
        public override void Enter()
        {
            Debug.Log("空闲状态");
        }

        public override void UpDate()
        {
            Debug.Log("空闲状态");
        }

        public override void Exit()
        {
            Debug.Log("空闲状态");
        }
    }
}
