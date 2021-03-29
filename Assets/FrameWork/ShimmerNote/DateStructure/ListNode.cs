
namespace ShimmerNote
{
    /// <summary>
    /// 链表节点类
    /// </summary>
    public class ListNode   // 结点类
    {
        /// <summary>
        /// 构造函数 初始化链表节点的值
        /// </summary>
        /// <param name="NewValue"></param>
        public ListNode(int NewValue)
        {
            Value = NewValue;
        }
        public ListNode Previous;      //前一个
        public ListNode Next;      //后一个
        public int Value;              //值
    }

}

