using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System;
using System.Text;

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

public class ClientSocket
{

    CircleBuffer m_receiveCB;
    CircleBuffer m_sendCB;
    AutoResetEvent _receiveEvent;
    AutoResetEvent _sendEvent;
    Socket m_clientSocket;
    Socket m_serverSocket;
    Thread m_receiveThr;
    Thread m_sendThr;
    ReaderWriterLock _receiveLock;

    private static ClientSocket _instance;
    public static ClientSocket Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ClientSocket();
            }
            return _instance;
        }
    }

    private ClientSocket()
    {

    }

    public void start()
    {
        m_clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        m_clientSocket.Connect(new IPEndPoint(IPAddress.Parse("192.168.0.103"), 8830));
        GlobalLog.v("connect success");
        SocketReadStateObject so = new SocketReadStateObject(m_clientSocket);
        m_receiveThr = new Thread(receiveThreading);
        m_receiveThr.Start(so);
        m_receiveCB = new CircleBuffer();
        _receiveEvent = new AutoResetEvent(false);

        //m_clientSocket.Send(BitConverter.GetBytes("fuck you now"));
        
        //m_sendThr = new Thread(sendThreading);
        //_sendEvent = new AutoResetEvent(false);
    }

    public void stop()
    {
        if (m_clientSocket != null)
        {
            m_clientSocket.Close(0);
        }
        if (m_receiveThr != null)
        {
            m_receiveThr.Abort();
            m_receiveThr = null;
        }
    }

    private void receiveThreading(System.Object obj)
    {
        GlobalLog.v("enter receive threading");
        SocketReadStateObject so = (SocketReadStateObject)obj;
        Socket soc = so.WorkSocket;
        while (true)
        {
            try
            {
                soc.BeginReceive(so._buffer, 0, SocketReadStateObject.BUFFER_SIZE, 0, new AsyncCallback(callback), so);
                //soc.Receive(so._buffer);
                _receiveEvent.WaitOne();
                _receiveEvent.Reset();
            }
            catch (Exception e)
            {
                GlobalLog.v(e.ToString());
                soc.Shutdown(SocketShutdown.Both);
                soc.Close();
                soc = null;
                break;
            }
        }
    }

    


    private void callback(System.IAsyncResult ar)
    {
        SocketReadStateObject so = (SocketReadStateObject)ar.AsyncState;
        Socket soc = so.WorkSocket;
        int n = soc.EndReceive(ar);
        if (n > 0)
        {
             m_receiveCB.appendData(so._buffer, n);
            if (m_receiveCB.getLength() > 4)
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
                }
            }
        }
        soc.BeginReceive(so._buffer, 0, SocketReadStateObject.BUFFER_SIZE, 0, new AsyncCallback(callback), so);
        //
        //_receiveEvent.Set();
    }

    private void sendThreading(System.Object obj)
    {
        GlobalLog.v("enter send threading");
        SocketReadStateObject so = (SocketReadStateObject)obj;
        Socket soc = so.WorkSocket;
        while (true)
        {
            try
            {
                soc.BeginSend(so._buffer, 0, SocketReadStateObject.BUFFER_SIZE, 0, new AsyncCallback(sendCallback), so);
                _sendEvent.WaitOne();
                _sendEvent.Reset();
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
//            _errCode = ESessionError.ERR_SEND_END_EXCEPTION;
  //          _errMessage = ex.Message;
           // Close();
        }
    }
}
