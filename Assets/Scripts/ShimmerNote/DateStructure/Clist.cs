using UnityEngine;


namespace ShimmerNote
{
    /// <summary>
    /// 链表类
    /// </summary>
    public class Clist : MonoBehaviour
    {
        private ListNode Head;      //头指针
        private ListNode Tail;      //尾指针

        private ListNode Current;   //当前指针

        private int ListCountValue; //链表数据的个数


        //构造函数
        public Clist()
        {
            ListCountValue = 0;//初始化

            Head = null;
            Tail = null;
        }

        /// <summary>
        /// 尾部添加数据
        /// </summary>
        /// <param name="DataValue"></param>
        public void Append(int DataValue)
        {
            ListNode NewNode = new ListNode(DataValue);

            if (IsNull())//如果头指针为空
            {
                Head = NewNode;
                Tail = NewNode;
            }
            else
            {
                Tail.Next = NewNode;
                NewNode.Previous = Tail;
                Tail = NewNode;
            }
            Current = NewNode;
            ListCountValue += 1;//链表数据个数加一
        }


        /// <summary>
        /// 删除当前数据
        /// </summary>
        public void Delete()
        {
            if (!IsNull())//若为空链表
            {
                if (IsBof())//若删除头
                {
                    Head = Current.Next;
                    Current = Head;
                    ListCountValue -= 1;
                    return;
                }
                if (IsEof())//若删除尾
                {
                    Tail = Current.Previous;
                    Current = Tail;
                    ListCountValue -= 1;
                    return;
                }
                Current.Previous.Next = Current.Next;//若删除中间数据
                Current = Current.Previous;
                ListCountValue -= 1;
                return;
            }
        }


        /// <summary>
        /// 当前位置插入代码
        /// </summary>
        /// <param name="DataValue"></param>
        public void Insert(int DataValue)
        {
            ListNode NewNode = new ListNode(DataValue);
            if (IsNull())
            {
                Append(DataValue);//如果为空表，则添加
                return;
            }
            if (IsBof())
            {
                //为头部插入
                NewNode.Next = Head;
                Head.Previous = NewNode;
                Head = NewNode;
                Current = Head;
                ListCountValue += 1;
                return;
            }
            //中间插入
            NewNode.Next = Current;
            NewNode.Previous = Current.Previous;
            Current.Previous.Next = NewNode;
            Current.Previous = NewNode;
            Current = NewNode;
            ListCountValue += 1;
        }


        /// <summary>
        /// 升序插入
        /// </summary>
        /// <param name="InsertValue"></param>
        public void InsertAscending(int InsertValue)
        {
            //参数：InsertValue 插入的数据
            if (IsNull())//为空链表
            {
                Append(InsertValue);//添加
                return;
            }
            MoveFrist();//移动到头
            if ((InsertValue < GetCurrentValue()))
            {
                Insert(InsertValue);//满足条件，则插入，退出
                return;
            }
            while (true)
            {
                if (InsertValue < GetCurrentValue())
                {
                    Insert(InsertValue);//满足条件，则插入，退出
                    break;
                }
                if (IsEof())
                {
                    Append(InsertValue);//尾部添加
                    break;
                }
                MoveNext();//移动到下一个指针
            }
        }


        /// <summary>
        /// 降序插入
        /// </summary>
        /// <param name="InsertValue"></param>
        public void InsertUnAscending(int InsertValue)
        {
            //参数：InsertValue 插入的数据
            if (IsNull())//为空链表
            {
                Append(InsertValue);//添加
                return;
            }
            MoveFrist();//移动到头
            if (InsertValue > GetCurrentValue())
            {
                Insert(InsertValue);//满足条件，则插入，退出
                return;
            }
            while (true)
            {
                if (InsertValue > GetCurrentValue())
                {
                    Insert(InsertValue);//满足条件，则插入，退出
                    break;
                }
                if (IsEof())
                {
                    Append(InsertValue);//尾部添加
                    break;
                }
                MoveNext();//移动到下一个指针
            }
        }

        /// <summary>
        /// 向后移动一个数据
        /// </summary>
        public void MoveNext()
        {
            if (!IsEof()) Current = Current.Next;
        }

        /// <summary>
        /// 向前移动一个数据
        /// </summary>
        public void MovePrevious()
        {
            if (!IsBof()) Current = Current.Previous;
        }

        /// <summary>
        /// 移动到第一个数据
        /// </summary>
        public void MoveFrist()
        {
            Current = Head;
        }

        /// <summary>
        /// 移动到最后一个数据
        /// </summary>
        public void MoveLast()
        {
            Current = Tail;
        }

        /// <summary>
        /// 判断当前列表是否为空列表
        /// </summary>
        /// <returns></returns>
        public bool IsNull()
        {
            if (ListCountValue == 0)
                return true;
            return false;
        }

        /// <summary>
        /// 判断是否达到尾部
        /// </summary>
        /// <returns></returns>
        public bool IsEof()
        {
            if (Current == Tail)
                return true;
            return false;
        }

        /// <summary>
        /// 判断是否达到头部
        /// </summary>
        /// <returns></returns>
        public bool IsBof()
        {
            if (Current == Head)
                return true;
            return false;
        }

        /// <summary>
        /// 获取节点值
        /// </summary>
        /// <returns></returns>
        public int GetCurrentValue()
        {
            return Current.Value;
        }

        /// <summary>
        /// 获取链表个数
        /// </summary>
        /// <value></value>
        public int ListCount
        {
            get
            {
                return ListCountValue;
            }
        }

        //清空链表
        public void Clear()
        {
            MoveFrist();
            while (!IsNull())
            {
                Delete();//若不为空链表,从尾部删除
            }
        }
    }
}
