using System.Net;
using System.Net.Sockets;
using ShimmerFramework;

namespace ShimmerNote
{
    //UDP服务器端管理器
    public class UDPSeverManager : BaseManager<UDPSeverManager>
    {
        UdpClient udpClient;
        IPEndPoint remote;

        private void Init()
        {
            //端口号服务器端支持1-65535
            udpClient = new UdpClient(3333);

        }

        //接受消息
        private void Receive()
        {
            while (udpClient != null)
            {

            }
        }
    }
}