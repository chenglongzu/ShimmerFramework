using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SeverFramework.Commonity
{
    public class HandlerThread
    {

        private List<NormalDelegate> DelegateList = null;

        void Start()
        {
            DelegateList = new List<NormalDelegate>();
            //StartCoroutine(ChildThread());

        }


        private IEnumerator ChildThread()
        {
            while (true)
            {
                if (DelegateList.Count > 0)
                {
                    foreach (NormalDelegate item in DelegateList)
                    {
                        item();
                    }
                    DelegateList.Clear();
                }

                //yield return new WaitForSeconds(0.1f);
                yield return 1;
            }
        }

        /// <summary>
        /// 将委托传递到主线程去执行
        /// </summary>
        /// <param name="del"></param>
        public void AddDelegate(NormalDelegate del)
        {
            DelegateList.Add(del);
        }
    }
}
