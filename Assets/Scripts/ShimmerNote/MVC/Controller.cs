namespace ShimmerNote
{
    /// <summary>
    /// Controller控制器层
    /// </summary>
    public abstract class Controller
    {
        //执行事件
        public abstract void Execute(object data);

        #region 由外部调用获取Model和View
        #endregion

        #region 调用MVC控制器 注册Model View和Controller
        #endregion
    }
}