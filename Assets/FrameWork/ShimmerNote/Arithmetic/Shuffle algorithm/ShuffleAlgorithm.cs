using UnityEngine;

namespace ShimmerNote
{

    /// <summary>
    /// 洗牌算法
    /// </summary>
    public class ShuffleAlgorithm : MonoBehaviour
    {
        /// <summary>
        /// 传递进来数组参数 随机打乱
        /// 外部调用时使用queue来接收数组 达到随机值的效果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataArray"></param>
        /// <returns></returns>
        public static T[] ShuffleCoords<T>(T[] dataArray) where T : Object
        {
            for (int i = 0; i < dataArray.Length; i++)
            {
                int randomIndex = Random.Range(i, dataArray.Length);

                //SWAP思想 数据互换
                T tempDate = dataArray[randomIndex];
                dataArray[randomIndex] = dataArray[i];
                dataArray[i] = tempDate;
            }

            return dataArray;
        }
    }
}