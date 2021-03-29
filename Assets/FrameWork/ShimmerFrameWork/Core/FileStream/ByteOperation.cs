using System.IO;
using UnityEngine;

namespace ShimmerFramework
{
    public class ByteOperation : BaseManager<ByteOperation>
    {
        /// <summary>
        /// 从文件中读取字节数组
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public byte[] ReadFileToByte(string path)
        {
            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);

            fileStream.Seek(0, SeekOrigin.Begin);

            byte[] buffer = new byte[fileStream.Length]; //创建文件长度的buffer  

            fileStream.Read(buffer, 0, (int)fileStream.Length);

            fileStream.Close();

            fileStream.Dispose();

            fileStream = null;

            return buffer;
        }

        /// <summary>
        /// 将字节数组写入到文件中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public T WriteFileToByte<T>(string path, byte[] buffer) where T : Object
        {
            using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                fileStream.Seek(0, SeekOrigin.Begin);

                if (File.Exists(path))
                {
                    fileStream.Write(buffer, 0, (int)buffer.Length);

                }
                else
                {
                    File.Create(path);
                    fileStream.Write(buffer, 0, (int)buffer.Length);
                }

                fileStream.Flush();


                T res = ResourcesManager.GetInstance().LoadAsset<T>(path.Substring(50, path.Length - 54));
                return res;

            }
        }
    }
}