using System;

namespace SocketDLL.Message
{
    [Serializable]
    public class HitInfo
    {
        public int ID;
        public int HP;
        public float Blood;

        public HitInfo(int id, int hp, float blood)
        {
            this.ID = id;
            this.HP = hp;
            this.Blood = blood;
        }

    }
}
