print("*********Lua调用C# 协程相关知识点***********")
--xlua提供的一个工具表
--一定是要通过require调用之后 才能用
util = require("xlua.util")
--C#中协程启动都是通过继承了Mono的类 通过里面的启动函数StartCoroutine

GameObject = CS.UnityEngine.GameObject
WaitForSeconds = CS.UnityEngine.WaitForSeconds
--在场景中新建一个空物体  然后挂一个脚本上去 脚本继承mono使用它来开启协程
local obj = GameObject("Coroutine")
local mono = obj:AddComponent(typeof(CS.LuaCallCSharp))

--希望用来被开启的协程函数 
fun = function()
	local a = 1
	while true do
		--lua中 不能直接使用 C#中的 yield return 
		--就使用lua中的协程返回
		coroutine.yield(WaitForSeconds(1))
		print(a)
		a = a + 1
		if a > 10 then
			--停止协程和C#当中一样
			mono:StopCoroutine(b)
		end
	end
end
--我们不能直接将 lua函数传入到开启协程中！！！！！
--如果要把lua函数当做协程函数传入
--必须 先调用 xlua.util中的cs_generator(lua函数)
b = mono:StartCoroutine(util.cs_generator(fun))