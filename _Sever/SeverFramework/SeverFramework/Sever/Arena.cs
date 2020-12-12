
namespace SeverFramework.Sever
{
    /// <summary>
    /// 竞技场抽象类
    /// </summary>
    class Arena
    {
        public ClientState PlayerA { get; set; }
        public ClientState PlayerB { get; set; }

        public Arena(ClientState playerA, ClientState playerB)
        {
            this.PlayerA = playerA;
            this.PlayerB = playerB;
        }

    }
}
