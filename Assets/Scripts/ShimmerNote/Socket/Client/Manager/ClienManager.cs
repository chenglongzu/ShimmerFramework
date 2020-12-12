using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;
using SocketDLL.Message;
using SocketDLL;
using ShimmerFramework;

namespace ShimmerNote
{
    /// <summary>
    /// 客户端管理器脚本
    /// </summary>
    public class ClientManager : BaseManager<ClientManager>
    {

        private Socket socket;
        private IPEndPoint point;

        private byte[] byteBuffer;               //字节缓冲区.
        private bool socketState = false;        //Socket状态.

        public event ClientMessageDelegate ClientMessageEvent;    //消息处理事件.


        [SerializeField]
        private UserData currentPlayerData;       //当前角色的数据.
        public UserData CurrentPlayerData
        {
            get { return currentPlayerData; }
            set { currentPlayerData = value; }
        }

        /// <summary>
        /// 开启客户端.
        /// </summary>
        public void StartClient(string ip, int port)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress address = IPAddress.Parse(ip);
            point = new IPEndPoint(address, port);

            socket.BeginConnect(point, new AsyncCallback(HandlerConnect), socket);

            byteBuffer = new byte[socket.ReceiveBufferSize];
            socket.BeginReceive(byteBuffer, 0, byteBuffer.Length, 0, new AsyncCallback(HandlerReceive), socket);
        }

        /// <summary>
        /// 处理客户端连接服务器端.
        /// </summary>
        private void HandlerConnect(IAsyncResult ar)
        {
            if (socket.Connected)
            {
                EventManager.GetInstance().ActionTrigger("SucceedConnectSever");
                Message("客户端连接服务器端成功.");
                socketState = true;
                Socket tempSocket = (Socket)ar.AsyncState;
                socket.EndConnect(ar);
            }
            else
            {
                Message("客户端连接服务器端失败.");
            }
        }

        /// <summary>
        /// 关闭客户端.
        /// </summary>
        public void CloseClient()
        {
            SocketMessage message = new SocketMessage();

            message.Head = MessageHead.CS_Exit;

            message.Body = ClientCityPlayerManager.GetInstance().CurrentID;

            byte[] bytes = SocketTools.Serialize(message);
            Send(bytes);

            if (socketState) socket.Close();
            socketState = false;

            Message("客户端已经下线.");
        }

        /// <summary>
        /// 消息发送方法.
        /// </summary>
        public void Send(byte[] bytes)
        {
            if (socketState == false) return;
            try
            {
                //发送数据.
                socket.BeginSend(bytes, 0, bytes.Length, 0, new AsyncCallback(HandlerSend), socket);
            }
            catch
            {
                Message("消息发送失败.");
            }
        }

        /// <summary>
        /// 消息发送成功之后的回调方法.
        /// </summary>
        private void HandlerSend(IAsyncResult ar)
        {
            //发送的数据量.
            int count = socket.EndSend(ar);
            Message("消息发送成功,长度为:" + count);
        }


        /// <summary>
        /// 接收到服务器端发送过来的消息之后的回调方法.
        /// </summary>
        private void HandlerReceive(IAsyncResult ar)
        {
            //接收到的数据长度.
            int count = socket.EndReceive(ar);
            if (count == 0)
            {
                Message("长度为0.");
                return;
            }
            SocketMessage message = (SocketMessage)SocketTools.Deserialize(byteBuffer, count);
            ClientMessageEvent(message);

            //重置字节数组.
            byteBuffer = new byte[socket.ReceiveBufferSize];
            //接收下一条数据.
            socket.BeginReceive(byteBuffer, 0, byteBuffer.Length, 0, new AsyncCallback(HandlerReceive), socket);
        }

        /// <summary>
        /// 重连服务器端.
        /// </summary>
        public void ResetConnect()
        {
            socket.BeginConnect(point, new AsyncCallback(HandlerConnect), socket);
        }

        /// <summary>
        /// 消息调试.
        /// </summary>
        private void Message(string str)
        {
            Debug.Log("MESSAGE:" + str);
        }

    }
}