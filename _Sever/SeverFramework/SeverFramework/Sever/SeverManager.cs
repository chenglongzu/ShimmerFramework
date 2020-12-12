using SeverFramework.Commonity;
using SeverFramework.FrameWork;
using SocketDLL;
using SocketDLL.Message;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SeverFramework.Sever
{

    /// <summary>
    /// 服务器管理器脚本
    /// </summary>
    public class ServerManager : BaseManager<ServerManager>
    {
        private UserManager userManager;

        private int maxCount = 100;              //最大连接数.
        private Socket socket;                   //主Socket对象.

        public event NormalDelegate StartSocketEvent;   //Socket开启事件.
        public event NormalDelegate CloseSocketEvent;   //Socket关闭事件.

        public event ServerMessageDelegate ServerMessageEvent;      //消息处理事件.

        private int clientIndex=1000;
        

        /// <summary>
        /// 开启服务器.
        /// </summary>
        public void StartServer(string ip, int port)
        {
            userManager = UserManager.GetInstance();

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress address = IPAddress.Parse(ip);
            IPEndPoint point = new IPEndPoint(address, port);
            socket.Bind(point);
            socket.Listen(maxCount);

            socket.BeginAccept(new AsyncCallback(HandlerAccept), socket);

            Message("服务器端已启动.");
        }


        /// <summary>
        /// 关闭服务器.
        /// </summary>
        public void CloseServer()
        {
            socket.Close();
            CloseSocketEvent();
            Message("服务器端已关闭");
        }


        /// <summary>
        /// 处理用户上线.
        /// </summary>
        private void HandlerAccept(IAsyncResult ar)
        {
            Socket tempSocket = (Socket)ar.AsyncState;      //服务器端主Socket.
            Socket clientSocket = socket.EndAccept(ar);     //客户端的子Socket.

            Message(clientSocket.RemoteEndPoint.ToString() + "用户上线.");

            ClientState clientState = new ClientState(clientSocket);

            clientIndex++;
            UserData userdate = new UserData(clientIndex);

            clientState.UserData = userdate;
            userManager.Add(clientState);

            clientState.ByteBuffer = new byte[socket.ReceiveBufferSize];

            //异步接收用户传入数据
            clientSocket.BeginReceive(clientState.ByteBuffer, 0, clientState.ByteBuffer.Length, 0, new AsyncCallback(HandlerReceive), clientState);

            //继续监听下一个用户的上线请求.
            socket.BeginAccept(new AsyncCallback(HandlerAccept), socket);
        }

        /// <summary>
        /// 接收到客户端发送过来的消息之后的回调方法.
        /// </summary>
        private void HandlerReceive(IAsyncResult ar)
        {
            try
            {
                ClientState clientState = (ClientState)ar.AsyncState;
                Socket clientSocket = clientState.ClientSocket;

                int count = clientSocket.EndReceive(ar);
                if (count == 0)
                {
                    Message(clientSocket.RemoteEndPoint.ToString() + "客户端已下线.");
                    //移除对象.
                    userManager.Remove(clientState);
                    return;
                }

                SocketMessage message = (SocketMessage)SocketTools.Deserialize(clientState.ByteBuffer, count);
                ServerMessageEvent(clientState, message);

                //重置字节数组.
                clientState.ByteBuffer = new byte[socket.ReceiveBufferSize];

                if (clientState != null)
                {
                    try
                    {
                        //接收下一条数据.
                        clientSocket.BeginReceive(clientState.ByteBuffer, 0, clientState.ByteBuffer.Length, 0, new AsyncCallback(HandlerReceive), clientState);

                    }
                    catch (Exception)
                    {

                        Message("客户端状态类已销毁");
                    }

                }

            }
            catch (Exception e)
            {
                Message("\n 我发生错误啦"+e);
                
            }
        }



        /// <summary>
        /// 消息发送方法.
        /// </summary>
        public void Send(Socket clientSocket, string text)
        {
            //将要发送的数据转码为UTF8格式的字节数组.
            byte[] message = Encoding.UTF8.GetBytes(text);
            //发送数据.
            clientSocket.BeginSend(message, 0, message.Length, 0, new AsyncCallback(HandlerSend), clientSocket);
        }

        /// <summary>
        /// 消息发送方法.
        /// </summary>
        public void Send(Socket clientSocket, byte[] bytes)
        {
            //发送数据.
            clientSocket.BeginSend(bytes, 0, bytes.Length, 0, new AsyncCallback(HandlerSend), clientSocket);
        }


        /// <summary>
        /// 消息发送成功之后的回调方法.
        /// </summary>
        private void HandlerSend(IAsyncResult ar)
        {
            Socket clientSocket = (Socket)ar.AsyncState;
            //发送的数据量.
            int count = clientSocket.EndSend(ar);
            Message("Succeed Send Message And Count:" + count);
        }


        /// <summary>
        /// 消息广播.
        /// </summary>
        public void SendAll()
        {
            for (int i = 0; i < userManager.ClientStateList.Count; i++)
            {
                Send(userManager.ClientStateList[i].ClientSocket, "服务器端发送过来的测试消息.");
            }
        }


        /// <summary>
        /// 消息调试.
        /// </summary>
        public void Message(string str)
        {
            Console.WriteLine("MESSAGE:" + str);
        }

    }
}
