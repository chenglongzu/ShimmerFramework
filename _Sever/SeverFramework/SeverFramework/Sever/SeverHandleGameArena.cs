using SocketDLL;
using SocketDLL.Message;
using System.Collections.Generic;
using System.Net.Sockets;
using SeverFramework.FrameWork;
using System;
using System.Diagnostics;

namespace SeverFramework.Sever
{
    class SeverHandleGameArena:BaseManager<SeverHandleGameArena>
    {
        private UserManager userManager;

        /// <summary>
        /// 竞技场逻辑初始化
        /// </summary>
        public void Init()
        {
            ServerManager.GetInstance().ServerMessageEvent += HandlerArenaMessage;
            userManager = UserManager.GetInstance();
        }

        /// <summary>
        /// 处理竞技场消息.
        /// </summary>
        private void HandlerArenaMessage(ClientState clientState, SocketMessage message)
        {
            Socket clientSocket = clientState.ClientSocket;

            switch (message.Head)
            {
                case MessageHead.CS_EnterArena:
                    int id = (int)message.Body;
                    ClientState state = userManager.GetMKClientStateByID(id);
                    if (userManager.AddWaitPlayer(state))
                    {
                        //开启竞技场相关代码逻辑.
                        int[] ids = userManager.InitArena();

                        //非竞技场角色客户端.
                        for (int i = 0; i < userManager.CityPlayerList.Count; i++)
                        {
                            PlayersExit(userManager.CityPlayerList[i].ClientSocket, ids);
                        }

                        //竞技场角色客户端.
                        EnterArena(userManager.Arena.PlayerA.ClientSocket);
                        EnterArena(userManager.Arena.PlayerB.ClientSocket);
                    }
                    break;

                case MessageHead.CS_NewPlayerEnterArena:
                    CreateArenaPlayer(clientSocket, userManager.GetArenaList());
                    break;

                    //客户端玩家在竞技场中的一系列操作
                case MessageHead.CS_ArenaPlayerMove:
                    Move move = (Move)message.Body;

                    ArenaPlayerMove(userManager.Arena.PlayerA.ClientSocket, move);
                    ArenaPlayerMove(userManager.Arena.PlayerB.ClientSocket, move);
                    break;

                case MessageHead.CS_ArenaPlayerAttack:
                    int attackID = (int)message.Body;
                    ArenaPlayerAttack(userManager.Arena.PlayerA.ClientSocket, attackID);
                    ArenaPlayerAttack(userManager.Arena.PlayerB.ClientSocket, attackID);
                    break;

                case MessageHead.CS_Hit:
                    int hitID = (int)message.Body;
                    HitInfo info = userManager.CalcHit(hitID);
                    ArenaHit(userManager.Arena.PlayerA.ClientSocket, info);
                    ArenaHit(userManager.Arena.PlayerB.ClientSocket, info);
                    break;

                case MessageHead.CS_Input:
                    InputInfo inputInfo = (InputInfo)message.Body;

                    ArenaPlayerInput(userManager.Arena.PlayerA.ClientSocket, inputInfo);
                    ArenaPlayerInput(userManager.Arena.PlayerB.ClientSocket, inputInfo);

                    break;
            }
        }


        /// <summary>
        /// 竞技场伤害计算.
        /// </summary>
        private void ArenaHit(Socket clientSocket, HitInfo info)
        {
            SocketMessage message = new SocketMessage();
            message.Head = MessageHead.SC_Hit;
            message.Body = info;

            byte[] bytes = SocketTools.Serialize(message);

            ServerManager.GetInstance().Send(clientSocket, bytes);
        }

        /// <summary>
        /// 竞技场角色攻击.
        /// </summary>
        private void ArenaPlayerAttack(Socket clientSocket, int id)
        {
            SocketMessage message = new SocketMessage();
            message.Head = MessageHead.SC_ArenaPlayerAttack;
            message.Body = id;

            byte[] bytes = SocketTools.Serialize(message);

            ServerManager.GetInstance().Send(clientSocket, bytes);
        }


        /// <summary>
        /// 竞技场角色移动.
        /// </summary>
        private void ArenaPlayerMove(Socket clientSocket, Move move)
        {
            SocketMessage message = new SocketMessage();
            message.Head = MessageHead.SC_ArenaPlayerMove;
            message.Body = move;

            byte[] bytes = SocketTools.Serialize(message);

            ServerManager.GetInstance().Send(clientSocket, bytes);
        }

        /// <summary>
        /// 竞技场角色输入.
        /// </summary>
        private void ArenaPlayerInput(Socket clientSocket, InputInfo input)
        {
            SocketMessage message = new SocketMessage();
            message.Head = MessageHead.SC_Input;
            message.Body = input;

            byte[] bytes = SocketTools.Serialize(message);

            ServerManager.GetInstance().Send(clientSocket, bytes);
        }


        /// <summary>
        /// 多个角色进入主城消息.
        /// </summary>
        private void PlayersExit(Socket clientSocket, int[] ids)
        {
            SocketMessage message = new SocketMessage();
            message.Head = MessageHead.SC_ExitCity;
            message.Body = ids;

            byte[] bytes = SocketTools.Serialize(message);

            ServerManager.GetInstance().Send(clientSocket, bytes);
        }

        /// <summary>
        /// 角色进入竞技场消息.
        /// </summary>
        private void EnterArena(Socket clientSocket)
        {
            SocketMessage message = new SocketMessage();
            message.Head = MessageHead.SC_EnterArena;
            message.Body = null;

            byte[] bytes = SocketTools.Serialize(message);

            ServerManager.GetInstance().Send(clientSocket, bytes);
        }


        /// <summary>
        /// 服务器端返回竞技场角色数据集合.
        /// </summary>
        private void CreateArenaPlayer(Socket clientSocket, List<UserData> userDataList)
        {
            SocketMessage message = new SocketMessage();
            message.Head = MessageHead.SC_NewPlayerEnterArena;
            message.Body = userDataList;

            byte[] bytes = SocketTools.Serialize(message);

            ServerManager.GetInstance().Send(clientSocket, bytes);
        }
    }
}
