using System;

namespace SocketDLL.Message
{
    [Serializable]
    public class InputInfo
    {
        public int ID;

        public KeyCode keycode;
        public KeyCodeState keyCodeState;
    }
}

