using ShimmerFramework;
using System.Collections.Generic;
using UnityEngine;

namespace ShimmerNote
{
    public class ClientArenaPlayerManager : BaseManager<ClientArenaPlayerManager>
    {
        public int CurrentID { get; set; }

        private Dictionary<int, ClientCityPlayer> arenaPlayerDic = new Dictionary<int, ClientCityPlayer>();

        /// <summary>
        /// 添加数据.
        /// </summary>
        public void Add(int id, ClientCityPlayer cityPlayer)
        {
            if (!arenaPlayerDic.ContainsKey(id))
            {
                arenaPlayerDic.Add(id, cityPlayer);
            }
        }

        /// <summary>
        /// 移除数据.
        /// </summary>
        public void Remove(int id)
        {
            arenaPlayerDic.Remove(id);
        }

        /// <summary>
        /// 通过ID获取CityPlayer对象.
        /// </summary>
        /// <param name="id"></param>
        public ClientCityPlayer GetCityPlayerByID(int id)
        {
            ClientCityPlayer temp;
            if (arenaPlayerDic.TryGetValue(id, out temp))
            {
                return temp;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 通过游戏物体对象获取对应的ID值.
        /// </summary>
        public int GetIDByGameObject(GameObject go)
        {
            foreach (var item in arenaPlayerDic.Keys)
            {
                if (arenaPlayerDic[item].Player == go)
                {
                    return arenaPlayerDic[item].UserData.ID;
                }
            }
            return 0;
        }
    }
}