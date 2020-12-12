using SeverFramework.FrameWork;
using SocketDLL.Message;
using System;
using System.Collections.Generic;

namespace SeverFramework.Sever
{
    /// <summary>
    /// 服务器端模拟数据库脚本
    /// </summary>
    class UserManager : BaseManager<UserManager>
    {
        private ServerManager serverSocket;
        private List<UserData> userDataList = new List<UserData>();                 //模拟用户数据.

        //竞技场对象
        public Arena Arena { get; private set; } = null;

        //链接服务器的客户端集合
        public List<ClientState> ClientStateList { get; } = new List<ClientState>();

        //主城客户端集合
        public List<ClientState> CityPlayerList { get; } = new List<ClientState>();

        //竞技场客户端集合
        public List<ClientState> ArenaWaitList { get; } = new List<ClientState>();


        /// <summary>
        /// 模拟用户数据,初始化.
        /// </summary>
        public void InitUserData()
        {
            serverSocket = ServerManager.GetInstance();
            serverSocket.CloseSocketEvent += Clear;

            userDataList.Add(new UserData(1, "ulognn", "face1" ,10, 1500,"123456", new PlayerModelInfo("cube_1", 1, 0, 0), new PlayerPositionInfo(0, 0, 0, 0, 0, 0)));
            userDataList.Add(new UserData(2, "weiguang", "face2", 12, 1500, "123456", new PlayerModelInfo("cube_2", 0, 1, 0), new PlayerPositionInfo(3, 0, 0, 0, 0, 0)));
            userDataList.Add(new UserData(3, "longcheng", "face3", 14, 1500, "123456", new PlayerModelInfo("cube_3", 0, 0, 1), new PlayerPositionInfo(6, 0, 0, 0, 0, 0)));
            userDataList.Add(new UserData(4, "zulong", "face4", 16, 1500, "123456", new PlayerModelInfo("cube_4", 0, 0, 1), new PlayerPositionInfo(8, 0, 0, 0, 0, 0)));

        }

        /// <summary>
        /// 初始化竞技场房间对象.
        /// </summary>
        public int[] InitArena()
        {
            int[] tempIDs = new int[2];
            tempIDs[0] = ArenaWaitList[0].UserData.ID;
            tempIDs[1] = ArenaWaitList[1].UserData.ID;

            Arena = new Arena(ArenaWaitList[0], ArenaWaitList[1]);

            CityPlayerList.Remove(ArenaWaitList[0]);
            CityPlayerList.Remove(ArenaWaitList[1]);

            ArenaWaitList.Clear();
            return tempIDs;
        }

        /// <summary>
        /// 获取竞技场内角色对象集合.
        /// </summary>
        public List<UserData> GetArenaList()
        {
            List<UserData> userDataList = new List<UserData>();
            Arena.PlayerA.UserData.PositionInfo.Pos_X = 0;
            Arena.PlayerA.UserData.PositionInfo.Pos_Y = 0;
            Arena.PlayerA.UserData.PositionInfo.Pos_Z = 0;

            Arena.PlayerB.UserData.PositionInfo.Pos_X = 3;
            Arena.PlayerB.UserData.PositionInfo.Pos_Y = 0;
            Arena.PlayerB.UserData.PositionInfo.Pos_Z = 0;

            userDataList.Add(Arena.PlayerA.UserData);
            userDataList.Add(Arena.PlayerB.UserData);
            return userDataList;
        }

        /// <summary>
        /// 计算竞技场上角色伤害数据.
        /// </summary>
        public HitInfo CalcHit(int hitID)
        {
            HitInfo info = null;
            if (hitID == Arena.PlayerA.UserData.ID)
            {
                //对PlayerB进行伤害计算.
                int id = Arena.PlayerB.UserData.ID;
                Arena.PlayerB.UserData.HP = Arena.PlayerB.UserData.HP - 100;
                float blood = Arena.PlayerB.UserData.HP / 1500.0f;
                info = new HitInfo(id, Arena.PlayerB.UserData.HP, blood);
            }
            else if (hitID == Arena.PlayerB.UserData.ID)
            {
                //对PlayerA进行伤害计算.
                int id = Arena.PlayerA.UserData.ID;
                Arena.PlayerA.UserData.HP = Arena.PlayerA.UserData.HP - 100;
                float blood = Arena.PlayerA.UserData.HP / 1500.0f;
                info = new HitInfo(id, Arena.PlayerA.UserData.HP, blood);
            }
            return info;
        }


