print("*********Lua调用C# nil和null比较的相关知识点***********")

--往场景对象上添加一个脚本 如果存在就不加 如果不存在再加
GameObject = CS.UnityEngine.GameObject
Rigidbody = CS.UnityEngine.Rigidbody

local obj = GameObject("测试加脚本")
--得到身上的刚体组件  如果没有 就加 有就不管
local rig = obj:GetComponent(typeof(Rigidbody))
print(rig)
--判断空
--nil和null 没法进行==比较
--第一种方法
--if rig:Equals(nil) then
--if IsNull(rig) then
if rig:IsNull() then
	print("123")
	rig = obj:AddComponent(typeof(Rigidbody))
end
print(rig)
