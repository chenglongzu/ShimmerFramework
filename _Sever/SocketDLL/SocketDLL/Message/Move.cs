using System;

namespace SocketDLL.Message
{
    [Serializable]
    public class Move
    {
        public int ID;
        public float X;
        public float Y;
        public float Z;

        public Move(int id, float x, float y, float z)
        {
            this.ID = id;
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
    }
}
