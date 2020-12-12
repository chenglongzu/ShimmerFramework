#if XLua
using System;
using UnityEngine;
using UnityEngine.Events;
using XLua;
using ShimmerHotUpdate;

//无参无返回值的委托
public delegate void CustomCall();

//有参有返回 的委托
//该特性是在XLua命名空间中的
//加了过后 要在编辑器里 生成 Lua代码
[CSharpCallLua]
public delegate int CustomCall2(int a);

[CSharpCallLua]
public delegate int CustomCall3(int a, out int b, out bool c, out string d, out int e);
[CSharpCallLua]
public delegate int CustomCall4(int a, ref int b, ref bool c, ref string d, ref int e);

[CSharpCallLua]
public delegate void CustomCall5(string a, params int[] args);//变长参数的类型 是根据实际情况来定的

public class XLua_CallFunction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LuaManager.GetInstance().Init();

        LuaManager.GetInstance().DoLuaFile("Main");

        //无参无返回的获取
        //委托
        CustomCall call = LuaManager.GetInstance().Global.Get<CustomCall>("testFun");
        call();
        //Unity自带委托
        UnityAction ua = LuaManager.GetInstance().Global.Get<UnityAction>("testFun");
        ua();
        //C#提供的委托
        Action ac = LuaManager.GetInstance().Global.Get<Action>("testFun");
        ac();
        //Xlua提供的一种 获取函数的方式 少用
        LuaFunction lf = LuaManager.GetInstance().Global.Get<LuaFunction>("testFun");
        lf.Call();

        //有参有返回
        CustomCall2 call2 = LuaManager.GetInstance().Global.Get<CustomCall2>("testFun2");
        Debug.Log("有参有返回：" + call2(10));
        //C#自带的泛型委托 方便我们使用
        Func<int, int> sFun = LuaManager.GetInstance().Global.Get<Func<int, int>>("testFun2");
        Debug.Log("有参有返回：" + sFun(20));
        //Xlua提供的
        LuaFunction lf2 = LuaManager.GetInstance().Global.Get<LuaFunction>("testFun2");
        Debug.Log("有参有返回：" + lf2.Call(30)[0]);

        //多返回值
        //使用 out 和 ref 来接收
        CustomCall3 call3 = LuaManager.GetInstance().Global.Get<CustomCall3>("testFun3");
        int b;
        bool c;
        string d;
        int e;
        Debug.Log("第一个返回值：" + call3(100, out b, out c, out d, out e));
        Debug.Log(b + "_" + c + "_" + d + "_" + e);

        CustomCall4 call4 = LuaManager.GetInstance().Global.Get<CustomCall4>("testFun3");
        int b1 = 0;
        bool c1 = true;
        string d1 = "";
        int e1 = 0;
        Debug.Log("第一个返回值：" + call4(200, ref b1, ref c1, ref d1, ref e1));
        Debug.Log(b1 + "_" + c1 + "_" + d1 + "_" + e1);
        //Xlua
        LuaFunction lf3 = LuaManager.GetInstance().Global.Get<LuaFunction>("testFun3");
        object[] objs = lf3.Call(1000);
        for( int i = 0; i < objs.Length; ++i )
        {
            Debug.Log("第" + i + "个返回值是：" + objs[i]);
        }

        //变长参数
        CustomCall5 call5 = LuaManager.GetInstance().Global.Get<CustomCall5>("testFun4");
        call5("123", 1, 2, 3, 4, 5, 566, 7, 7, 8, 9, 99);

        LuaFunction lf4 = LuaManager.GetInstance().Global.Get<LuaFunction>("testFun4");
        lf4.Call("456", 6, 7, 8, 99, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
#endif