using SeverFramework.FrameWork;
using SocketDLL;
using SocketDLL.Message;
using System;
using System.Net.Sockets;

namespace SeverFramework.Sever
{
    /// <summary>
    /// 登录监听消息控制器控制器
    /// </summary>
    public class SeverHandlerGameLogin : BaseManager<SeverHandlerGameLogin>
    {
        public void Init()
        {
            ServerManager.GetInstance().ServerMessageEvent += HandlerLoginMessage;
        }

        /// <summary>
        /// 处理登录消息.
        /// </summary>
        private void HandlerLoginMessage(ClientState clientState, SocketMessage message)
        {
            Socket clientSocket = clientState.ClientSocket;

            switch (message.Head)
            {
                case MessageHead.CS_Login:
                    Login login = (Login)message.Body;

                    UserData userData = UserManager.GetInstance().GetUserData(login.UserName);
                    if (userData != null)
                    {
                        HandlerLogin(clientSocket, userData);
                        clientState.UserData = userData;
                    }
                    else
                    {
                        HandlerLogin(clientSocket, null);
                    }
                    break;


            }
        }


        /// <summary>
        /// 处理账号登录.
        /// </summary>
        private void HandlerLogin(Socket clientSocket, UserData userData)
        {
            //消息体封装.
            SocketMessage message = new SocketMessage();
            message.Head = MessageHead.SC_Login;
            message.Body = userData;

            //序列化.
            byte[] bytes = SocketTools.Serialize(message);

            //发送.
            ServerManager.GetInstance().Send(clientSocket, bytes);
        }
    }
}
