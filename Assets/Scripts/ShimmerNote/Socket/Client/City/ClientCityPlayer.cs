using UnityEngine;
using SocketDLL.Message;

namespace ShimmerNote
{
    /// <summary>
    /// 主城角色的抽象类
    /// </summary>
    public class ClientCityPlayer
    {
        public UserData UserData { get; set; }
        public GameObject Player { get; set; }
        public CharacterInfo Character_Info { get; set; }

        public ClientCityPlayer(UserData data)
        {
            this.UserData = data;
        }

        public ClientCityPlayer(UserData data, GameObject player)
        {
            this.UserData = data;
            this.Player = player;
        }

        public ClientCityPlayer(UserData data, CharacterInfo characterInfo)
        {
            this.UserData = data;
            this.Character_Info = characterInfo;
        }

        public ClientCityPlayer(UserData data, GameObject player, CharacterInfo characterInfo)
        {
            this.UserData = data;
            this.Player = player;
            this.Character_Info = characterInfo;
        }

    }
}