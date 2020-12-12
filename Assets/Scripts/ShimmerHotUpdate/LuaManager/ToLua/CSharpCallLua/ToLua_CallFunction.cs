using System;
using System.Collections;
using System.Collections.Generic;
using LuaInterface;
using UnityEngine;
using UnityEngine.Events;


#if ToLua

public delegate int CutomCallOut(int a, out int b, out bool c, out string d, out int e);
public delegate int CutomCallRef(int a, ref int b, ref bool c, ref string d, ref int e);

public delegate void CustomCallParams(int a, params object[] objs);

public class ToLua_CallFunction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LuaManager.GetInstance().Init();
        LuaManager.GetInstance().DoLuaFile("Main");

        #region 无参无返回
        //第一种方法 通过 GetFunction方法获取
        LuaFunction function = LuaManager.GetInstance().LuaState.GetFunction("testFun");
        //调用 无参无返回
        function.Call();
        //执行完过后 
        function.Dispose();

        //第二种方法 通过 中括号 函数名的形式
        function = LuaManager.GetInstance().LuaState["testFun"] as LuaFunction;
        function.Call();
        function.Dispose();

        //第三种方式 转为委托来使用
        //首先得到一个luafunction 然后再转成委托的形式
        function = LuaManager.GetInstance().LuaState.GetFunction("testFun");
        //通过luafunction中的方法 存入到委托中再使用
        UnityAction action = function.ToDelegate<UnityAction>();
        action();
        function.Dispose();
        #endregion
    
        #region 有参有返回
        //第一种方式 通过luafunction 的 call来访问
        function = LuaManager.GetInstance().LuaState.GetFunction("testFun2");
        //开始使用
        function.BeginPCall();
        //传参
        function.Push(99);
        //传参结束 调用
        function.PCall();
        //得到返回值
        int result = (int)function.CheckNumber();
        //执行结束
        function.EndPCall();
        Debug.Log("有参有返回值 Call:" + result);

        //第二种方式 通过 luaFunctionde Invoke方法来调用
        //最后一个泛型类型 是返回值类型 前面的 是参数类型 依次往后排
        result = function.Invoke<int, int>(199);
        Debug.Log("有参有返回值 Invoke:" + result);

        //第三种方式 转委托
        //最后一个泛型类型 是返回值类型 前面的 是参数类型 依次往后排
        Func<int, int> func = function.ToDelegate<Func<int, int>>();
        result = func(900);
        Debug.Log("有参有返回值 委托:" + result);

        //第四种方式 不得function 直接执行
        result = LuaManager.GetInstance().LuaState.Invoke<int, int>("testFun2", 800, true);
        Debug.Log("有参有返回值 通过解析器直接调用:" + result);
        #endregion
    
        #region 多返回值函数
        //第一种方式 通过Call的形式
        function = LuaManager.GetInstance().LuaState.GetFunction("testFun3");
        //开始调用
        function.BeginPCall();
        //传参
        function.Push(1000);
        //传参结束 开始调用
        function.PCall();
        //得到返回值
        int a1 = (int)function.CheckNumber();
        int b1 = (int)function.CheckNumber();
        bool c1 = function.CheckBoolean();
        string d1 = function.CheckString();
        int e1 = (int)function.CheckNumber();
        //执行结束
        function.EndPCall();
        Debug.Log("Check接的第一个返回值:" + a1);
        Debug.Log(b1 + "_" + c1 + "_" + d1 + "_" + e1);

        //第二种方式 通过 out来接
        //如果是自定义委托 用来状Lua 必须做一件非常重要的事情 就是在
        //关键文件 CustomSetting customDelegateList中去加上自定义委托
        //然后必须要 在菜单栏 Lua中去生成Lua代码 不然会报错
        CutomCallOut callOut = function.ToDelegate<CutomCallOut>();
        int b2;
        bool c2;
        string d2;
        int e2;
        result = callOut(999, out b2, out c2, out d2, out e2);
        Debug.Log("Out接的第一个返回值:" + result);
        Debug.Log(b2 + "_" + c2 + "_" + d2 + "_" + e2);

        //第三种方式 通过 ref来接
        CutomCallRef callRef = function.ToDelegate<CutomCallRef>();
        int b3 = 0;
        bool c3 = true;
        string d3 = "";
        int e3 = 0;
        result = callRef(999, ref b3, ref c3, ref d3, ref e3);
        Debug.Log("Out接的第一个返回值:" + result);
        Debug.Log(b3 + "_" + c3 + "_" + d3 + "_" + e3);

        #endregion

        #region 变长参数函数
        //第一种方式 通过自定义委托来执行
        function = LuaManager.GetInstance().LuaState.GetFunction("testFun4");
        //通过委托装载它
        CustomCallParams call = function.ToDelegate<CustomCallParams>();
        call(100, 1, true, "123", 0, false);

        //第二种方式 通过luaFunction里面直接call
        function.Call<int, int, bool, string, int>(100, 10, true, "123", 0);
        
        #endregion

        
    }
}

#endif