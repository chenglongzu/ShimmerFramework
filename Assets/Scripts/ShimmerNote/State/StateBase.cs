namespace ShimmerNote
{
    /// <summary>
    /// 有限状态机的抽象父类
    /// </summary>
    public abstract class StateBase
    {
        public abstract void Enter();

        public abstract void UpDate();

        public abstract void Exit();

    }
}
