using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//引用命名空间
using XLua;

public class Lesson1_LuaEnv : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Lua解析器 能够让我们在Unity中执行Lua
        //一般情况下 保持它的唯一性
        LuaEnv env = new LuaEnv();

        //执行Lua语言
        env.DoString("print('你好世界')");

        //执行一个Lua脚本 Lua知识点 ：多脚本执行 require
        //默认寻找脚本的路径 是在 Resources下 并且 因为在这里
        //估计是通过 Resources.Load去加载Lua脚本  txt bytes等等
        //所以Lua脚本 后缀要加一个txt
        env.DoString("require('Main')");

        //帮助我们清楚Lua中我们没有手动释放的对象 垃圾回收
        //帧更新中定时执行 或者 切场景时执行
        env.Tick();

        //销毁Lua解析器 
        env.Dispose();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
