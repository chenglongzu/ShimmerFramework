using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SocketDLL.Message;
using SocketDLL;
using ShimmerFramework;

namespace ShimmerNote
{
    /// <summary>
    /// 主城控制器
    /// </summary>
    public class ClientHandleGameCity : BaseManager<ClientHandleGameCity>
    {
        private List<UserData> userDataList = null;
        private UserData userData = null;
        private Move move = null;
        private int exitID = 0;     //推出物体临时编号
        private int[] ids = null;

        public void Init()
        {
            ClientManager.GetInstance().ClientMessageEvent += HandlerCityMessage;
        }

        /// <summary>
        /// 主城消息处理.
        /// </summary>
        private void HandlerCityMessage(SocketMessage message)
        {
            switch (message.Head)
            {
                case MessageHead.SC_CreateNewPlayer:

                    //玩家是第一个进入主城的角色
                    if (message.Body.ToString() == "OK")
                    {
                        HandlerThread.GetInstance().AddDelegate(CreatePlayer);
                    }
                    else
                    {
                        //玩家不是第一个进入主城的角色
                        userData = (UserData)message.Body;

                        HandlerThread.GetInstance().AddDelegate(CreateNewPlayer);
                    }
                    break;

                case MessageHead.SC_CreateAllPlayer:
                    userDataList = (List<UserData>)message.Body;
                    HandlerThread.GetInstance().AddDelegate(CreateAllPlayer);
                    break;

                case MessageHead.SC_PlayerMove:
                    move = (Move)message.Body;
                    HandlerThread.GetInstance().AddDelegate(PlayerMove);
                    break;

                case MessageHead.SC_Exit:
                    exitID = (int)message.Body;
                    HandlerThread.GetInstance().AddDelegate(PlayerExit);
                    break;

                case MessageHead.SC_ExitCity:
                    ids = (int[])message.Body;
                    HandlerThread.GetInstance().AddDelegate(PlayersExit);
                    break;

                case MessageHead.SC_EnterArena:
                    HandlerThread.GetInstance().AddDelegate(EnterArena);
                    break;
            }
        }

