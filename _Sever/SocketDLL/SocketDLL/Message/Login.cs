using System;

namespace SocketDLL.Message
{
    [Serializable]
    public class Login
    {
        public string UserName;
        public string LoginInfo;

        public override string ToString()
        {
            return "UserName:" + UserName + " LoginInfo:" + LoginInfo;
        }
    }
}
