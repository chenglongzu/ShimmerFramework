using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace ShimmerFramework
{
	/// <summary>
	/// Http发送数据的回调委托
	/// </summary>
	/// <param name="args"></param>
	public delegate void HttpSendDataCallBack(HttpCallBackArgs args);

	/// <summary>
	/// Http访问器
	/// </summary>
	public class HttpRoutine
	{
		#region 属性

		/// <summary>
		/// Http请求回调
		/// </summary>
		private HttpSendDataCallBack m_CallBack;

		/// <summary>
		/// Http请求回调数据
		/// </summary>
		private HttpCallBackArgs m_CallBackArgs;

		/// <summary>
		/// 是否繁忙
		/// </summary>
		public bool IsBusy { get; private set; }

		/// <summary>
		/// 当前重试次数
		/// </summary>
		private int m_CurrRetry = 0;

		private string m_Url;
		private string m_Json;

		/// <summary>
		/// 发送的数据
		/// </summary>
		private Dictionary<string, object> m_Dic;
		#endregion

		public HttpRoutine()
		{
			m_CallBackArgs = new HttpCallBackArgs();
			m_Dic = new Dictionary<string, object>();
		}

		#region SendData 发送web数据
		/// <summary>
		/// 发送web数据
		/// </summary>
		/// <param name="url"></param>
		/// <param name="callBack"></param>
		/// <param name="isPost"></param>
		/// <param name="isGetData">是否获取字节数据</param>
		/// <param name="dic"></param>
		internal void Get(string url, HttpSendDataCallBack callBack = null)
		{
			if (IsBusy) return;
			IsBusy = true;

			m_Url = url;
			m_CallBack = callBack;

			GetUrl(m_Url);
		}

		internal void Post(string url, string json = null, HttpSendDataCallBack callBack = null)
		{
			if (IsBusy) return;
			IsBusy = true;

			m_Url = url;
			m_CallBack = callBack;
			m_Json = json;

			PostUrl(m_Url);
		}
		#endregion

		#region GetUrl Get请求
		/// <summary>
		/// Get请求
		/// </summary>
		/// <param name="url"></param>
		private void GetUrl(string url)
		{
			UnityWebRequest data = UnityWebRequest.Get(url);
			MonoManager.GetInstance().StartCoroutine(Request(data));
		}
		#endregion

		#region PostUrl Post请求
		/// <summary>
		/// Post请求
		/// </summary>
		/// <param name="url"></param>
		/// <param name="json"></param>
		private void PostUrl(string url)
		{
			//if (!string.IsNullOrWhiteSpace(m_Json))
			//{
			//	if (false && m_CurrRetry == 0)
			//	{
			//		m_Dic["value"] = m_Json;
			//		//web加密
			//		m_Dic["deviceIdentifier"] = DeviceUtil.DeviceIdentifier;
			//		m_Dic["deviceModel"] = DeviceUtil.DeviceModel;
			//		long t = GameEntry.Data.SysDataManager.CurrServerTime;
			//		m_Dic["sign"] = EncryptUtil.Md5(string.Format("{0}:{1}", t, DeviceUtil.DeviceIdentifier));
			//		m_Dic["t"] = t;
			//	}
			//	}
			//WWWForm form = new WWWForm();
			//form.AddField("json", m_Dic.ToJson());
			//UnityWebRequest unityWeb = UnityWebRequest.Post(url, form);

			//	if (!string.IsNullOrWhiteSpace(string.Empty))
			//		unityWeb.SetRequestHeader("Content-Type", string.Empty);

			//MonoManager.GetInstance().StartCoroutine(Request(unityWeb));
		}
		#endregion

		#region Request 请求服务器
		/// <summary>
		/// 请求服务器
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		private IEnumerator Request(UnityWebRequest data)
		{
			yield return data.SendWebRequest();
			if (data.isNetworkError || data.isHttpError)
			{
				//报错了 进行重试
				if (m_CurrRetry > 0) yield return new WaitForSeconds(HttpManager.GetInstance().RetryInterval);
				m_CurrRetry++;
				if (m_CurrRetry <= HttpManager.GetInstance().Retry)
				{
					switch (data.method)
					{
						case UnityWebRequest.kHttpVerbGET:
							GetUrl(m_Url);
							break;
						case UnityWebRequest.kHttpVerbPOST:
							PostUrl(m_Url);
							break;
					}
					yield break;
				}

				IsBusy = false;
				m_CallBackArgs.HasError = true;
				m_CallBackArgs.Value = data.error;
				m_CallBack?.Invoke(m_CallBackArgs);
			}
			else
			{
				IsBusy = false;
				m_CallBackArgs.HasError = false;
				m_CallBackArgs.Value = data.downloadHandler.text;
				m_CallBackArgs.Data = data.downloadHandler.data;
				m_CallBack?.Invoke(m_CallBackArgs);
			}

			if (!string.IsNullOrWhiteSpace(m_CallBackArgs.Value)) Debug.Log(string.Format("WebAPI回调:{0}, ==>>{1}", m_Url, m_CallBackArgs));

			m_CurrRetry = 0;
			m_Url = null;
			if (m_Dic != null)
			{
				m_Dic.Clear();
			}
			m_CallBackArgs.Data = null;
			data.Dispose();
			data = null;

		}
		#endregion
	}
}