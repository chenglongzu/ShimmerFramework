using SeverFramework.FrameWork;
using SocketDLL;
using SocketDLL.Message;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace SeverFramework.Sever
{
    /// <summary>
    /// 主城消息监听控制器
    /// </summary>
    public class SeverHandlerGameCity : BaseManager<SeverHandlerGameCity>
    {
        private UserManager userManager;

        public void Init()
        {
            userManager = UserManager.GetInstance();
            ServerManager.GetInstance().ServerMessageEvent += HandlerEnterCityMessage;
        }

        /// <summary>
        /// 处理新角色进入主城消息.
        /// </summary>
        private void HandlerEnterCityMessage(ClientState clientState, SocketMessage message)
        {
            Socket clientSocket = clientState.ClientSocket;

            switch (message.Head)
            {
                case MessageHead.CS_PlayerEnterCity:

                    int id = (int)message.Body;

                    //当此玩家是准备进入主城时
                    if (userManager.Move(id))
                    {
                        //<1>A玩家是第一个进入主城的角色.
                        if (userManager.CityPlayerList.Count == 1)
                        {
                            HandlerEnterCity(clientSocket, "OK");
                        }

                        if (userManager.CityPlayerList.Count > 1)
                        {
                            //<2>A玩家是第N个进入主城的角色. 实例化所有当前在主城中的玩家角色
                            HandlerCreateAll(clientSocket, userManager.GetCityUserDataList());

                            //<3>通知其他客户端A玩家进入主城. 通知其他客户端实例化当前的玩家角色
                            UserData currentUserData = userManager.GetUserDataByID(id);

                            for (int i = 0; i < userManager.CityPlayerList.Count; i++)
                            {
                                if (userManager.CityPlayerList[i].UserData.ID != id)
                                {
                                    HandlerCreateOne(userManager.CityPlayerList[i].ClientSocket, currentUserData);
                                }
                            }
                        }
                    }
                    else
                    {
                        HandlerEnterCity(clientSocket, "服务器端逻辑错误");
                    }
                    break;

                case MessageHead.CS_PlayerMove:

                    Move move = (Move)message.Body;
                    UserData userData = UserManager.GetInstance().GetUserDataByID(move.ID);
                    userData.PositionInfo.Pos_X = move.X;
                    userData.PositionInfo.Pos_Y = move.Y;
                    userData.PositionInfo.Pos_Z = move.Z;
                    for (int i = 0; i < userManager.CityPlayerList.Count; i++)
                    {
                        PlayerMove(userManager.CityPlayerList[i].ClientSocket, move);
                    }
                    break;

                case MessageHead.CS_Exit:

                    int exitID = (int)message.Body;
                    userManager.RemovePlayerByID(exitID);
                    for (int i = 0; i < userManager.CityPlayerList.Count; i++)
                    {
                        PlayerExit(userManager.CityPlayerList[i].ClientSocket, exitID);
                    }
                    break;
            }
        }

        /// <summary>
        /// 通知客户端 处理角色进入主城
        /// </summary>
        private void HandlerEnterCity(Socket clientSocket, string str)
        {
            SocketMessage message = new SocketMessage();
            message.Head = MessageHead.SC_CreateNewPlayer;
            message.Body = str;

            byte[] bytes = SocketTools.Serialize(message);

            ServerManager.GetInstance().Send(clientSocket, bytes);
        }



        /// <summary>
        /// 通知客户端 创建主城内所有玩家角色.
        /// </summary>
        private void HandlerCreateAll(Socket clientSocket, List<UserData> userDataList)
        {
            SocketMessage message = new SocketMessage();
            message.Head = MessageHead.SC_CreateAllPlayer;
            message.Body = userDataList;

            byte[] bytes = SocketTools.Serialize(message);

            ServerManager.GetInstance().Send(clientSocket, bytes);
        }


        /// <summary>
        /// 通知客户端 实例化新角色.
        /// </summary>
        private void HandlerCreateOne(Socket clientSocket, UserData userData)
        {

            SocketMessage message = new SocketMessage();
            message.Head = MessageHead.SC_CreateNewPlayer;
            message.Body = userData;

            byte[] bytes = SocketTools.Serialize(message);

            ServerManager.GetInstance().Send(clientSocket, bytes);
        }

        /// <summary>
        /// 通知客户端 某一角色模型移动.
        /// </summary>
        private void PlayerMove(Socket clientSocket, Move move)
        {
            SocketMessage message = new SocketMessage();
            message.Head = MessageHead.SC_PlayerMove;
            message.Body = move;

            byte[] bytes = SocketTools.Serialize(message);

            ServerManager.GetInstance().Send(clientSocket, bytes);
        }

        /// <summary>
        /// 通知客户端角色退出消息.
        /// </summary>
        private void PlayerExit(Socket clientSocket, int exitID)
        {
            SocketMessage message = new SocketMessage();
            message.Head = MessageHead.SC_Exit;
            message.Body = exitID;

            byte[] bytes = SocketTools.Serialize(message);

            ServerManager.GetInstance().Send(clientSocket, bytes);
        }

    }
}
