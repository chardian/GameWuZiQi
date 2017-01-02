using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ProtoBuf;
using qi;
using System.IO;
using System.Threading;

public class RecvData
{
    public int msgID;
    public byte[] data;
}
//逻辑层
public class GameNetLogic
{
    delegate void AckCallBack(byte[] data);
    /// <summary>
    /// 根据不同的msg id来解析数据，再分发给客户端的各个UI界面。
    /// </summary>
    private Dictionary<int, AckCallBack> m_mapCallBack;
    List<RecvData> m_recvData;
    static object SingleLock = new object();
    static GameNetLogic _instance;
    private ReaderWriterLock rwlock;

    private void init()
    {
        //TODO: 这一段可以通过python脚本来生成。 
        m_mapCallBack = new Dictionary<int, AckCallBack>();
        m_mapCallBack.Add((int)PROTOCOL.__LoginAck, OnLoginAck);
        m_mapCallBack.Add((int)PROTOCOL.__GetRoomAck, OnGetRoomAck);
        m_recvData = new List<RecvData>();
        rwlock = new ReaderWriterLock();
    }

    public static GameNetLogic Instance
    {
        get
        {
            lock (SingleLock)
            {
                if (_instance == null)
                {
                    _instance = new GameNetLogic();
                    _instance.init();
                }
            }
            return _instance;
        }
    }

    public void OnReceiveData(int msgID, byte[] data)
    {
        rwlock.AcquireWriterLock(-1);
        RecvData rd = new RecvData();
        rd.msgID = msgID;
        rd.data = data;
        m_recvData.Add(rd);
        rwlock.ReleaseReaderLock();
    }

    void OnLoginAck(byte[] data)
    {
        using (MemoryStream ms = new MemoryStream(data))
        {
            LoginAck ack = Serializer.Deserialize<LoginAck>(ms);
            PlayerInformation.Instance.m_userID = ack.id;
            GameFacade.Instance.SendNotification(GameFacade.ON_LOGIN, ack);
        }
    }
    void OnGetRoomAck(byte[] data)
    {
        using (MemoryStream ms = new MemoryStream(data))
        {
            GetRoomAck ack = Serializer.Deserialize<GetRoomAck>(ms);
            GameFacade.Instance.SendNotification(GameFacade.ON_GET_ROOM, ack);
        }
    }

    public void update()
    {
        if (m_recvData != null)
        {
            rwlock.AcquireWriterLock(-1);
            while (m_recvData.Count > 0)
            {
                RecvData rd = m_recvData[0];
                if (m_mapCallBack.ContainsKey(rd.msgID))
                {
                    m_mapCallBack[rd.msgID](rd.data);
                    m_recvData.RemoveAt(0);
                }
                else
                {
                    Debug.Log(string.Format("msgID:{0}'s callback has no implemented", rd.msgID));
                }
            }
            rwlock.ReleaseReaderLock();
        }
    }
}
