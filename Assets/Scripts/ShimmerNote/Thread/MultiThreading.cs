using UnityEngine;
using System.Threading;

namespace ShimmerNote
{
    /// <summary>
    /// Thread实现多线程
    /// </summary>
    public class MultiThreading : MonoBehaviour
    {
        ManualResetEvent manualResetEvent = new ManualResetEvent(true);//传入构造方法的参数决定了线程是否开启，true为开启，false关闭
        void Start()
        {
            Thread thread = new Thread(() =>
            {
                //lambda表达式 写关于线程的方法
                while (true)
                {
                    manualResetEvent.WaitOne();//通过调用这个方法使线程可开关闭
                    Thread.Sleep(1000);//方法所在线程延迟1000ms，调用Thread类中的Sleep静态方法
                }
            });

            thread.IsBackground = false;//为线程状态赋值，是否为后台线程。
            thread.Start();//使用Thread对象的Start方法开启线程


            thread.Join();//等待线程结束后再执行以下方法

            Debug.Log("It's My Time");
        }

        private void ForceCloseThread(Thread thread)
        {
            try
            {
                thread.Abort();//强制结束线程(可能会发生错误)，所以一般使用ManualResetEvent类来结束线程
            }
            catch (System.Exception exception)
            {
                Debug.Log(exception);
            }

        }

        private void OpenManualThread()
        {
            manualResetEvent.Set();//继续执行线程
        }

        private void CloseManualThread()
        {
            manualResetEvent.Reset();//结束线程
        }
    }
}