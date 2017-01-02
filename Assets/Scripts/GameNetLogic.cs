using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ProtoBuf;
using qi;
using System.IO;

//逻辑层
public class GameNetLogic
{
    delegate void AckCallBack(byte[] data);
    /// <summary>
    /// 根据不同的msg id来解析数据，再分发给客户端的各个UI界面。
    /// </summary>
    private Dictionary<int, AckCallBack> m_mapCallBack;
    static object SingleLock;
    static GameNetLogic _instance;

    private void init()
    {
        //TODO: 这一段可以通过python脚本来生成。 
        m_mapCallBack = new Dictionary<int, AckCallBack>();
        m_mapCallBack.Add((int)PROTOCOL.__LoginAck, OnLoginAck);
        m_mapCallBack.Add((int)PROTOCOL.__GetRoomAck, OnGetRoomAck);
    }

    public static GameNetLogic Instance
    {
        get
        {
            //lock (SingleLock)
            //{
            if (_instance == null)
            {
                _instance = new GameNetLogic();
                _instance.init();
            }
            //}
            return _instance;
        }
    }

    public void OnReceiveData(int msgID, byte[] data)
    {
        if (m_mapCallBack.ContainsKey(msgID))
        {
            m_mapCallBack[msgID](data);
        }
        else
        {
            Debug.Log(string.Format("msgID:{0}'s callback has no implemented", msgID));
        }
    }

    void OnLoginAck(byte[] data)
    {
        using (MemoryStream ms = new MemoryStream(data))
        {
            LoginAck ack = Serializer.Deserialize<LoginAck>(ms);
            GameFacade.Instance.SendNotification(GameFacade.ON_LOGIN, ack);
        }
    }
    void OnGetRoomAck(byte[] data)
    {
        using (MemoryStream ms = new MemoryStream(data))
        {
            GetRoomAck ack = Serializer.Deserialize<GetRoomAck>(ms);
            GameFacade.Instance.SendNotification(GameFacade.ON_GET_ROOM);
        }
    }
}
