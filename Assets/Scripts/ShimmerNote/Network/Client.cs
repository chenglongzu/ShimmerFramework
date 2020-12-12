using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;
using System.Text;

namespace ShimmerNote
{
    public class Client : MonoBehaviour
    {
        //2.字段.
        [SerializeField]
        public string ip;      //IP地址.

        [SerializeField]
        public int port;                //端口号.
        private Socket socket;                   //主Socket对象.
        private IPEndPoint point;                //端口对象.

        private byte[] byteBuffer;               //字节缓冲区.
        private bool socketState = false;        //Socket状态.

        void Start()
        {
            StartClient();
        }

        void OnDestroy()
        {
            //CloseClient();
        }

        /// <summary>
        /// 开启客户端.
        /// </summary>
        private void StartClient()
        {
            //3.创建Socket对象.
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //4.创建IP对象.
            IPAddress address = IPAddress.Parse(ip);
            //5.创建端口对象.
            point = new IPEndPoint(address, port);

            //6.异步方法连接服务器端.
            socket.BeginConnect(point, new AsyncCallback(HandlerConnect), socket);

            //初始化字节数组.
            byteBuffer = new byte[socket.ReceiveBufferSize];
            //接收.
            socket.BeginReceive(byteBuffer, 0, byteBuffer.Length, 0, new AsyncCallback(HandlerReceive), socket);
        }


        /// <summary>
        /// 处理客户端连接服务器端.
        /// </summary>
        private void HandlerConnect(IAsyncResult ar)
        {
            if (socket.Connected)
            {
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
            //转码成字符串.
            string str = Encoding.UTF8.GetString(byteBuffer, 0, count);
            Message(str);

            //重置字节数组.
            byteBuffer = new byte[socket.ReceiveBufferSize];
            //接收下一条数据.
            socket.BeginReceive(byteBuffer, 0, byteBuffer.Length, 0, new AsyncCallback(HandlerReceive), socket);
        }

        /// <summary>
        /// 消息发送方法.
        /// </summary>
        public void Send(string text)
        {
            if (socketState == false) return;
            //将要发送的数据转码为UTF8格式的字节数组.
            byte[] message = Encoding.UTF8.GetBytes(text);
            try
            {
                //发送数据.
                socket.BeginSend(message, 0, message.Length, 0, new AsyncCallback(HandlerSend), socket);
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
        /// 重连服务器端.
        /// </summary>
        public void ResetConnect()
        {
            socket.BeginConnect(point, new AsyncCallback(HandlerConnect), socket);
        }

        /// <summary>
        /// 关闭客户端.
        /// </summary>
        private void CloseClient()
        {
            socketState = false;
            socket.Close();
            Message("客户端已经下线.");
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