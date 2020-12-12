using SeverFramework.Sever;
using System;
using System.Collections.Generic;
using System.Text;


namespace SeverFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            string ipPath;
            int port;

            Console.WriteLine("press ip path……");

            ipPath = Console.ReadLine();
            if (ipPath == "")
            {
                ipPath = "192.168.43.238";
            }

            Console.WriteLine("press ip port……");

            string tempPort = Console.ReadLine();
            if (tempPort == "")
            {
                port = 33333;
            }
            else
            {
                port = int.Parse(tempPort);
            }

            //启动服务器链接
            ServerManager.GetInstance().StartServer(ipPath, port);
            //服务器端数据库初始化
            UserManager.GetInstance().InitUserData();
            //初始化客户端登录监听
            SeverHandlerGameLogin.GetInstance().Init();
            //初始化客户端主城监听
            SeverHandlerGameCity.GetInstance().Init();
            //初始化客户端竞技场监听
            SeverHandleGameArena.GetInstance().Init();



            Console.ReadKey();

        }

    }
}
