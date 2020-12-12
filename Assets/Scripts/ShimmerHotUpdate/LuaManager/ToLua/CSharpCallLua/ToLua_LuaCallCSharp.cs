using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//类
#region ToLua CallClass
public class Test
{
    public void Speak(string str)
    {
        Debug.Log("Test:" + str);
    }
}

namespace MrTang
{
    public class Test2
    {
        public void Speak(string str)
        {
            Debug.Log("Test2:" + str);
        }
    }
}

#endregion

//枚举
#region Lesson2 CallEnum
/// <summary>
/// 自定义枚举
/// </summary>
public enum E_MyEnum
{
    Idle,
    Move,
    Atk,
}
#endregion

//数组
#region Lesson3 CallArray List Dic
public class Lesson3
{
    public int[] array = new int[5]{1,2,3,4,5};

    public List<int> list = new List<int>();
    
    public Dictionary<int, string> dic = new Dictionary<int, string>();
}
#endregion

//拓展方法相关
#region Lesson4 CallFUnction 

public static class Tools
{
    public static void Move(this Lesson4 obj)
    {
        Debug.Log(obj.name + "移动");
    }
}

public class Lesson4
{
    public string name = "唐老狮";
    public void Speak(string str)
    {
        Debug.Log(str);
    }

    public static void Eat()
    {
        Debug.Log("吃东西");
    }
}
#endregion

//ref和out
#region Lesson5 CallFunction ref out
public class Lesson5
{
    public int RefFun(int a, ref int b, ref int c, int d)
    {
        b = a + d;
        c = a - d;
        return 100;
    }

    public int OutFun(int a, out int b, out int c, int d)
    {
        b = a;
        c = d;
        return 200;
    }

    public int RefOutFun(int a, out int b, ref int c)
    {
        b = a * 10;
        c = a * 20;
        return 300;
    }
}
#endregion

//重载函数
#region Lesson6 CallFunction 重载

public class Lesson6
{
    public int Calc()
    {
        return 100;
    }

    public int Calc(int a)
    {
        Debug.Log("Int");
        return a;
    }

    public float Calc(float a)
    {
        Debug.Log("Float");
        return a;
    }

    public string Calc(string a)
    {
        Debug.Log("string");
        return a;
    }

    public int Calc(int a, int b)
    {
        return a + b;
    }

    public int Calc(int a, out int b)
    {
        b = 10;
        return a + b;
    }
}

#endregion

//委托 事件
#region Lesson7 CallDel Event
public class Lesson7
{
    public UnityAction del;

    public event UnityAction eventAction;

    public void DoDel()
    {
        if(del != null)
            del();
    }

    public void DoEvent()
    {
        if( eventAction != null )
            eventAction();
    }

    public void ClearEvent()
    {
        eventAction = null;
    }
}
#endregion
