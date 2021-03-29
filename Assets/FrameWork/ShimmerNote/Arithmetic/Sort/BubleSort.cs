using UnityEngine;

namespace ShimmerNote
{
    /// <summary>
    /// 冒泡排序算法
    /// </summary>
    public class BubleSort : MonoBehaviour
    {

        /// <summary>
        /// 升序排列
        /// </summary>
        /// <param name="arr"></param>
        private void UpgradeValue(int[] arr)
        {
            //使用数组内置的排列的方法 数组升序排列
            //Array.Sort(arr);

            for (int i = 0; i < arr.Length - 1; i++)
            {
                for (int j = 0; j < arr.Length - 1 - i; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
                }
            }

            for (int i = 0; i < arr.Length; i++)
            {
                Debug.Log(arr[i]);
            }
        }

        /// <summary>
        /// 降序排列
        /// </summary>
        /// <param name="arr"></param>
        private void DropValue(int[] arr)
        {
            //使用数组内置的排列方法 首先数组升序排列 然后对数组进行反转
            //Array.Sort(arr);
            //Array.Reverse(arr);

            for (int i = 0; i < arr.Length - 1; i++)
            {
                for (int j = 0; j < arr.Length - 1 - i; j++)
                {
                    if (arr[j] < arr[j + 1])
                    {
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
                }
            }

            for (int i = 0; i < arr.Length; i++)
            {
                Debug.Log(arr[i]);
            }
        }
    }
}