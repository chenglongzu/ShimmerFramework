using System;

namespace SocketDLL.Message
{
    /// <summary>
    /// 玩家角色模型信息.
    /// </summary>
    [Serializable]
    public class PlayerModelInfo
    {
        /// <summary>
        /// 模型名称
        /// </summary>
        public string ModelName;
        /// <summary>
        /// R
        /// </summary>
        public float R;
        /// <summary>
        /// G
        /// </summary>
        public float G;
        /// <summary>
        /// B
        /// </summary>
        public float B;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="modelName"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        public PlayerModelInfo(string modelName, float r, float g, float b)
        {
            this.ModelName = modelName;
            this.R = r;
            this.G = g;
            this.B = b;
        }
    }
}
