# FrameWork
ShimmerFramework面向unity引擎的游戏客户端轻量级框架

模块化框架，保持框架的“轻量级”，框架分为四个主要模块基本模块，热更模块，网络模块，本地sqlite数据库模块，在使用框架时可根据项目的具体用途来导入且使用框架的各个模块。

热更支持腾讯开源的XLua框架，在ShimmerFramework最初的时候同时支持XLua和ToLua，但是在2020.03.29的更新中删掉了ToLua的模块，主要发现框架越来越臃肿，违背了轻量级的原则。

基本模块主要包括声音管理器，数据管理器，事件管理器，有限状态机，输入管理器，场景以及资源管理器。包括一个轻量的UI框架，计时器，单例基类以及继承了Mono的管理器。

框架遵循设计模式的原则，尽量在保证代码耦合度低的情况下照顾框架运行的性能。

