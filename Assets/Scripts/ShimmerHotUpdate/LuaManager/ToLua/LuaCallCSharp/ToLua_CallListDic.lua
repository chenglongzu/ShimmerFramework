print("**************toLua访问C#的List和Dictionary******************")

--虽然自定义类Lesson3已经在 CustomSetting去添加了并且生成了
--但是我们改变了这个类 里面新加了 两个变量
--我们必须要在菜单栏中重新生成一次
local obj = Lesson3()
print("****List****")
obj.list:Add(10)
obj.list:Add(12)
obj.list:Add(15)
--得到其中一个元素
print(obj.list[0])
--长度
print("长度:" .. obj.list.Count)

--遍历
for i = 0, obj.list.Count - 1 do
    print("遍历" .. obj.list[i])
end

--在toLua中创建一个list
print("创建List")
--toLua它对泛型支持比较糟糕 想要用什么泛型类型的对象
--都需要在CustomSetting中去添加对应的类型 生成
--才能够在toLua中去使用
--List<string>
local list2 = System.Collections.Generic.List_string()
list2:Add("123")
print(list2[0])

print("****Dictionary****")
obj.dic:Add(1, "123")
obj.dic:Add(2, "234")
obj.dic:Add(3, "345")

print(obj.dic[1])

--xlua是通过pairs去遍历的 字典
--toLua中不支持这种遍历方式
--toLua中要用迭代器来进行遍历
local iter = obj.dic:GetEnumerator()
while iter:MoveNext() do
    local v = iter.Current
    print(v.Key .. "_" .. v.Value)
end
--遍历键
local keyIter = obj.dic.Keys:GetEnumerator()
while keyIter:MoveNext() do
    print("键：".. keyIter.Current)
end
--遍历值
local valueIter = obj.dic.Values:GetEnumerator()
while valueIter:MoveNext() do
    print("值：".. valueIter.Current)
end

--如果要在Lua中创建字典对象
--和List一样 需要在CustomSetting中去添加对应的类型
print("创建Dic")
local dic2 = System.Collections.Generic.Dictionary_int_string()
dic2:Add(10, "123")
print(dic2[10])

local dic3 = System.Collections.Generic.Dictionary_string_int()
dic3:Add("123", 88888)
--toLua使用Dic这 不能够直接通过字符串作为键来访问值
--print(dic3["123"])
local b,v = dic3:TryGetValue("123", nil)
print(v)
