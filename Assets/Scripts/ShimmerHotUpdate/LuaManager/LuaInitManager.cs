using UnityEngine;

namespace ShimmerHotUpdate
{
    public class LuaInitManager : MonoBehaviour
    {
        private void Awake()
        {

#if XLua || ToLua
            //初始化Lua管理器
            LuaManager.GetInstance().Init();
#endif

        }
        void Start()
        {
            //是否为Lua热更新
#if XLua || ToLua
            LuaManager.GetInstance().DoLuaFile("main");
#endif

        }
    }
}