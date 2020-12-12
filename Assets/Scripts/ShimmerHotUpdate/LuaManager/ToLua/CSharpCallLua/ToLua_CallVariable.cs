using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if ToLua
public class ToLua_CallVariable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LuaManager.GetInstance().Init();

        LuaManager.GetInstance().DoLuaFile("Main");

        //获取全局变量
        //toLua中访问全局变量 一个套路 得到luastate解析器 然后中括号 变量名 即可得到
        Debug.Log("testNumber:" + LuaManager.GetInstance().LuaState["testNumber"]);
        Debug.Log("testBool:" + LuaManager.GetInstance().LuaState["testBool"]);
        Debug.Log("testFloat:" + LuaManager.GetInstance().LuaState["testFloat"]);
        Debug.Log("testString:" + LuaManager.GetInstance().LuaState["testString"]);

        //值拷贝
        int value = Convert.ToInt32(LuaManager.GetInstance().LuaState["testNumber"]);
        value = 99;
        Debug.Log("testNumber:" + LuaManager.GetInstance().LuaState["testNumber"]);
        //如果要改值 直接这样写即可
        LuaManager.GetInstance().LuaState["testNumber"] = 99;
        Debug.Log("testNumber:" + LuaManager.GetInstance().LuaState["testNumber"]);

        //toLua中 也没有办法通过C#得到local申明的局部临时变量
        Debug.Log("testLocal:" + LuaManager.GetInstance().LuaState["testLocal"]);

        //可以在C#处为lua添加全局变量 相当于在lua中的_G中加了一个变量
        Debug.Log("addValue:" + LuaManager.GetInstance().LuaState["addValue"]);
        LuaManager.GetInstance().LuaState["addValue"] = 999;
        Debug.Log("addValue:" + LuaManager.GetInstance().LuaState["addValue"]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
#endif