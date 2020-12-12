#if ToLua
using LuaInterface;
using UnityEngine;

public class ToLua_CallCoroutine : MonoBehaviour
{
    void Start()
    {
        LuaManager.GetInstance().Init();
        LuaManager.GetInstance().DoLuaFile("Main");

        LuaFunction function = LuaManager.GetInstance().LuaState.GetFunction("StartDelay");
        function.Call();
        function.Dispose();
    }
}
#endif