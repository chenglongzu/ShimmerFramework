#if XLua
using UnityEngine;
using XLua;
using ShimmerHotUpdate;

public class XLua_CallLuaTable : MonoBehaviour
{
    void Start()
    {
        XLuaManager.GetInstance().Init();
        XLuaManager.GetInstance().DoLuaFile("Main");

        //不建议使用LuaTable和LuaFunction 效率低
        //引用对象
        LuaTable table = XLuaManager.GetInstance().Global.Get<LuaTable>("testClas");
        Debug.Log(table.Get<int>("testInt"));
        Debug.Log(table.Get<bool>("testBool"));
        Debug.Log(table.Get<float>("testFloat"));
        Debug.Log(table.Get<string>("testString"));

        table.Get<LuaFunction>("testFun").Call();
        //改  引用
        table.Set("testInt", 55);
        Debug.Log(table.Get<int>("testInt"));
        LuaTable table2 = XLuaManager.GetInstance().Global.Get<LuaTable>("testClas");
        Debug.Log(table2.Get<int>("testInt"));

        table.Dispose();
        table2.Dispose();
    }

}
#endif