using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System;
using System.Text;
using qi;
using System.IO;
using ProtoBuf;

//网络层的数据
public class SocketReadStateObject
{
    private Socket _workSocket;

    public byte[] _buffer;
    //64 * 1024
    public const int BUFFER_SIZE = 1024;

    public SocketReadStateObject(Socket _socket)
    {
        _buffer = new byte[BUFFER_SIZE];
        _workSocket = _socket;
    }

    public Socket WorkSocket
    {
        get { return _workSocket; }
    }
}

public class SocketSendStateObject
{
    private Socket _workSocket;

    public byte[] _buffer;
    public int _iLeft;

    public SocketSendStateObject(Socket _socket, byte[] buffer)
    {
        _buffer = buffer;
        _iLeft = _buffer.Length;
        _workSocket = _socket;
    }

    public Socket WorkSocket
    {
        get { return _workSocket; }
    }
}

public class GameSocket
{
    Queue _sendDataQueue;
    CircleBuffer m_receiveCB;
    CircleBuffer m_sendCB;
    AutoResetEvent _receiveEvent;
    AutoResetEvent _sendEvent;
    ManualResetEvent _stopEvent;
    Socket m_clientSocket;
    Socket m_serverSocket;
    ReaderWriterLock _receiveLock;
    Dictionary<int, Action<Byte[]>> CallBackMap;

    private static GameSocket _instance;
    public static GameSocket Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameSocket();
            }
            return _instance;
        }
    }

    private GameSocket()
    {

    }

    public void start()
    {
        initCallBackMap();
        _receiveEvent = new AutoResetEvent(false);
        _sendEvent = new AutoResetEvent(false);
        _stopEvent = new ManualResetEvent(false);
        m_clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        m_clientSocket.Connect(new IPEndPoint(IPAddress.Parse("192.168.0.103"), 8830));
        GlobalLog.v("connect success");
        m_receiveCB = new CircleBuffer();
        _sendDataQueue = new Queue();

        ThreadPool.QueueUserWorkItem(new WaitCallback(receiveThreading));
        ThreadPool.QueueUserWorkItem(new WaitCallback(sendThreading));
    }

    private void initCallBackMap()
    {
        CallBackMap = new Dictionary<int, Action<byte[]>>();

    }

    public void stop()
    {
        //关闭线程
        _stopEvent.Set();
        if (m_clientSocket != null)
        {
            Debug.Log("========== close socket now");
            m_clientSocket.Close(0);
        }
    }

    /// <summary>
    /// 发送数据
    /// </summary>
    /// <param name="protoid"></param>
    /// <param name="oo"></param>
    public void send(int protoid, object oo)
    {
        using (MemoryStream ret = new MemoryStream())
        {
            BinaryWriter bw = new BinaryWriter(ret);
            bw.Write(0);
            bw.Write(protoid);
            Serializer.Serialize(ret, oo);
            int len = (int)ret.Length - 8;
            ret.Position = 0;
            bw.Write(len);
            sendMessageToGame(ret.ToArray());
        }
    }

    private void sendMessageToGame(byte[] msg)
    {
        _sendDataQueue.Enqueue(msg);
        _sendEvent.Set();
    }

    private void receiveThreading(System.Object o)
    {
        GlobalLog.v("enter receive threading");
        while (true)
        {
            try
            {
                WaitHandle[] handles = new WaitHandle[1];
                handles[0] = _stopEvent;
                SocketReadStateObject so = new SocketReadStateObject(m_clientSocket);
                m_clientSocket.BeginReceive(so._buffer, 0, SocketReadStateObject.BUFFER_SIZE, 0, new AsyncCallback(receiveCallback), so);
                if (WaitHandle.WaitAny(handles) == 0)
                {
                    //_stopEvent
                    break;
                }
            }
            catch (Exception e)
            {
                GlobalLog.v(e.ToString());
                m_clientSocket.Shutdown(SocketShutdown.Both);
                m_clientSocket.Close();
                m_clientSocket = null;
                break;
            }
        }
    }

    private void receiveCallback(System.IAsyncResult ar)
    {
        SocketReadStateObject so = (SocketReadStateObject)ar.AsyncState;
        Socket soc = so.WorkSocket;
        int n = soc.EndReceive(ar);
        if (n > 8)
        {
            //足够读取一个INT类型的参数的时候。
            m_receiveCB.appendData(so._buffer, n);
            int msgSize = m_receiveCB.readInt();
            m_receiveCB.moveNext(sizeof(int));
            int msgID = m_receiveCB.readInt();
            m_receiveCB.moveNext(sizeof(int));
            if (msgSize > 0)
            {
                //读取下一个数据包的长度。
                byte[] data = m_receiveCB.getBytes(msgSize);
                GameNetLogic.Instance.OnReceiveData(msgID, data);

                /*MemoryStream mm = new MemoryStream(data);
                LoginAck ack = Serializer.Deserialize<LoginAck>(mm);
                Debug.Log("ack is" + ack.id + "," + ack.ret);*/
            }




            /*if (m_receiveCB.getLength() > 4)
            {
                //byte[] bytesize = null;
                //byte[] bytecontent = null;
                //bytesize = new byte[4];
                //m_receiveCB.ReadData(ref bytesize, 0);
                int contentsize = m_receiveCB.readInt();
                contentsize += 4;
                if (m_receiveCB.getLength() < contentsize)
                {
                    //没有接收完整
                    Console.WriteLine("没有接收完整");
                }
                else
                {
                    m_receiveCB.moveNext(contentsize);
                    //get data
                    int length = m_receiveCB.readInt();
                    if(length > 8)
                    {
                        m_receiveCB.moveNext(sizeof(int));
                        int proid = m_receiveCB.readInt();

                    }
                }
            }*/
        }
        soc.BeginReceive(so._buffer, 0, SocketReadStateObject.BUFFER_SIZE, 0, new AsyncCallback(receiveCallback), so);
    }

    private void sendThreading(System.Object o)
    {
        Queue workQueue = new Queue();
        GlobalLog.v("enter send threading");
        while (true)
        {
            try
            {
                workQueue.Clear();
                WaitHandle[] handles = new WaitHandle[2];
                handles[0] = _stopEvent;
                handles[1] = _sendEvent;
                if (WaitHandle.WaitAny(handles) == 0)
                {
                    //_stopEvent
                    break;
                }
                else
                {
                    //_sendLock.AcquireWriterLock(-1);
                    foreach (byte[] item in _sendDataQueue)
                    {
                        SocketSendStateObject so = new SocketSendStateObject(m_clientSocket, item);
                        workQueue.Enqueue(so);
                    }
                    _sendDataQueue.Clear();
                    //_sendLock.ReleaseWriterLock();
                    if (workQueue != null)
                    {
                        foreach (SocketSendStateObject item in workQueue)
                        {
                            m_clientSocket.BeginSend(item._buffer, 0, item._buffer.Length, 0, new AsyncCallback(sendCallback), item);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                GlobalLog.v(ex.ToString());
            }
        }
    }

    private void sendCallback(IAsyncResult ar)
    {
        SocketSendStateObject so = (SocketSendStateObject)ar.AsyncState;
        Socket s = so.WorkSocket;

        if (s == null || !s.Connected)
        {
            return;
        }

        try
        {
            int sz = s.EndSend(ar);
            if (sz < so._iLeft)
            {
                // Handle in-completed sent

                so._iLeft -= sz;

                s.BeginSend(so._buffer, so._buffer.Length - so._iLeft, so._iLeft, 0, new AsyncCallback(sendCallback), so);
            }
        }
        catch (Exception ex)
        {
            stop();
        }
    }
}
