using UnityEngine;
using SocketDLL;

namespace ShimmerNote
{
    public class ClientArenaSceneManager : MonoBehaviour
    {
        private ClientManager clientSocket;

        private void Start()
        {
            clientSocket = ClientManager.GetInstance();

            SaveCurrentUserInfo();
            NewPlayerEnterArena();
        }

        /// <summary>
        /// 存储当前角色信息.
        /// </summary>
        private void SaveCurrentUserInfo()
        {
            ClientArenaPlayerManager.GetInstance().CurrentID = clientSocket.CurrentPlayerData.ID;
            ClientArenaPlayerManager.GetInstance().Add(clientSocket.CurrentPlayerData.ID, new ClientCityPlayer(clientSocket.CurrentPlayerData));
        }

        /// <summary>
        /// 进入竞技场.
        /// </summary>
        private void NewPlayerEnterArena()
        {
            SocketMessage message = new SocketMessage();
            message.Head = MessageHead.CS_NewPlayerEnterArena;
            message.Body = clientSocket.CurrentPlayerData.ID;

            byte[] bytes = SocketTools.Serialize(message);

            clientSocket.Send(bytes);
        }
    }

}