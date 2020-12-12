using System;
using System.Collections.Generic;

namespace SeverFramework.FrameWork
{

    public class EventManager : BaseManager<EventManager>
    {
        private Dictionary<string, Action> actionDic_1 = new Dictionary<string, Action>();
        private Dictionary<string, Action<object>> actionDic_2 = new Dictionary<string, Action<object>>();
        private Dictionary<string, Action<object, object>> actionDic_3 = new Dictionary<string, Action<object, object>>();

        private List<Action> eventInitList = new List<Action>() { };

        #region 初始化事件
        public void AddEventInit(Action action)
        {
            eventInitList.Add(action);
        }

        public void EventInitTrigger()
        {
            for (int i = 0; i < eventInitList.Count; i++)
            {
                eventInitList[i]();
            }
        }
        #endregion


        public void AddAction(string name, Action action)
        {
            if (actionDic_1.ContainsKey(name))
            {
                actionDic_1[name] += action;
            }
            else
            {
                actionDic_1.Add(name, action);
            }
        }
        public void AddAction(string name, Action<object> action)
        {
            if (actionDic_2.ContainsKey(name))
            {
                actionDic_2[name] += action;
            }
            else
            {
                actionDic_2.Add(name, action);
            }
        }
        public void AddAction(string name, Action<object, object> action)
        {
            if (actionDic_3.ContainsKey(name))
            {
                actionDic_3[name] += action;
            }
            else
            {
                actionDic_3.Add(name, action);
            }
        }

        /// <summary>
        /// 需优化
        /// </summary>
        /// <param name="name"></param>
        public void RemoveAction(string name)
        {
            if (actionDic_1.ContainsKey(name))
            {
                actionDic_1[name] = null;
            }

            if (actionDic_2.ContainsKey(name))
            {
                actionDic_2[name] = null;
            }

            if (actionDic_3.ContainsKey(name))
            {
                actionDic_3[name] = null;
            }
        }

        public void ActionTrigger(string name)
        {
            if (actionDic_1.ContainsKey(name))
            {
                actionDic_1[name]();
            }
        }
        public void ActionTrigger(string name, object info)
        {
            if (actionDic_2.ContainsKey(name))
            {
                actionDic_2[name](info);
            }
        }
        public void ActionTrigger(string name, object info_1, object info_2)
        {
            if (actionDic_3.ContainsKey(name))
            {
                actionDic_3[name](info_1, info_2);
            }
        }

        public void ClearAction()
        {
            actionDic_2.Clear();
            actionDic_1.Clear();

        }
    }
}
