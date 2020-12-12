using ShimmerFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShimmerNote
{
    public class TestMMOMemory : MonoBehaviour
    {
        void Start()
        {
            //初始化一个类的数据
            Item item = new Item() { id = 1, name = "测试" };

            byte[] arr = null;

            //将类数据读取成字节数组
            using (MMOMemoryStream mmoMemoryStream = new MMOMemoryStream())
            {
                mmoMemoryStream.WriteInt(item.id);
                mmoMemoryStream.WriteUTF8String(item.name);

                arr = mmoMemoryStream.ToArray();

                for (int i = 0; i < arr.Length; i++)
                {
                    Debug.Log(arr[i]);
                }
            }


            Item item_1 = new Item();

            //将字节数组再次转换为item数据实体类
            using (MMOMemoryStream mmoMemoryStream = new MMOMemoryStream(arr))
            {
                item_1.id = mmoMemoryStream.ReadInt();
                item_1.name = mmoMemoryStream.ReadUTF8String();
            }
        }
    }
}