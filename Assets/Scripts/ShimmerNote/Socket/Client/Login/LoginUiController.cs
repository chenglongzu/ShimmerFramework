using UnityEngine.UI;
using SocketDLL.Message;
using SocketDLL;
using ShimmerFramework;

namespace ShimmerNote
{
    public class LoginUiController : BasePanel
    {
        public string ip;
        public int port;

        private bool isConnected;
        public override void Start()
        {
            base.Start();

            //开启服务器
            ClientManager.GetInstance().StartClient(ip, port);

            //点击登录按钮 发送服务器登录消息
            GetUiController<Button>("SignIn").onClick.AddListener(() =>
            {
                if (isConnected)
                {
                    SocketMessage socketMessage = new SocketMessage();
                    socketMessage.Head = MessageHead.CS_Login;
                    Login login = new Login();

                    login.UserName = GetUiController<Text>("SignInInputFieldText").text;
                    login.LoginInfo = null;
                    socketMessage.Body = login;

                    byte[] message = SocketTools.Serialize(socketMessage);
                    ClientManager.GetInstance().Send(message);
                }

            });

            //成功连接服务器修改标志位
            EventManager.GetInstance().AddAction("SucceedConnectSever", () =>
            {
                isConnected = true;
            });
        }
    }
}