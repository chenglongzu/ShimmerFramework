using System;

namespace SocketDLL.Message
{
    /// <summary>
    /// 玩家角色位置信息.
    /// </summary>
    [Serializable]
    public class PlayerPositionInfo
    {
        public float Pos_X;
        public float Pos_Y;
        public float Pos_Z;

        public float Rot_X;
        public float Rot_Y;
        public float Rot_Z;

        public PlayerPositionInfo(float posX, float posY, float posZ, float rotX, float rotY, float rotZ)
        {
            this.Pos_X = posX;
            this.Pos_Y = posY;
            this.Pos_Z = posZ;

            this.Rot_X = rotX;
            this.Rot_Y = rotY;
            this.Rot_Z = rotZ;
        }

    }
}
