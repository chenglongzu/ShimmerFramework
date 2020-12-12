print("**************toLua访问C#的枚举******************")

--枚举的调用规则 和 类的调用规则是一样
--命名空间.枚举名.枚举成员
--也支持取别名

--调用Unity自带的枚举
--如果报错 需要在CustomSetting中去加上 然后生成代码
PrimitiveType = UnityEngine.PrimitiveType
GameObject = UnityEngine.GameObject
--GameObject中的静态方法 创建 几何体
local obj = GameObject.CreatePrimitive(PrimitiveType.Cube)

--调用我们自定义的枚举
local c = E_MyEnum.Idle
local d = E_MyEnum.Move
--userdata类型 保留了语言中的数据类型
print(c)

if c == d then
    print("枚举比较")
else
    print("枚举不相等")
end

print("枚举转字符串")
print(tostring(c))
print("枚举转数字")
print(c:ToInt())
print(d:ToInt())

print("数字转枚举")
print(E_MyEnum.IntToEnum(0))
print(tostring(E_MyEnum.IntToEnum(0)))
print(E_MyEnum.IntToEnum(1))
print(tostring(E_MyEnum.IntToEnum(1)))
--和xLua的区别 没有提供字符串转枚举的功能