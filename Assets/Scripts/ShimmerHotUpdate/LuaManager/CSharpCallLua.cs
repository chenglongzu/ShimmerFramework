using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShimmerHotUpdate
{
#if XLua
    using XLua;

    #region XLua中三种用来接收函数的委托
    //必须在自定义委托上加这个关键字 在引擎中重写生成wrap文件
    [CSharpCallLua] //无参无返回值 
    public delegate void ReceiveFunction1();

    [CSharpCallLua] //有参多返回值 定义委托，第一个返回值参数可以直接调用函数来接收
                    //其他参数使用out 和ref参数来接收
    public delegate int ReceiveFunction2(int a, ref bool b, out char c, out int d);

    //params 用在传递参数时参数的长度不固定的时候 可用来传递多个同类型的参数
    [CSharpCallLua] //变长参数 且关键词只能用在最后一个参数的位置
    public delegate int ReceiveFunction3(string a, params int[] args);
    #endregion

#elif ToLua
using LuaInterface;

    #region //ToLua中的委托要使用一定要在CustomSetting中添加,然后在引擎Generate一下。

public delegate int ReceiveFunction_1(string a,out int b);

    #endregion
#endif

    public class CSharpCallLua : MonoBehaviour
    {
        void Start()
        {
#if XLua

            LuaManager.GetInstance().DoLuaFile("main");

            int luaVariable = LuaManager.GetInstance().Global.Get<int>("variable name");

            #region 获取Lua函数
            //获取Lua中的函数
            ReceiveFunction1 receiveFunction1 = LuaManager.GetInstance().Global.Get<ReceiveFunction1>("");
            Action action = LuaManager.GetInstance().Global.Get<Action>("");

            //当只有一个返回值的时候可以使用C#的内置委托func来接收
            ReceiveFunction2 receiveFunction2 = LuaManager.GetInstance().Global.Get<ReceiveFunction2>("");
            bool b = false;
            char c;
            int d;
            //在调用函数时，第一个值为函数的参数，第一个函数的返回值由调用函数来接收返回值
            //其他返回值使用ref out参数传出
            receiveFunction2(100, ref b, out c, out d);

            ReceiveFunction3 receiveFunction3 = LuaManager.GetInstance().Global.Get<ReceiveFunction3>("");
            receiveFunction3("string", 1, 2, 3);

            #endregion

            #region lua映射list<>和Dictionary<> 全部由Xlua封装完毕
            List<int> resoultList = LuaManager.GetInstance().Global.Get<List<int>>("");
            Dictionary<string, int> resoultDictionary = LuaManager.GetInstance().Global.Get<Dictionary<string, int>>("");
            #endregion

            #region Lua映射类

            //值引用，修改过变量以后原来的值不会改变
            XLuaCallLuaClass callLuaClass_1 = LuaManager.GetInstance().Global.Get<XLuaCallLuaClass>("");

            XLuaCallLuaClass callLuaClass_2 = LuaManager.GetInstance().Global.Get<XLuaCallLuaClass>("");

            #endregion

            #region Lua映射接口
            //存储在引用空间
            //ICallInterface iCallInterface = LuaManager.GetInstance().Global.Get<ICallInterface>("");
            #endregion

            #region Lua映射LuaTable表
            //不建议使用LuaTable和LuaFunction执行效率低
            //存储在引用空间
            LuaTable luaTable = LuaManager.GetInstance().Global.Get<LuaTable>("");
            #endregion
#elif ToLua

        LuaManager.GetInstance().DoLuaFile("Main");

        //获得Lua中的变量值
        object luaVariacle =LuaManager.GetInstance().LuaState["变量的键"];
        //C#中无法获得Lua中loacl修饰的变量

            #region C#CallLuaFunction
        //获得Lua中的函数
            #region 无参无返回值
        //第一种方法 通过LuaManager中的GetFunction函数获取方法
        LuaFunction function = LuaManager.GetInstance().LuaState.GetFunction("函数名");
        function.Call();    //调用函数方法
        function.Dispose(); //废弃函数

        //第二种方法 通过得到的Luafunction转化成委托的形式然后再使用。
        UnityAction action = function.ToDelegate<UnityAction>();
            #endregion

            #region 有参有返回值
        //第一种方法
        LuaFunction function_1 = LuaManager.GetInstance().LuaState.GetFunction("函数名");
        function_1.BeginPCall();
        function_1.Push(99);
        function_1.PCall();
        int resoult = (int)function_1.CheckNumber();

        function.EndPCall();

        //第二种方法 前面的参数都是参数类型，后面的都是返回值的类型。
        int resoult_1 = function_1.Invoke<int, int>(199);

        //第三种方式 转化成委托的形式
        Func<int, int> func = function_1.ToDelegate<Func<int, int>>();

        int resoult_2 = func(900);

        //第四种方式
        int resoult_3 = LuaManager.GetInstance().LuaState.Invoke<int, int>("testFun2",800,true);
            #endregion

            #region 多返回值函数
        //第一种方法
        LuaFunction luafunction_2 = LuaManager.GetInstance().LuaState.GetFunction("testFun3");
        luafunction_2.BeginPCall();
        luafunction_2.Push(1000);

        luafunction_2.PCall();

        int a1 = (int)function.CheckNumber();
        int b1 = (int)function.CheckNumber();

        luafunction_2.EndPCall();

        //第二种方法
        ReceiveFunction_1 receiveFunction = luafunction_2.ToDelegate<ReceiveFunction_1>();
        int b;

        int resoult_4 = receiveFunction("string",out b);
        //其他的返回值由外部int b调用

            #endregion
            #endregion


            #region C#中获取Lua中的表

        LuaTable luaTable = LuaManager.GetInstance().LuaState.GetTable("table name");

        object[] objects = luaTable.ToArray();

        for (int i = 0; i < objects.Length; i++)
        {
            //luaTable需要转换成objects数组之后才可以遍历
            //数据类型是引用类型 修改的话Lua中也是修改的
        }

        //当LuaTable转化为Dic时。通过键值对来获取值的话只能当键时int或者是string时。
        LuaDictTable<object, object> luaDicTable = luaTable.ToDictTable<object,object>();
        //将LuaTable转化为LuaDicTable后就可以使用任何键来输出了。list和dictionary同样都是引用拷贝。

            #endregion

            #region C#中获取Lua中的类
        LuaTable luatable2 = LuaManager.GetInstance().LuaState.GetTable("ClassName");

        Debug.Log(luatable2["变量名"]);

        LuaFunction luaFunction_3 = luatable2.GetLuaFunction("testFun");
        luaFunction_3.Call();
            #endregion
#endif

        }

    }
}