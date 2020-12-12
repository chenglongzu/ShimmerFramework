--ToLua

--Lua中访问Unity中的类
--Lua中访问unity中的类，可以通过 命名空间+类名
--和XLua相比 区别是不需要加CS
GameObject=UnityEngine.GameObject
--使用其他Unity中的类时需要先在CustomSetting文件中的customTypeList中添加这个类
--调用unity中的类时，和XLua中一样，调用成员方法时使用：调用，调用静态方法时使用.来调用

--当自定义类时，使用时也要在CustomSetting中添加

--Lua中访问Unity中的枚举
--和访问Unity中的类一样，通过命名空间和枚举名访问，可通过别名来访问
local MyEnum = MyEnum.idle

--Lua中映射C#中的数组
