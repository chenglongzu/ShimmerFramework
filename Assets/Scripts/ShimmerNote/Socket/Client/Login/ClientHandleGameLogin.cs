using UnityEngine;
using UnityEngine.SceneManagement;
using SocketDLL;
using SocketDLL.Message;
using ShimmerFramework;

namespace ShimmerNote
{
    public class ClientHandleGameLogin : BaseManager<ClientHandleGameLogin>
    {
        public void Init()
        {
            ClientManager.GetInstance().ClientMessageEvent += HandlerMessage;
        }

        /// <summary>
        /// 登录消息处理.
        /// </summary>
        private void HandlerMessage(SocketMessage message)
        {
            switch (message.Head)
            {
                case MessageHead.SC_Login:
                    Debug.Log(message.Body);
                    if (message.Body == null)
                    {
                        Debug.Log("登录失败了...不存在您的账号");
                        return;
                    }
                    ClientManager.GetInstance().CurrentPlayerData = (UserData)message.Body;

                    HandlerThread.GetInstance().AddDelegate(JumpScene);

                    Debug.Log("登录成功");
                    break;
            }
        }

        /// <summary>
        /// 场景跳转.
        /// </summary>
        private void JumpScene()
        {
            //ClientManager.GetInstance().CurrentPlayerData = tempUserData;
            SceneManager.LoadScene("GameCity");

        }
    }

}