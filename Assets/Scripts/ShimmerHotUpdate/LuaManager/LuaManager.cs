using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ShimmerHotUpdate
{
    using ShimmerFramework;

#if XLua

    #region XLuaManager
    using XLua;


    /// <summary>
    /// Lua管理器
    /// 提供 lua解析器
    /// 保证解析器的唯一性
    /// </summary>
    public class LuaManager : BaseManager<LuaManager>
    {
        //Xlua运行环境 由外部实例化后调用管理器里的方法
        private LuaEnv luaEnv;

        //获得大G表
        public LuaTable Global
        {
            //外部通过调用Get函数
            get
            {
                return luaEnv.Global;
            }
        }


        /// <summary>
        /// 初始化解析器
        /// </summary>
        public void Init()
        {
            //保证Lua解析器的唯一性
            if (luaEnv != null)
                return;
            luaEnv = new LuaEnv();

            //委托监听 重定向Lua脚本的加载路径
            luaEnv.AddLoader(MyCustomLoader);
            luaEnv.AddLoader(MyCustomABLoader);
        }

        //委托方法 重定向lua脚本的加载的路径 定向到Lua文件夹中
        private byte[] MyCustomLoader(ref string filePath)
        {
            string path = Application.dataPath + "/Lua/" + filePath + ".lua";

            //通过返回值来判断是否加载到lua脚本
            if (File.Exists(path))
            {
                return File.ReadAllBytes(path);
            }
            else
            {
                Debug.Log("MyCustomLoader重定向失败，文件名为" + filePath);
            }

            return null;
        }


        //委托方法 重定向lua脚本的加载的路径 定向到AB包加载
        private byte[] MyCustomABLoader(ref string filePath)
        {
            //通过我们的AB包管理器 加载的lua脚本资源
            TextAsset lua = AssetBundleManager.GetInstance().LoadResources<TextAsset>("lua", filePath + ".lua");
            if (lua != null)
                return lua.bytes;
            else
                Debug.Log("MyCustomABLoader重定向失败，文件名为：" + filePath);

            return null;
        }


        // 传入lua文件名 执行lua脚本
        public void DoLuaFile(string fileName)
        {
            string str = string.Format("require('{0}')", fileName);
            DoString(str);
        }

        // 执行Lua语言
        public void DoString(string str)
        {
            if (luaEnv == null)
            {
                Debug.Log("解析器未初始化");
                return;
            }

            luaEnv.DoString(str);
        }

        // 释放lua 垃圾
        public void Tick()
        {
            if (luaEnv == null)
            {
                Debug.Log("解析器为初始化");
                return;
            }
            luaEnv.Tick();
        }

        // 销毁解析器
        public void Dispose()
        {
            if (luaEnv == null)
            {
                Debug.Log("解析器为初始化");
                return;
            }
            luaEnv.Dispose();
            luaEnv = null;
        }
    }

    #endregion

#elif ToLua

    #region ToLuaManager
using LuaInterface;

public class LuaManager : SingletonAutoMono<LuaManager>
{
    private LuaState luaState;

    public void Init()
    {
#if UNITY_EDITOR

#else
        //最后打包的时候 再用 再取消注释 重定向lua解析方式
        new LuaCustomLoader();
#endif

        //就是初始化 唯一的 luaState
        luaState = new LuaState();
        luaState.Start();

        //为Lua解析器添加一个新的解析路径
        //luaState.AddSearchPath(Application.dataPath + "/Lua/CSharpCallLua");

        //委托工厂初始化
        DelegateFactory.Init();

        //协程相关
        LuaLooper loop = this.gameObject.AddComponent<LuaLooper>();
        loop.luaState = this.luaState;

        //Lua中访问Unity中的类相关
        LuaBinder.Bind(luaState);
    }

    /// <summary>
    /// 该属性可以让外部获取到 解析器
    /// </summary>
    /// <value></value>
    public LuaState LuaState
    {
        get
        {
            return luaState;
        }
    }

    /// <summary>
    /// 提供一个外部执行 lua语法字符串的 方法
    /// </summary>
    /// <param name="str">lua语法字符串</param>
    /// <param name="chunkName">执行出处</param>
    public void DoString(string str, string chunkName = "LuaMgr.cs")
    {
        luaState.DoString(str, chunkName);
    }

    /// <summary>
    /// 执行指定名字的lua脚本
    /// </summary>
    /// <param name="fileName">lua脚本名</param>
    public void DoLuaFile(string fileName)
    {
        luaState.Require(fileName);
    }

    /// <summary>
    /// 销毁lua解析器
    /// </summary>
    public void Dispose()
    {
        if (luaState == null)
            return;
        //检查解析器栈顶是否为空
        luaState.CheckTop();
        luaState.Dispose();
        luaState = null;
    }
}

    #endregion

#endif
}