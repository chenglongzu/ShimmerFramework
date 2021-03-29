using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace ShimmerFramework
{
	/// <summary>
	/// Http�������ݵĻص�ί��
	/// </summary>
	/// <param name="args"></param>
	public delegate void HttpSendDataCallBack(HttpCallBackArgs args);

	/// <summary>
	/// Http������
	/// </summary>
	public class HttpRoutine
	{
		#region ����

		/// <summary>
		/// Http����ص�
		/// </summary>
		private HttpSendDataCallBack m_CallBack;

		/// <summary>
		/// Http����ص�����
		/// </summary>
		private HttpCallBackArgs m_CallBackArgs;

		/// <summary>
		/// �Ƿ�æ
		/// </summary>
		public bool IsBusy { get; private set; }

		/// <summary>
		/// ��ǰ���Դ���
		/// </summary>
		private int m_CurrRetry = 0;

		private string m_Url;
		private string m_Json;

		/// <summary>
		/// ���͵�����
		/// </summary>
		private Dictionary<string, object> m_Dic;
		#endregion

		public HttpRoutine()
		{
			m_CallBackArgs = new HttpCallBackArgs();
			m_Dic = new Dictionary<string, object>();
		}

		#region SendData ����web����
		/// <summary>
		/// ����web����
		/// </summary>
		/// <param name="url"></param>
		/// <param name="callBack"></param>
		/// <param name="isPost"></param>
		/// <param name="isGetData">�Ƿ��ȡ�ֽ�����</param>
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

		#region GetUrl Get����
		/// <summary>
		/// Get����
		/// </summary>
		/// <param name="url"></param>
		private void GetUrl(string url)
		{
			UnityWebRequest data = UnityWebRequest.Get(url);
			MonoManager.GetInstance().StartCoroutine(Request(data));
		}
		#endregion

		#region PostUrl Post����
		/// <summary>
		/// Post����
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
			//		//web����
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

		#region Request ���������
		/// <summary>
		/// ���������
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		private IEnumerator Request(UnityWebRequest data)
		{
			yield return data.SendWebRequest();
			if (data.isNetworkError || data.isHttpError)
			{
				//������ ��������
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

			if (!string.IsNullOrWhiteSpace(m_CallBackArgs.Value)) Debug.Log(string.Format("WebAPI�ص�:{0}, ==>>{1}", m_Url, m_CallBackArgs));

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