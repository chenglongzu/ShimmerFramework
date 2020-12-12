using System.Collections.Generic;
using UnityEngine;
using SocketDLL;
using SocketDLL.Message;
using ShimmerFramework;

namespace ShimmerNote
{
    public class ClientHandlerGameArena : BaseManager<ClientHandlerGameArena>
    {
        private float currtenTime;
        private float pastTime;

        private int attackID = 0;

        private Move move = null;
        private HitInfo hitInfo = null;
        private InputInfo inputInfo = null;

        private List<UserData> userDataList = null;


        public void Init()
        {
            ClientManager.GetInstance().ClientMessageEvent += HandlerArenaMessage;
            MonoManager.GetInstance().AddUpdateAction(Update);
        }

        private void Update()
        {
            currtenTime += Time.deltaTime;
        }

        /// <summary>
        /// 竞技场消息处理.
        /// </summary>
        private void HandlerArenaMessage(SocketMessage message)
        {

            switch (message.Head)
            {
                case MessageHead.SC_NewPlayerEnterArena:
                    userDataList = (List<UserData>)message.Body;
                    HandlerThread.GetInstance().AddDelegate(CreateArenaPlayers);
                    break;
                case MessageHead.SC_ArenaPlayerMove:

                    Debug.Log(currtenTime - pastTime);
                    pastTime = currtenTime;

                    move = (Move)message.Body;
                    HandlerThread.GetInstance().AddDelegate(PlayerMove);
                    break;
                case MessageHead.SC_ArenaPlayerAttack:
                    attackID = (int)message.Body;
                    HandlerThread.GetInstance().AddDelegate(PlayerAttack);
                    break;
                case MessageHead.SC_Hit:
                    hitInfo = (HitInfo)message.Body;
                    HandlerThread.GetInstance().AddDelegate(PlayerHit);
                    break;
                case MessageHead.SC_Input:
                    inputInfo = (InputInfo)message.Body;
                    Debug.Log(inputInfo);
                    HandlerThread.GetInstance().AddDelegate(PlayerInput);
                    break;
            }
        }



        /// <summary>
        /// 角色受伤害.
        /// </summary>
        private void PlayerHit()
        {
            ClientCityPlayer cityPlayer = ClientArenaPlayerManager.GetInstance().GetCityPlayerByID(hitInfo.ID);
            cityPlayer.Player.GetComponent<CharacterController>().Hit();

        }

        /// <summary>
        /// 竞技场角色移动.
        /// </summary>
        private void PlayerMove()
        {
            if (move.ID != ClientArenaPlayerManager.GetInstance().CurrentID)
            {
                ClientCityPlayer cityPlayer = ClientArenaPlayerManager.GetInstance().GetCityPlayerByID(move.ID);
                CharacterController playerController = cityPlayer.Player.GetComponent<CharacterController>();
                playerController.Move(move.X, move.Y, move.Z);
                //move = null;

            }
        }


        /// <summary>
        /// 竞技场角色攻击.
        /// </summary>
        private void PlayerAttack()
        {
            ClientCityPlayer cityPlayer = ClientArenaPlayerManager.GetInstance().GetCityPlayerByID(hitInfo.ID);
            CharacterController playerController = cityPlayer.Player.GetComponent<CharacterController>();
            playerController.Hit();
        }

        /// <summary>
        /// 竞技场角色输入
        /// </summary>
        private void PlayerInput()
        {
            Debug.Log(11111);
            ClientCityPlayer cityPlayer = ClientArenaPlayerManager.GetInstance().GetCityPlayerByID(inputInfo.ID);
            CharacterController playerController = cityPlayer.Player.GetComponent<CharacterController>();
            playerController.Input(inputInfo.keycode, inputInfo.keyCodeState);
            //inputInfo = null;
        }

        /// <summary>
        /// 创建竞技场角色.
        /// </summary>
        private void CreateArenaPlayers()
        {
            for (int i = 0; i < userDataList.Count; i++)
            {
                //实例化生成本地角色.
                if (userDataList[i].ID == ClientArenaPlayerManager.GetInstance().CurrentID)
                {
                    CreatePlayer(userDataList[i]);
                    continue;
                }

                Vector3 pos = new Vector3(
                        userDataList[i].PositionInfo.Pos_X,
                        userDataList[i].PositionInfo.Pos_Y,
                        userDataList[i].PositionInfo.Pos_Z
                    );
                Vector3 rot = new Vector3(
                        userDataList[i].PositionInfo.Rot_X,
                        userDataList[i].PositionInfo.Rot_Y,
                        userDataList[i].PositionInfo.Rot_Z
                    );
                Color color = new Color(
                        userDataList[i].ModelInfo.R,
                        userDataList[i].ModelInfo.G,
                        userDataList[i].ModelInfo.B
                    );
#if Addressable
                //实例化生成其他角色.
                ResourcesManager.GetInstance().LoadAssetAsync<GameObject>("Socket/" + userDataList[i].ModelInfo.ModelName, pos, Quaternion.Euler(rot),(obj)=> {
                    GameObject player = obj;
                    ClientCityPlayer cityPlayer = new ClientCityPlayer(userDataList[i], player);

                    ClientArenaPlayerManager.GetInstance().Add(userDataList[i].ID, cityPlayer);
                });

#else
                //实例化生成其他角色.
                GameObject player = ResourcesManager.GetInstance().LoadAsset<GameObject>("Socket/" + userDataList[i].ModelInfo.ModelName, pos, Quaternion.Euler(rot));

                ClientCityPlayer cityPlayer = new ClientCityPlayer(userDataList[i], player);

                ClientArenaPlayerManager.GetInstance().Add(userDataList[i].ID, cityPlayer);
#endif

            }
            userDataList.Clear();
            userDataList = null;
        }

        /// <summary>
        /// 角色实例化.
        /// </summary>
        private void CreatePlayer(UserData userData)
        {
            ClientCityPlayer cityPlayer = ClientArenaPlayerManager.GetInstance().GetCityPlayerByID(ClientArenaPlayerManager.GetInstance().CurrentID);

            Vector3 pos = new Vector3(
                    userData.PositionInfo.Pos_X,
                    userData.PositionInfo.Pos_Y,
                    userData.PositionInfo.Pos_Z
                );
            Vector3 rot = new Vector3(
                    cityPlayer.UserData.PositionInfo.Rot_X,
                    cityPlayer.UserData.PositionInfo.Rot_Y,
                    cityPlayer.UserData.PositionInfo.Rot_Z
                );
            Color color = new Color(
                    cityPlayer.UserData.ModelInfo.R,
                    cityPlayer.UserData.ModelInfo.G,
                    cityPlayer.UserData.ModelInfo.B
                );

#if Addressable
            ResourcesManager.GetInstance().LoadAssetAsync<GameObject>("Socket/" + cityPlayer.UserData.ModelInfo.ModelName, pos, Quaternion.Euler(rot),(obj)=> {
                cityPlayer.Player = obj;
            });
#else
            GameObject player = ResourcesManager.GetInstance().LoadAsset<GameObject>("Socket/" + cityPlayer.UserData.ModelInfo.ModelName, pos, Quaternion.Euler(rot));

            cityPlayer.Player = player;
#endif
        }
    }
}