using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShimmerFramework
{
	public class HttpManager : BaseManager<HttpManager>
	{
		/// <summary>
		/// ��ʽ�˺ŷ�����Url
		/// </summary>
		private string m_WebAccountUrl;

		/// <summary>
		/// �����˺ŷ�����Url
		/// </summary>
		private string m_TestWebAccountUrl;

		/// <summary>
		/// �Ƿ���Ի���
		/// </summary>
		private bool m_IsTest;

		/// <summary>
		/// ��ʵ�˺ŷ�����Url
		/// </summary>
		public string RealWebAccountUrl
		{
			get
			{
				return "http://" + RealIpAndPort + "/";
			}
		}
		public string RealIpAndPort
		{
			get
			{
				return m_IsTest ? m_TestWebAccountUrl : m_WebAccountUrl;
			}
		}


		/// <summary>
		/// ����ʧ�ܺ����Դ���
		/// </summary>
		public int Retry
		{
			get;
			private set;
		}

		/// <summary>
		/// ����ʧ�ܺ����Լ�����룩
		/// </summary>
		public int RetryInterval
		{
			get;
			private set;
		}

		public void Dispose()
		{

		}

		internal void Init(string webAccountUrl,string testWebAccountUrl,bool isTest)
		{
			m_WebAccountUrl = webAccountUrl;
			m_TestWebAccountUrl = testWebAccountUrl;
			m_IsTest = isTest;

			//Retry = GameEntry.ParamsSettings.GetGradeParamData(YFConstDefine.Http_Retry, GameEntry.CurrDeviceGrade);
			//RetryInterval = GameEntry.ParamsSettings.GetGradeParamData(YFConstDefine.Http_RetryInterval, GameEntry.CurrDeviceGrade);
		}

		#region Get
		public void Get(string url, HttpSendDataCallBack callBack)
		{
			HttpRoutine http = new HttpRoutine();
			http.Get(url, callBack);
		}
		#endregion

		#region Post
		public void PostArgs(string url, string json = null, HttpSendDataCallBack callBack = null)
		{
			HttpRoutine http = new HttpRoutine();
			http.Post(url, json, callBack);
		}
		public void Post(string url, string json = null, Action<string, byte[]> callBack = null)
		{
			PostArgs(url, json, (args) =>
			{
				if (!args.HasError) callBack?.Invoke(args.Value, args.Data);
			});
		}
		#endregion
	}
}