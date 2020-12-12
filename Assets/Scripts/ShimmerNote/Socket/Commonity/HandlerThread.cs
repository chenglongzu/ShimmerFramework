using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShimmerFramework;

namespace ShimmerNote
{
    public class HandlerThread : SingletonMono<HandlerThread>
    {

        private List<NormalDelegate> DelegateList = null;

        protected override void Awake()
        {
            base.Awake();
        }

        void Start()
        {
            DelegateList = new List<NormalDelegate>();

            StartCoroutine("ChildThread");
        }


        private IEnumerator ChildThread()
        {
            while (true)
            {
                if (DelegateList.Count > 0)
                {
                    for (int i = 0; i < DelegateList.Count; i++)
                    {
                        DelegateList[i]();
                    }

                    DelegateList.Clear();
                }

                yield return new WaitForSeconds(0.1f);
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