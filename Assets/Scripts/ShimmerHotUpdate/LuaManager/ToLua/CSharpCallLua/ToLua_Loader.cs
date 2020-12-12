using System.Collections;
using System.Collections.Generic;
using LuaInterface;
using ShimmerFramework;
using UnityEngine;

public class ToLua_Loader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //主要学习目的 学会通过toLua的解析器 自定义解析路径

        //之所以自定义解析类 new了就可以用 是因为 父类是个单例模式对象
        //并且父类的构造函数中 初始化了instance变量
        new LuaCustomLoader();

        //申明Lua解析器 用于执行Lua代码
        LuaState luaState = new LuaState();
        luaState.Start();

        //第二种 使用LuaState中的方法 AddSearchPath
        //luaState.AddSearchPath(Application.dataPath + "/Lua/CSharpCallLua");
        //luaState.Require("Lesson2_Loader");

        //第一种 如果该文件 属于 Lua文件夹下 那么可以直接加父目录
        luaState.Require("CSharpCallLua/Lesson2_Loader");
        
        //测试 新建文件夹的加载
        //luaState.AddSearchPath(Application.dataPath + "/MyLua");
        //移除一个搜索路径 用的很少
        //luaState.RemoveSeachPath(Application.dataPath + "/MyLua");
        //luaState.Require("Leeson2_Loader2");


        //xLua自定义解析方式 AddLoader 把解析函数传入
        //主要学习目标 学会通过toLua的解析器 自定义解析方式 
        //要实现我们的自定义解析方式  就是新建一个类 继承 LuaFileUtils 然后重写加载函数 就达到了自定义的目的

    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
