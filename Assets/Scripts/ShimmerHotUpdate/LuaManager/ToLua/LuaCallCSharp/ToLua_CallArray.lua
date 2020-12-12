print("**************toLua访问C#的数组******************")

--自定义类 需要在CustomSetting中去添加 并生成
local obj = Lesson3()

--数组的长度
print(obj.array.Length)

--访问元素
print(obj.array[0])

--查找元素
print("查找元素位置:" .. obj.array:IndexOf(2))

--遍历 lua中是从1开始
--但是数组是C#那边的结构 规则和C#一样
for i = 0, obj.array.Length - 1 do
    print("Array:" .. obj.array[i])
end

--toLua中比xLua多了几种遍历方式
--迭代器遍历
local iter = obj.array:GetEnumerator()
while iter:MoveNext() do
    print("iter:" .. iter.Current)
end

--转table遍历
local t = obj.array:ToTable()
for i = 1, #t do
    print("table:" .. t[i])
end

--创建数组
--如果出现不认识的类 记住！！！！！！CustomSetting
local array2 = System.Array.CreateInstance(typeof(System.Int32), 10)
print(array2.Length)
print(array2[0])
print(array2[1])
array2[0] = 99
print(array2[0])