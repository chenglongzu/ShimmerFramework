using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;

public class XLua_Loader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LuaEnv env = new LuaEnv();

        //xlua提供的一个 路径重定向 的方法
        //允许我们自定义 加载 Lua文件的规则
        //当我们执行Lua语言 require 时 相当于执行一个lua脚本
        //它就会 执行 我们自定义传入的这个函数
        env.AddLoader(MyCustomLoader);
        //最终我们其实 会去AB包中加载 lua文件 

        env.DoString("require('Main')");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //自动执行
    private byte[] MyCustomLoader(ref string filePath)
    {
        //通过函数中的逻辑 去加载 Lua文件 
        //传入的参数 是 require执行的lua脚本文件名
        //拼接一个Lua文件所在路径
        string path = Application.dataPath + "/Lua/" + filePath + ".lua";
        Debug.Log(path);

        //有路径 就去加载文件 
        //File知识点 C#提供的文件读写的类
        //判断文件是否存在
        if ( File.Exists(path) )
        {
            return File.ReadAllBytes(path);
        }
        else
        {
            Debug.Log("MyCustomLoader重定向失败，文件名为" + filePath);
        }

        
        return null;
    }
}
