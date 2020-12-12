using SocketDLL.Message;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace SeverFramework.Sever
{
    /// <summary>
    /// 客户端的抽象状态类
    /// </summary>
    public class ClientState
    {
        public byte[] ByteBuffer { get; set; }

        public Socket ClientSocket { get; set; }

        public UserData UserData { get; set; }


        public ClientState(Socket clientSocket)
        {
            this.ClientSocket = clientSocket;
        }

    }
}
