using UnityEngine;

namespace ShimmerNote
{
    public class StateInitManager : MonoBehaviour
    {
        private void Awake()
        {
            //有限状态机管理器初始化
            StateManager.GetInstance().Init();
        }

    }
}
