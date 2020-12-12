using UnityEngine;

namespace ShimmerNote
{
    public class ClientInitManager : MonoBehaviour
    {
        void Start()
        {
            DontDestroyOnLoad(gameObject);

            //是否联机
#if NetWorking
            ClientHandleGameLogin.GetInstance().Init();
            ClientHandleGameCity.GetInstance().Init();
            ClientHandlerGameArena.GetInstance().Init();
#endif

        }
        private void OnApplicationQuit()
        {
            //是否联机
#if NetWorking
            ClientManager.GetInstance().CloseClient();
#endif

        }
    }
}