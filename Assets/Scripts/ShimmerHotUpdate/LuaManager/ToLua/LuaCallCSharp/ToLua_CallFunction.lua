print("**************toLua访问C#的ref和out方法******************")

--toLua和xLua中对ref和out函数使用基本类似
--通过多返回值的形式来处理的
--如果 是out和ref函数
--第一个返回值 是函数的默认返回值 
--之后的返回值 就是 ref和out对应的结果 从左到右 一一对应

local obj = Lesson5()

print(obj:RefFun(10, 0, 0, 1))
local a,b,c = obj:RefFun(10, 0, 0, 1)
print(a)
print(b)
print(c)
--toLua中out函数调用和xlua有区别
--xLua中可省略传入 out参数
--toLua不能省略 
print(obj:OutFun(20, nil, nil,30))
local a,b,c = obj:OutFun(20, nil, nil,30)
print(a)
print(b)
print(c)

--在toLua中要使用ref和out时  尽量就使用out
print(obj:RefOutFun(20, nil, 1)) -- 300 200 400
local a,b,c = obj:RefOutFun(20, nil, 1)
print(a)
print(b)
print(c)