using UnityEngine;
using System.Net.Sockets;
using ShimmerFramework;

namespace ShimmerNote
{
    public class UDPClientManager : BaseManager<UDPClientManager>
    {
        private string ipPath = "127.0.0.0";
        UdpClient udpClient;

        private void Start()
        {
            //0表示由系统自动分配
            udpClient = new UdpClient(0);

            Receive();
        }

        //由外部调用进行信息的发送
        public async void Send(byte[] date)
        {
            if (udpClient != null)
            {
                try
                {
                    //三个参数 分别是 字节数组 数组长度 ip地址 端口号
                    int length = await udpClient.SendAsync(date, date.Length, ipPath, 3333);

                    if (date.Length == length)
                    {
                        //说明数据完整地发送了出去
                    }
                }
                catch (System.Exception error)
                {
                    //打印输出发生错误输出的错误信息
                    Debug.Log(error);

                    //未正确发送，做相应处理
                    throw;
                }
            }
            else
            {
                //发送失败，作相应处理

                //udpClient = null;
            }
        }

        private async void Receive()
        {
            while (udpClient != null)
            {
                try
                {
                    UdpReceiveResult result = await udpClient.ReceiveAsync();

                    //Encoding.UTF8.GetString(result.Buffer); 将接受到的字节数组转化为字符串
                }
                catch (System.Exception error)
                {
                    //打印输出错误信息
                    Debug.Log(error);

                    //接收失败作相应处理
                    //udpClient = null;
                    throw;
                }
            }
        }
    }
}