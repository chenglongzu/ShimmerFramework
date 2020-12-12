#if ToLua
using LuaInterface;
using UnityEngine;

public class ToLua_CallTable : MonoBehaviour
{
    void Start()
    {
        //主要学习目的 学会toLua中如何在C#调用lua中的自定义table
        LuaManager.GetInstance().Init();
        LuaManager.GetInstance().DoLuaFile("Main");

        //通过 luastate中的 getTable方法 来获取
        LuaTable table = LuaManager.GetInstance().LuaState.GetTable("testClass");
        //table = LuaMgr.GetInstance().LuaState["testClass"] as LuaTable;
        //访问其中的变量
        //中括号 变量名 就可以获取
        Debug.Log(table["testInt"]);
        Debug.Log(table["testBool"]);
        Debug.Log(table["testFloat"]);
        Debug.Log(table["testString"]);
        table["testInt"] = 10;
        //它是引用拷贝
        LuaTable table2 = LuaManager.GetInstance().LuaState.GetTable("testClass");
        Debug.Log(table2["testInt"]);
        //获取其中的函数 
        LuaFunction function = table.GetLuaFunction("testFun");
        function.Call();
    }

}
#endif