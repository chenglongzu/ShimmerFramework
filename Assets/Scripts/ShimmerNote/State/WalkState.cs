using UnityEngine;

namespace ShimmerNote
{
    public class WalkState : StateBase
    {
        public override void Enter()
        {
            Debug.Log("Walk I am Enter");
        }

        public override void UpDate()
        {
            Debug.Log("Walk I anm Update");
        }

        public override void Exit()
        {
            Debug.Log("Walk I am Exit");
        }
    }
}