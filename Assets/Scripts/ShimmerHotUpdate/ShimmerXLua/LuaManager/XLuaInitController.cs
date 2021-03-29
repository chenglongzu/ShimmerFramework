using UnityEngine;

namespace ShimmerHotUpdate
{
    public class XLuaInitController : MonoBehaviour
    {
        private void Awake()
        {
            //初始化Lua管理器
            XLuaManager.GetInstance().Init();
        }
        void Start()
        {
            //是否为Lua热更新
            XLuaManager.GetInstance().DoLuaFile("main");
        }
    }
}

