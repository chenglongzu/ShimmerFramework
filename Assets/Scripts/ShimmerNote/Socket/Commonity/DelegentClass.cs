using SocketDLL;

namespace ShimmerNote
{
    //默认委托.
    public delegate void NormalDelegate();

    //客户端消息处理委托
    public delegate void ClientMessageDelegate(SocketMessage message);
}
