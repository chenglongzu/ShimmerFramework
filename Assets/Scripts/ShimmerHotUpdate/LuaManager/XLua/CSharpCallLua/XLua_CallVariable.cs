#if XLua
using ShimmerHotUpdate;
using UnityEngine;

public class XLua_CallVariable : MonoBehaviour
{
    void Start()
    {
        LuaManager.GetInstance().Init();

        LuaManager.GetInstance().DoLuaFile("Main");

        //int local = LuaMgr.GetInstance().Global.Get<int>("testLocal");
        //Debug.Log("testLocal:" + local);

        //使用lua解析器luaenv中的 Global属性 
        int i = LuaManager.GetInstance().Global.Get<int>("testNumber");
        Debug.Log("testNumber:" + i);
        i = 10;
        //改值
        LuaManager.GetInstance().Global.Set("testNumber", 55);
        //值拷贝 不会影响原来Lua中的值
        int i2 = LuaManager.GetInstance().Global.Get<int>("testNumber");
        Debug.Log("testNumber_i2:" + i2);

        bool b = LuaManager.GetInstance().Global.Get<bool>("testBool");
        Debug.Log("testBool:" + b);

        float f = LuaManager.GetInstance().Global.Get<float>("testFloat");
        Debug.Log("testFloat:" + f);

        double d = LuaManager.GetInstance().Global.Get<double>("testFloat");
        Debug.Log("testFloat_double:" + d);

        string s = LuaManager.GetInstance().Global.Get<string>("testString");
        Debug.Log("testString:" + s);
    }

}
#endif