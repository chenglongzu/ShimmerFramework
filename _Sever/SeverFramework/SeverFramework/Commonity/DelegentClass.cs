using SeverFramework.Sever;
using SocketDLL;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeverFramework.Commonity
{
    public delegate void NormalDelegate();

    //服务器消息处理委托.
    public delegate void ServerMessageDelegate(ClientState clientState, SocketMessage message);
    //客户端消息处理委托
    public delegate void ClientMessageDelegate(SocketMessage message);
}
