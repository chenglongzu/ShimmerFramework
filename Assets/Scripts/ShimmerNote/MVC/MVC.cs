using System;
using System.Collections.Generic;
using ShimmerFramework;

namespace ShimmerNote
{
    /// <summary>
    /// MVC控制器，用于存储Model View 和 Controller
    /// 并提供方法由外部调用获取Model View 和 Controller
    /// </summary>
    public class MVC : BaseManager<MVC>
    {
        #region Model View和ComandMap的容器 (键 名称 值MV和Comand实例)

        public Dictionary<string, Model> Models = new Dictionary<string, Model>();

        public Dictionary<string, View> Views = new Dictionary<string, View>();

        public Dictionary<string, Type> ComandMap = new Dictionary<string, Type>();
        #endregion

        #region 注册MVC
        public void RegisterView(View view)
        {
            if (Views.ContainsKey(view.Name))
            {
                Views.Remove(view.Name);
            }

            view.RegisterAttentionEvent();//调用视图方法,注册视图关心事件,存放在关心事件列表中
            Views[view.Name] = view;
        }

        public void RegisterModel(Model model)
        {
            Models[model.Name] = model;
        }

        public void RegisterController(string eventName, Type controllerType)
        {
            ComandMap[eventName] = controllerType;
        }
        #endregion

        #region 获取Model和View
        public T GetModel<T>() where T : Model
        {
            foreach (var m in Models.Values)
            {
                if (m is T)
                {
                    return (T)m;
                }
            }
            return null;
        }

        public T GetView<T>() where T : View
        {
            foreach (var v in Views.Values)
            {
                if (v is T)
                {
                    return (T)v;
                }
            }
            return null;
        }
        #endregion

        //触发事件
        public void SendEvent(string eventName, object data = null)
        {
            //传入控制器名称 然后调用Controller中的Execute方法
            if (ComandMap.ContainsKey(eventName))
            {
                Type t = ComandMap[eventName];
                Controller c = Activator.CreateInstance(t) as Controller;
                //执行被t所重写的Execute方法,data是传入的数据(object类型)
                c.Execute(data);
            }

            //view处理
            //遍历所有视图,注意:一个视图允许有多个事件，而且一个事件可能会在不同的视图触发，而事件的内容不确定（事件可理解为触发消息）
            foreach (var v in Views.Values)
            {
                //视图v的关心事件列表中存在该事件
                if (v.AttentionList.Contains(eventName))
                {
                    //让视图v执行该事件eventName,附带参数data
                    //HandleEvent方法是通过switch case的形式处理不同的事件
                    v.HandleEvent(eventName, data);
                }
            }
        }
    }
}