--XLua
--有括号的是对象，没括号的是类
--声明别名 获取unity引擎或是C#内置的类
GameObject=CS.UnityEngine.GameObject
Debug=CS.UnityEngine.Debug
Vector3=CS.UnityEngine.Vector3
UI=CS.UnityEngine.UI
WaitForSeconds=CS.UnityEngine.WaitForSeconds
--没有命名空间直接CS.脚本名
GameManager=CS.GameManager

--通过之前设置的别名来使用这个类
local  obj = GameObject("ulognn")

print("_______________Enum______________")

--声明别名获取C#中的枚举 可通过字符串数值的方式获取
--自定义变量来接收自定义的枚举，格式时CS.命名空间.枚举名
MyEnum=CS.MyEnum

local state_1 = MyEnum.Walk
--固定方法，传值获取枚举值
local state_2 = MyEnum.__CastFrom(1)
--固定方法，传递字符串获取枚举值
local state_3 = MyEnum.__CastFrom("Run")


print("_______________Arry______________")
--调用
--首先获取C#中的类，然后通过类调用类中的数组
--调用数组的长度时 用C#的语法格式 .Length的方法，不能用Lua中的#方式获取 
--C#中的数据保证是Public，外部可访问
local array_1 = GameManager().arry
print(array_1.Length)

--遍历C#中的数组索引要从0开始，遍历的长度要以数组的长度减一
for i=0,array_1.Length-1 do
	--print(array_1[i])
end

--创建
--在Lua中创建C#中的数组 
--调用C#System类中的Arry类中的GetInstance方法 两个参数一个是数据类型另一个是数组的长度
local array2=CS.System.Array.CreateInstance(typeof(CS.System.Int32),10)


print("_______________List______________")
--调用
local listArry = GameManager().listArry
print(listArry.Count)

--在Lua中创建C#中的list
--参数传递数值类型 获得了一个list类的别名 需要再次实例化
local list= CS.System.Collections.Generic.List(CS.System.String)
local list_String = list()

--打印数值为0 说明默认创建了一个空list
print(list_String.Count)
--调用List的内部方法 调用内部方法是使用： 调用静态方法是使用.
list_String:Add("111")
list_String:Add("222")
list_String:Add("333")

print(list_String.Count)

print("_______________Dictionary______________")
--调用

--在Lua中创建C#中的Dictionary
--参数传递键值对的类型 相当于创建了一个类的别名 需要再次实例化创建
local dicString = CS.System.Collections.Generic.Dictionary(CS.System.String,CS.UnityEngine.Vector3)
local list_String = dicString()
list_String:Add("1",Vector3(1,1,1))

print(list_String.Count)

print("_______________CallFunction______________")
--普通方法和静态方法 普通方法要“实例化”后调用“：”号调用 静态方法直接使用“.”号调用
GameManager():TestFunction()
GameManager.TestStaticFunction()

--当C#函数中有ref out参数时在lua中用多返回值来接收
--参数方面ref要传递参数和ref占位参数
local a,b,c= GameManager():TestReturnFunction(10,0,0)


print(a)
print(b) 
print(c)

print("_______________Delegent______________")

--C#中的委托变量要保持静态
--使用类名点的方式获取C#中的委托变量
--将lua脚本中的函数赋值给委托
--第一个使用= 第二个以后使用+的方法
--最后调用的时候使用调用静态方法的格式

fun_1=function ( ... )
	-- body
	print("第一次执行委托")
end
fun_2=function ( ... )
	-- body
	print("第二次执行委托")
end

--添加函数
GameManager.luaDele=fun_1

GameManager.luaDele = GameManager.luaDele+fun_2

--减少函数
GameManager.luaDele=GameManager.luaDele -fun_1
GameManager.luaDele=GameManager.luaDele -fun_2

--清空函数
GameManager.luaDele=nil

--调用函数
--GameManager.luaDele()

print("_______________Event______________")

--[[
fun_3=function ( ... )
	-- body
	print("第一次执行事件")
end
fun_4=function ( ... )
	-- body
	print("第二次执行事件")
end
GameManager:luaEvent("+",fun_3)
GameManager:luaEvent("+",fun_4)

GameManager:DoEvent()
]]

print("_______________二维数组______________")

--获取二维数组的两个方向的长度
GameManager().twoArry:GetLength(0)
GameManager().twoArry:GetLength(1)

--获取二维书两个方向的值
--传递两个参数 获取二维数组的值
GameManager().twoArry:GetValue(0,1)

print("_______________Lua中nil和C#中Null______________")
--if rig:Equals(nil) then
--if IsNull(rig) then
print("_______________获取Component______________")
--[[GameObject.Find("Slider"):GetComponent(typeof(UI.Slider)).onValueChanged:AddListener(function(value)
	-- body
	print(value)
end)]]
print("_______________调用协同程序______________")
--调用xlua提供的一个工具表
--util = require("xlua.util")

--util = require("C")

local object = GameObject("Coroutine")
local mono = object:AddComponent(typeof(CS.LuaCallCSharp))


fun = function ( ... )
	-- body
	while true do
		coroutine.yield(WaitForSeconds(1))
		print(a)
		a= a + 1

		if a>10 then
			--mono:StopCoroution(b)
		end

	end
end

--b=mono.StartCoroution(util.cs_generator(fun))
print("*********Lua调用C# 泛型函数相关知识点***********")

GameManager():TestGenerticity(CS.String,"ulognnnnnnn")