        /// <summary>
        /// 添加数据.
        /// </summary>
        public void Add(ClientState state)
        {
            if (!ClientStateList.Contains(state))
            {
                ClientStateList.Add(state);
            }
        }

        /// <summary>
        /// 移除数据.
        /// </summary>
        public void Remove(ClientState state)
        {
            if (ClientStateList.Contains(state))
            {
                ClientStateList.Remove(state);
            }
        }

        /// <summary>
        /// 添加角色数据.
        /// </summary>
        public void AddPlayer(ClientState state)
        {
            if (!CityPlayerList.Contains(state))
            {
                CityPlayerList.Add(state);
            }
            
        }

        /// <summary>
        /// 往等待列表中添加角色.
        /// </summary>
        public bool AddWaitPlayer(ClientState state)
        {
            if (!ArenaWaitList.Contains(state))
            {
                ArenaWaitList.Add(state);
            }

            if (ArenaWaitList.Count == 2)
            {
                Console.WriteLine("开启竞技场房间.");
                return true;
            }
            return false;
        }

        /// <summary>
        /// 移除角色数据.
        /// </summary>
        public void RemovePlayer(ClientState state)
        {
            if (CityPlayerList.Contains(state))
            {
                CityPlayerList.Remove(state);
            }
        }

        /// <summary>
        /// 通过ID值,在主城集合中移除对应的角色对象.
        /// </summary>
        /// <param name="id"></param>
        public void RemovePlayerByID(int id)
        {
            for (int i = 0; i < CityPlayerList.Count; i++)
            {
                if (CityPlayerList[i].UserData.ID == id)
                {
                    CityPlayerList[i].ClientSocket.Close();
                    CityPlayerList.Remove(CityPlayerList[i]);
                }
            }
        }

        /// <summary>
        /// 通过用户名,获取对应的用户数据对象.
        /// </summary>
        public UserData GetUserData(string userName)
        {
            for (int i = 0; i < userDataList.Count; i++)
            {
                if (userDataList[i].UserName == userName)
                {
                    return userDataList[i];
                }
            }
            return null;
        }

        /// <summary>
        /// 获取主城内所有角色的UserData.
        /// </summary>
        /// <returns></returns>
        public List<UserData> GetCityUserDataList()
        {
            List<UserData> userDataList = new List<UserData>();

            for (int i = 0; i < CityPlayerList.Count; i++)
            {
                userDataList.Add(CityPlayerList[i].UserData);
            }

            return userDataList;
        }

        /// <summary>
        /// 通过ID值,在主城角色集合中,获取对应角色的UserData.
        /// </summary>
        public UserData GetUserDataByID(int id)
        {
            for (int i = 0; i < CityPlayerList.Count; i++)
            {
                if (CityPlayerList[i].UserData.ID == id)
                {
                    return CityPlayerList[i].UserData;
                }
            }
            return null;
        }


        /// <summary>
        /// 通过ID值,在主城角色集合中,获取对应角色的MKClientState.
        /// </summary>
        public ClientState GetMKClientStateByID(int id)
        {
            for (int i = 0; i < CityPlayerList.Count; i++)
            {
                if (CityPlayerList[i].UserData.ID == id)
                {
                    return CityPlayerList[i];
                }
            }
            return null;
        }


        /// <summary>
        /// 清空数据.
        /// </summary>
        private void Clear()
        {
            //客户端状态对象集合.
            for (int i = 0; i < ClientStateList.Count; i++)
            {
                ClientStateList[i].ClientSocket.Close();
            }
            ClientStateList.Clear();

            //主城角色对象集合.
            for (int i = 0; i < CityPlayerList.Count; i++)
            {
                CityPlayerList[i].ClientSocket.Close();
            }
            CityPlayerList.Clear();

            userDataList.Clear();
        }

        /// <summary>
        /// 将指定角色--客户端状态对象集合-->主城角色对象集合. 如果不存在的话则返回false
        /// </summary>
        public bool Move(int id)
        {
            for (int i = 0; i < ClientStateList.Count; i++)
            {
                Console.WriteLine("数据库中的ID值为" + ClientStateList[i].UserData.ID);

                if (ClientStateList[i].UserData.ID == id)
                {
                    CityPlayerList.Add(ClientStateList[i]);
                    ClientStateList.Remove(ClientStateList[i]);
                    return true;
                }
            }
            return false;
        }

    }

}