         /// <summary>
         /// 角色实例化.
         /// </summary>
         private void CreatePlayer()
         {
             ClientCityPlayer cityPlayer = ClientCityPlayerManager.GetInstance().GetCityPlayerByID(ClientCityPlayerManager.GetInstance().CurrentID);

             Vector3 pos = new Vector3(
                     cityPlayer.UserData.PositionInfo.Pos_X,
                     cityPlayer.UserData.PositionInfo.Pos_Y,
                     cityPlayer.UserData.PositionInfo.Pos_Z
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
            ResourcesManager.GetInstance().LoadAssetAsync<GameObject>("Socket/" + userData.ModelInfo.ModelName, pos, Quaternion.Euler(rot), (obj) => {
                ResourcesManager.GetInstance().LoadAssetAsync<GameObject>("Socket/" + cityPlayer.UserData.ModelInfo.ModelName, pos, Quaternion.Euler(rot),
                    (obj_1)=> {
                        cityPlayer.Player = obj_1;

                    }
                );
            });
#else

            GameObject player = ResourcesManager.GetInstance().LoadAsset<GameObject>("Socket/" + cityPlayer.UserData.ModelInfo.ModelName, pos, Quaternion.Euler(rot));

            cityPlayer.Player = player;
#endif
         }

        /// <summary>
        /// 实例化所有角色.
        /// </summary>
        private void CreateAllPlayer()
         {
             for (int i = 0; i < userDataList.Count; i++)
             {
                 //实例化生成本地角色.
                 if (userDataList[i].ID == ClientCityPlayerManager.GetInstance().CurrentID)
                 {
                     CreatePlayer();
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
                 ResourcesManager.GetInstance().LoadAssetAsync<GameObject>("Socket/" + userData.ModelInfo.ModelName, pos, Quaternion.Euler(rot), (obj) => {
                     GameObject player = obj;

                     //将CityPlayer存储到CityPlaymanagerDic数据结构中
                     ClientCityPlayer cityPlayer = new ClientCityPlayer(userData, player);

                     ClientCityPlayerManager.GetInstance().Add(userData.ID, cityPlayer);

                     userDataList.Clear();
                     userData = null;
                 });
#else

                 //实例化生成其他角色.
                 GameObject player = ResourcesManager.GetInstance().LoadAsset<GameObject>("Socket/" + userDataList[i].ModelInfo.ModelName, pos, Quaternion.Euler(rot));

                 ClientCityPlayer cityPlayer = new ClientCityPlayer(userDataList[i], player);
                 ClientCityPlayerManager.GetInstance().Add(userDataList[i].ID, cityPlayer);
                 userDataList.Clear();
                 userDataList = null;
#endif
             }
         }
             /// <summary>
             /// 实例化新登录角色.
             /// </summary>
         private void CreateNewPlayer()
         {
             Vector3 pos = new Vector3(
                     userData.PositionInfo.Pos_X,
                     userData.PositionInfo.Pos_Y,
                     userData.PositionInfo.Pos_Z
                 );
             Vector3 rot = new Vector3(
                     userData.PositionInfo.Rot_X,
                     userData.PositionInfo.Rot_Y,
                     userData.PositionInfo.Rot_Z
                 );
             Color color = new Color(
                     userData.ModelInfo.R,
                     userData.ModelInfo.G,
                     userData.ModelInfo.B
                 );
#if Addressable
             ResourcesManager.GetInstance().LoadAssetAsync<GameObject>("Socket/" + userData.ModelInfo.ModelName, pos, Quaternion.Euler(rot), (obj) => {
                 GameObject player = obj;

                 //将CityPlayer存储到CityPlaymanagerDic数据结构中
                 ClientCityPlayer cityPlayer = new ClientCityPlayer(userData, player);

                 ClientCityPlayerManager.GetInstance().Add(userData.ID, cityPlayer);

                 userData = null;

             });
#else
            GameObject player = ResourcesManager.GetInstance().LoadAsset<GameObject>("Socket/" + userData.ModelInfo.ModelName, pos, Quaternion.Euler(rot));

            //将CityPlayer存储到CityPlaymanagerDic数据结构中
            ClientCityPlayer cityPlayer = new ClientCityPlayer(userData, player);

            ClientCityPlayerManager.GetInstance().Add(userData.ID, cityPlayer);

            userData = null;
#endif
         }

        /// <summary>
        /// 角色模型移动.
        /// </summary>
        private void PlayerMove()
         {
              ClientCityPlayer cityPlayer = ClientCityPlayerManager.GetInstance().GetCityPlayerByID(move.ID);
              CharacterController playerController = cityPlayer.Player.GetComponent<CharacterController>();

              //通过CityPlayermanager取值调用角色身上的函数
              playerController.Move(move.X, move.Y, move.Z);
              move = null;
         }

          /// <summary>
          /// 角色退出. 将角色从CityPlayerManager中取出 且销毁主城中的角色
          /// </summary>
          private void PlayerExit()
          {
              ClientCityPlayer cityPlayer = ClientCityPlayerManager.GetInstance().GetCityPlayerByID(exitID);

              GameObject.Destroy(cityPlayer.Player);
              ClientCityPlayerManager.GetInstance().Remove(exitID);
              exitID = 0;
          }

          /// <summary>
          /// 多个角色退出.多个角色从主城中销毁 从数据结构中取出
          /// </summary>
          private void PlayersExit()
          {
              for (int i = 0; i < ids.Length; i++)
              {
                  exitID = ids[i];
                  PlayerExit();
              }
              ids = null;
          }

          /// <summary>
          /// 进入竞技场. 监听方法加载进入竞技场场景
          /// </summary>
          private void EnterArena()
          {
              SceneManager.LoadScene("GameArena");
          }
    }
        
} 
