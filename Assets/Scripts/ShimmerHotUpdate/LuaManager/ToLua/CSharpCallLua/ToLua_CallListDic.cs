using System.Collections;
using System.Collections.Generic;
using LuaInterface;
using UnityEngine;

#if ToLua
public class ToLua_CallListDic : MonoBehaviour
{
    void Start()
    {
        //主要学习目标 学会tolua 如何在C#中调用Lua中table表现的List和Dictionary
        LuaManager.GetInstance().Init();
        LuaManager.GetInstance().DoLuaFile("Main");

        //List相关
        //toLua中 C#得到Lua中的表 都只有一个套路 通过LuaTable来获取
        LuaTable table = LuaManager.GetInstance().LuaState.GetTable("testList");
        Debug.Log(table[1]);
        Debug.Log(table[2]);
        Debug.Log(table[3]);
        Debug.Log(table[4]);
        Debug.Log(table[5]);
        //如果要遍历LuaTable对应的table 需要先转数组
        object[] objs = table.ToArray();
        for (int i = 0; i < objs.Length; i++)
        {
            Debug.Log("遍历打印：" + objs[i]);
        }
        //如果是修改 是否是引用拷贝呢？
        table[1] = 999;
        LuaTable tableTmp = LuaManager.GetInstance().LuaState.GetTable("testList");
        Debug.Log("测试引用拷贝：" + tableTmp[1]);

        LuaTable table2 = LuaManager.GetInstance().LuaState.GetTable("testList2");
        Debug.Log(table2[1]);
        Debug.Log(table2[2]);
        Debug.Log(table2[3]);
        Debug.Log(table2[4]);
        Debug.Log(table2[5]);

        objs = table2.ToArray();
        for (int i = 0; i < objs.Length; i++)
        {
            Debug.Log("遍历打印：" + objs[i]);
        }

        //Dictironary相关
        LuaTable dic = LuaManager.GetInstance().LuaState.GetTable("testDic");
        Debug.Log(dic["1"]);
        Debug.Log(dic["2"]);
        Debug.Log(dic["3"]);
        Debug.Log(dic["4"]);
        LuaDictTable<string, int> luaDic = dic.ToDictTable<string, int>();
        Debug.Log("luaDic:" + luaDic["1"]);
        Debug.Log("luaDic:" + luaDic["2"]);
        Debug.Log("luaDic:" + luaDic["3"]);
        Debug.Log("luaDic:" + luaDic["4"]);

        //dic也是引用拷贝 只要通过LuaTable拷贝出来的 都是引用拷贝 C#中改了 lua中也会变
        dic["1"] = 9999;
        LuaTable dicTmp = LuaManager.GetInstance().LuaState.GetTable("testDic");
        Debug.Log("引用拷贝测试：" + dicTmp["1"]);

        //通过中括号得到值 只支持 int和string 其它类型的 没办法直接来获取
        LuaTable dic2 = LuaManager.GetInstance().LuaState.GetTable("testDic2");
        //Debug.Log(dic[true]);
        LuaDictTable<object, object> luaDic2 = dic2.ToDictTable<object, object>();
        Debug.Log(luaDic2[true]);
        Debug.Log(luaDic2["123"]);
        Debug.Log(luaDic2[false]);

        //dic建议 使用 迭代器遍历
        IEnumerator<LuaDictEntry<object, object>> ie = luaDic2.GetEnumerator();
        while(ie.MoveNext())
        {
            Debug.Log(ie.Current.Key + "_" + ie.Current.Value);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
#endif