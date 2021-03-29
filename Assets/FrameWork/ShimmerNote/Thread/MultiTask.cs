using UnityEngine;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace ShimmerNote
{
    /// <summary>
    /// Task实现多线程
    /// </summary>
    public class MultiTask : MonoBehaviour
    {
        object monitorObj = new object();

        //用于取消和启用某个线程
        CancellationTokenSource ct = new CancellationTokenSource();

        void Start()
        {
            //使用Task类中的静态方法Run来运行线程方法,并有一个Task类型的返回值
            Task task = Task.Run(TaskOne);

            task.Wait();//等待线程运行结束

            Debug.Log("It's My Time");
        }

        //异步方法，实现一个延时等待的效果
        private async void TaskOne()
        {
            //await Task.Delay();方法实现一个延时等待的效果
            await Task.Delay(1000);


            //添加判定结果，当ct.IsCancellationRequested为True的时候则表示收到了取消任务的信号
            //当ct.IsCancellationRequested为True的时候则表示没收到，可以正常运行
            while (!ct.IsCancellationRequested)
            {

                //线程同步的第一种方法
                try
                {
                    Monitor.Enter(monitorObj);
                    //关于数据之间的运算写在中间，防止线性竞争，数据不牢靠，保证进行运算的数据不会被其他线程调用
                    Monitor.Exit(monitorObj);

                }
                catch (Exception expection)
                {
                    Monitor.Exit(monitorObj);
                    Debug.Log(expection);
                }

                //线程同步的第二种方法
                lock (monitorObj)
                {
                    //将数据的运算都写倒到lock方法内，保证数据在运算时不会被其他线程调用
                }
            }
        }

        private void StopTask()
        {
            ct.Cancel();
        }

        //或者可以通过占位符的方式来控制线程是否运行，当停止运行的时候则return。
    }
}