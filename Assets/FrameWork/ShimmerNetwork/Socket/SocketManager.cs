using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ShimmerFramework
{
	/// <summary>
	/// Socket������
	/// </summary>
	public class SocketManager : BaseManager<SocketManager>, IDisposable
	{
		/// <summary>
		/// SocketTcp����������
		/// </summary>
		private LinkedList<SocketTcpRoutine> m_SocketTcpRoutineList;

		[Header("ÿ֡���������")]
		public int MaxSendCount = 5;

		[Header("ÿ�η�������ֽ�����")]
		public int MaxSendByteCount = 1024;

		[Header("ÿ֡����������")]
		public int MaxReceiveCount = 5;

		[Header("������� ��")]
		public int HeartbeatInterval = 10;

		/// <summary>
		/// �ϴ�����ʱ��
		/// </summary>
		private float m_PrevHeartbeatInterval = 0;

		/// <summary>
		/// PINGֵ(����)
		/// </summary>
		[HideInInspector]
		public int PingValue;

		/// <summary>
		/// ��Ϸ��������ʱ��
		/// </summary>
		[HideInInspector]
		public long GameServerTime;

		/// <summary>
		/// �ͷ������Ա��ʱ��
		/// </summary>
		[HideInInspector]
		public float CheckServerTime;

		/// <summary>
		/// ��ȡ��ǰ��Socket������ʱ��
		/// </summary>
		/// <returns></returns>
		public long GetCurrServerTime()
		{
			return (int)((Time.realtimeSinceStartup - CheckServerTime) * 1000) + GameServerTime;
		}

		/// <summary>
		/// �Ƿ������ӵ��˷�����
		/// </summary>
		private bool m_IsConnectToMainSocket = false;


		/// <summary>
		/// �������ݵ�MemoryStream
		/// </summary>
		public ShimmerMemoryStream SocketSendMS { get; private set; }
		/// <summary>
		/// �������ݵ�MemoryStream
		/// </summary>
		public ShimmerMemoryStream SocketReceiveMS { get; private set; }


		internal void Init()
		{
			m_SocketTcpRoutineList = new LinkedList<SocketTcpRoutine>();
			SocketSendMS = new ShimmerMemoryStream();
			SocketReceiveMS = new ShimmerMemoryStream();

			m_MainSocket = CreateSocketTcpRoutine();

			m_MainSocket.OnConnectOK = () =>
			{
				//�Ѿ�����������
				m_IsConnectToMainSocket = true;

				//�ɷ��������ӵ��¼�
				EventManager.GetInstance().GlobalEventTrigger(EventIdDefine.OnConnectOKToMainSocket);
			};
		}

		/// <summary>
		/// ����SocketTcp������
		/// </summary>
		/// <returns></returns>
		public SocketTcpRoutine CreateSocketTcpRoutine()
		{
			//�Ӷ������ȡ��������
			return new SocketTcpRoutine();
		}

		/// <summary>
		/// ע��SocketTcp������
		/// </summary>
		/// <param name="routine"></param>
		internal void RegisterSocketTcpRoutine(SocketTcpRoutine routine)
		{
			m_SocketTcpRoutineList.AddFirst(routine);
		}

		/// <summary>
		/// �Ƴ�SocketTcp������
		/// </summary>
		/// <param name="routine"></param>
		internal void RemoveSocketTcpRoutine(SocketTcpRoutine routine)
		{
			m_SocketTcpRoutineList.Remove(routine);
		}

		internal void OnUpdate()
		{
			for (LinkedListNode<SocketTcpRoutine> curr = m_SocketTcpRoutineList.First; curr != null; curr = curr.Next)
			{
				curr.Value.OnUpdate();
			}

			if (m_IsConnectToMainSocket)
			{
				if (Time.realtimeSinceStartup > m_PrevHeartbeatInterval + HeartbeatInterval)
				{
					//ѭ����ʱ
					m_PrevHeartbeatInterval = Time.realtimeSinceStartup;

					//��������
					//System_HeartbeatProto proto = new System_HeartbeatProto();
					//proto.LocalTime = Time.realtimeSinceStartup * 1000;
					//CheckServerTime = Time.realtimeSinceStartup;
					//SendMainMsg(proto.ToArray());
				}
			}
		}

		public void Dispose()
		{
			m_SocketTcpRoutineList.Clear();

			m_IsConnectToMainSocket = false;

			m_MainSocket.DisConnect();

			SocketSendMS.Dispose();
			SocketReceiveMS.Dispose();

			SocketSendMS.Close();
			SocketReceiveMS.Close();
		}

		//=====================================
		/// <summary>
		/// ��Socket
		/// </summary>
		private SocketTcpRoutine m_MainSocket;

		/// <summary>
		/// ��Socket���ӷ�����
		/// </summary>
		/// <param name="ip"></param>
		/// <param name="port"></param>
		public void ConnectToMainSocket(string ip, int port)
		{
			m_MainSocket.Connect(ip, port);
		}

		/// <summary>
		/// ��Socket������Ϣ
		/// </summary>
		/// <param name="buffer"></param>
		public void SendMainMsg(IProto proto)
		{
			Debug.Log(string.Format("������Ϣ=={0}{1}", proto.ProtoEnName, proto/*.ToJson()*/));
			m_MainSocket.SendMsg(proto);
		}

		/// <summary>
		/// Lua�з�����Ϣ ��Lua�е���
		/// </summary>
		/// <param name="protoId">��Ϣ���</param>
		/// <param name="category">����</param>
		/// <param name="buffer">��Ϣ��</param>
		public void SendMainMsgForLua(ushort protoId, byte category, byte[] buffer)
		{
			m_MainSocket.SendMsg(protoId, category, buffer);
		}
	}
}