using System;

namespace SocketDLL
{

    [Serializable]
    public class SocketMessage
    {

        private MessageHead head;
        private Object body;

        public MessageHead Head
        {
            get { return head; }
            set { head = value; }
        }

        public Object Body
        {
            get { return body; }
            set { body = value; }
        }
    }

}
