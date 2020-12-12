using UnityEngine;
using SocketDLL;

namespace ShimmerNote
{
    public class ClientCitySceneManager : MonoBehaviour
    {
        private ClientManager clientSocket;
        private void Awake()
        {
            clientSocket = ClientManager.GetInstance();

            SaveCurrentUserInfo();

            NewPlayerEnterCity();
        }
        /// <summary>
        /// 存储当前角色信息.
        /// </summary>
        private void SaveCurrentUserInfo()
        {
            ClientCityPlayerManager.GetInstance().CurrentID = clientSocket.CurrentPlayerData.ID;
            ClientCityPlayerManager.GetInstance().Add(clientSocket.CurrentPlayerData.ID, new ClientCityPlayer(clientSocket.CurrentPlayerData));
        }

        /// <summary>
        /// 通知服务器有新角色进入主城
        /// </summary>
        private void NewPlayerEnterCity()
        {
            SocketMessage message = new SocketMessage();
            message.Head = MessageHead.CS_PlayerEnterCity;
            message.Body = clientSocket.CurrentPlayerData.ID;

            Debug.Log("我的ID是" + clientSocket.CurrentPlayerData.ID + "………………");

            byte[] bytes = SocketTools.Serialize(message);

            clientSocket.Send(bytes);
        }

    }
}