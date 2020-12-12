using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SocketDLL
{

    public class SocketTools
    {

        /// <summary>
        /// 对象序列化.
        /// </summary>
        public static byte[] Serialize(System.Object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();     //二进制序列化对象.
            MemoryStream ms = new MemoryStream();           //内存流对象.
            bf.Serialize(ms, obj);                          //序列化对象到内存中.
            //byte[] bytes = ms.GetBuffer();                  //在内存中获取byte[]数据.
            byte[] bytes = ms.ToArray();
            ms.Close();                                     //关闭内存流对象.
            return bytes;
        }

        /// <summary>
        /// 对象反序列化.
        /// </summary>
        public static System.Object Deserialize(byte[] bytes)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream(bytes);
            System.Object obj = bf.Deserialize(ms);
            ms.Close();
            return obj;
        }

        /// <summary>
        /// 对象反序列化.
        /// </summary>
        public static System.Object Deserialize(byte[] bytes, int cout)
        {
            BinaryFormatter bf = new BinaryFormatter();
            byte[] tempByte = new byte[cout];
            Array.Copy(bytes, tempByte, cout);
            MemoryStream ms = new MemoryStream(tempByte);
            System.Object obj = bf.Deserialize(ms);
            ms.Close();
            return obj;
        }
    }

}
