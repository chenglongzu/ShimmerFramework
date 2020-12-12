using System.Collections.Generic;
using ShimmerFramework;

namespace ShimmerNote
{
    /// <summary>
    /// 存储主城角色的数据结构
    /// </summary>
    public class ClientCityPlayerManager : BaseManager<ClientCityPlayerManager>
    {
        public Dictionary<int, ClientCityPlayer> cityPlayerDic = new Dictionary<int, ClientCityPlayer>();

        private int currentID = 0;          //当前角色ID值;
        public int CurrentID
        {
            get { return currentID; }
            set { currentID = value; }
        }

        /// <summary>
        /// 添加数据.
        /// </summary>
        public void Add(int id, ClientCityPlayer cityPlayer)
        {
            if (!cityPlayerDic.ContainsKey(id))
            {
                cityPlayerDic.Add(id, cityPlayer);
            }
        }

        /// <summary>
        /// 移除数据.
        /// </summary>
        public void Remove(int id)
        {
            cityPlayerDic.Remove(id);
        }

        /// <summary>
        /// 通过ID获取CityPlayer对象.
        /// </summary>
        /// <param name="id"></param>
        public ClientCityPlayer GetCityPlayerByID(int id)
        {
            ClientCityPlayer temp;
            if (cityPlayerDic.TryGetValue(id, out temp))
            {
                return temp;
            }
            else
            {
                return null;
            }
        }
    }

}