
#if XLua
using UnityEngine;
using UnityEngine.Events;
using XLua;
using ShimmerHotUpdate;

//接口中是不允许有成员变量的 
//我们用属性来接受
//接口和类规则一样 其中的属性多了少了 不影响结果 无非就是忽略他们
//嵌套几乎和类一样 无非 是要遵循接口的规则
[CSharpCallLua]
public interface ICSharpCallInterface
{
    int testInt
    {
        get;
        set;
    }

    bool testBool
    {
        get;
        set;
    }

    ////float testFloat
    ////{
    ////    get;
    ////    set;
    ////}

    string testString
    {
        get;
        set;
    }

    UnityAction testFun
    {
        get;
        set;
    }

    float testFloat222
    {
        get;
        set;
    }
}

public class XLua_CallInterface : MonoBehaviour
{
    void Start()
    {
        XLuaManager.GetInstance().Init();
        XLuaManager.GetInstance().DoLuaFile("Main");

        ICSharpCallInterface obj = XLuaManager.GetInstance().Global.Get<ICSharpCallInterface>("testClas");
        Debug.Log(obj.testInt);
        Debug.Log(obj.testBool);
        Debug.Log("新加的：" + obj.testFloat222);
        Debug.Log(obj.testString);
        obj.testFun();

        //接口拷贝 是引用拷贝 改了值 lua表中的值也变了
        obj.testInt = 10000;
        ICSharpCallInterface obj2 = XLuaManager.GetInstance().Global.Get<ICSharpCallInterface>("testClas");
        Debug.Log(obj2.testInt);


    }
}
#endif