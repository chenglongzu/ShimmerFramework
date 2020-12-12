using LuaInterface;
using UnityEngine;
using ShimmerHotUpdate;

namespace ShimmerFramework
{
    /// <summary>
    /// 最后项目打包发布后才使用自定义解析器
    /// </summary>
    public class LuaCustomLoader : LuaFileUtils
    {
        public override byte[] ReadFile(string fileName)
        {
            //Debug.Log("自定义解析方式" + fileName);
            //如果想要重新定义 解析lua的方式 那么只需要在该函数中去写逻辑即可

            //如果没有lua后缀 加上lua后缀 不管从AB包中加载还是从res下加载 都不支持用.lua后缀 所以toLua加上了bytes后缀
            //我们自己可以加上.txt后缀
            if (!fileName.EndsWith(".lua"))
            {
                fileName += ".lua";
            }
            byte[] buffer = null;
            //因为 进行热更新的lua代码 肯定是我们自己写的上层lua逻辑

            //第二种 从AB中加载lua文件
            //CSharpCallLua/Lesson2_Loader这样的名字 但是在AB中我们只需要文件名 所以需要拆分一下
            string[] strs = fileName.Split('/');
            //加载AB包中的lua文件
            TextAsset luaCode = AssetBundleManager.GetInstance().LoadResources<TextAsset>("lua", strs[strs.Length - 1]);
            if (luaCode != null)
            {
                buffer = luaCode.bytes;
                Resources.UnloadAsset(luaCode);
            }

            //toLua的自带逻辑和自带lua类 我们不太需要去热更新 直接从resources下去加载即可
            if (buffer == null)
            {
                //第一种 从resources中加载lua文件 通过 Lua——> copy to res  它只能把Lua文件夹下的自定义文件拷贝到res下
                string path = "Lua/" + fileName;
                TextAsset text = Resources.Load<TextAsset>(path);
                if (text != null)
                {
                    buffer = text.bytes;
                    //卸载使用后的 文本资源 
                    Resources.UnloadAsset(text);
                }
            }
            return buffer;
        }
    }
}