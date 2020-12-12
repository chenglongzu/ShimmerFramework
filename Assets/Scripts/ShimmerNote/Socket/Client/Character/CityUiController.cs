using UnityEngine.UI;
using SocketDLL;
using ShimmerFramework;

namespace ShimmerNote
{
    public class CityUiController : BasePanel
    {
        public override void Start()
        {
            base.Start();

            GetUiController<Button>("MatchingButton").onClick.AddListener(IntoArena);
        }

        /// <summary>
        /// 跳转到竞技场
        /// </summary>
        private void IntoArena()
        {
            SocketMessage message = new SocketMessage();
            message.Head = MessageHead.CS_EnterArena;
            message.Body = ClientManager.GetInstance().CurrentPlayerData.ID;

            byte[] bytes = SocketTools.Serialize(message);

            ClientManager.GetInstance().Send(bytes);
        }
    }
}