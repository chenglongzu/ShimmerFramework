using SocketDLL;
using SocketDLL.Message;
using UnityEngine;
using DG.Tweening;

namespace ShimmerNote
{
    public class CharacterController : MonoBehaviour
    {
        public void Move(float x, float y, float z)
        {
            //transform.position = new Vector3(x,y,z);
            transform.DOMove(new Vector3(x, y, z), 0.01f);
        }

        public void Attack()
        {

        }

        public void Hit()
        {

        }

        public void Input(SocketDLL.KeyCode keycode, KeyCodeState keyCodeState)
        {

        }

        public void CharacterMove(UnityEngine.KeyCode keycode)
        {
            switch (keycode)
            {
                case UnityEngine.KeyCode.A:
                    transform.Translate(Vector3.left.normalized * Time.deltaTime * 5);

                    SendMoveMessage(transform.position);
                    break;

                case UnityEngine.KeyCode.W:
                    transform.Translate(Vector3.forward.normalized * Time.deltaTime * 5);

                    SendMoveMessage(transform.position);
                    break;

                case UnityEngine.KeyCode.S:
                    transform.Translate(Vector3.back.normalized * Time.deltaTime * 5);

                    SendMoveMessage(transform.position);
                    break;

                case UnityEngine.KeyCode.D:
                    transform.Translate(Vector3.right.normalized * Time.deltaTime * 5);

                    SendMoveMessage(transform.position);
                    break;

                default:
                    break;

            }
        }

        /// <summary>
        /// 向服务器发送客户端位置消息
        /// </summary>
        public void SendMoveMessage(Vector3 pos)
        {
            SocketMessage message = new SocketMessage();
            message.Head = MessageHead.CS_ArenaPlayerMove;

            Move move = new Move(ClientArenaPlayerManager.GetInstance().CurrentID, pos.x, pos.y, pos.z);

            move.ID = ClientArenaPlayerManager.GetInstance().CurrentID;

            move.X = pos.x;
            move.Y = pos.y;
            move.Z = pos.z;

            message.Body = move;

            byte[] bytes = SocketTools.Serialize(message);
            ClientManager.GetInstance().Send(bytes);
        }
    }
}