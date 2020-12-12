using UnityEngine;

namespace ShimmerHotUpdate
{
    public class LuaCallCSharp : MonoBehaviour
    {
        void Start()
        {
            LuaManager.GetInstance().DoLuaFile("main");
            //Debug
        }
    }
}