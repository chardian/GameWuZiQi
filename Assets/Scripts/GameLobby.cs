using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Text;
using qi;
using System.IO;
using ProtoBuf;
using System;

public class GameLobby : MonoBehaviour
{
    public UILabel lbl_userName;
    public UIButton btn_login;

    Socket m_socket;
    const string IP = "192.168.0.103";
    const int PORT = 8830;
    // Use this for initialization
    void Start()
    {
        GameSocket.Instance.start();
        // StartCoroutine(test_handle());
        //ThreadPool.QueueUserWorkItem(new WaitCallback(test_handle));
        EventDelegate.Add(btn_login.onClick, onLogin);
    }

    void onLogin()
    {
        if (string.IsNullOrEmpty(lbl_userName.text) == false)
        {
            LoginReq req = new LoginReq() { name = lbl_userName.text };
            send(req.proID, req);
//             MemoryStream ms = new MemoryStream();//default length is 256
//             Serializer.Serialize(ms, req);
//             byte[] arr = ms.GetBuffer();
//             int len = arr.Length;
//             byte[] lenBytes = BitConverter.GetBytes(len);
//             ClientSocket.Instance.sendMessageToGame(ms.GetBuffer());
        }
    }

    void send(int protoid, object oo)
    {
        //byte[] data = ms.GetBuffer();
        //int len = data.Length;
        using (MemoryStream ret = new MemoryStream())
        {
            BinaryWriter bw = new BinaryWriter(ret);
            bw.Write(0);
            bw.Write(protoid);
            Serializer.Serialize(ret, oo);
            int len = (int)ret.Length - 8;
            ret.Position = 0;
            bw.Write(len);
            GameSocket.Instance.sendMessageToGame(ret.ToArray());
            //解析数据看看了
            /*
            byte[] data = ret.ToArray();
            ret.Position = 0;
            BinaryReader br = new BinaryReader(ret);
            print("size is : " + br.ReadInt32());
            MemoryStream mm = new MemoryStream(data, 4, data.Length - 4);
            LoginReq req = Serializer.Deserialize<LoginReq>(mm);
            print(req.proID + "," + req.name);
            */
        }
    }
    /*
    #region test
    void test()
    {
        CircleBuffer cb = new CircleBuffer();
        int a = 1989;
        int b = 1021;
        int c = 730;
        byte[] x = BitConverter.GetBytes(a);
        byte[] y = BitConverter.GetBytes(b);
        byte[] z = BitConverter.GetBytes(c);
        cb.appendData(x, x.Length);
        cb.appendData(y, y.Length);
        cb.appendData(z, z.Length);
        cb.displayAll();
        print("now length is: " + cb.getLength());
        cb.moveNext(8);
        print("now length is: " + cb.getLength());
        cb.appendData(BitConverter.GetBytes(1023), 4);
        cb.appendData(BitConverter.GetBytes(2048), 4);
        cb.displayAll();
        print(cb.readInt());
    }
    AutoResetEvent ar;
    ManualResetEvent mr;
    void test_handle(object state)
    {
        ar = new AutoResetEvent(false);
        mr = new ManualResetEvent(false);
        int i = 0;
        while (true)
        {
            WaitHandle[] a = new WaitHandle[2];
            a[0] = ar;
            a[1] = mr;
            print("enter while");

            //ar.WaitOne();
            //break;
            int n = WaitHandle.WaitAny(a);
            print("n is " + n);
            if (n == 0)
            {
                print("break now");
                break;
            }
            else
            {
                print("aaaa dosth");

            }
            i++;
            if (i > 100)
            {
                break;
            }
        }
        print("end of while");
    }
    void OnGUI()
    {
        if (GUILayout.Button("waaaa"))
        {
            //ar.Set();

            //same with the following two sentences
            //mr.Set();
            //mr.Reset();
            string aaa = "what the fuck";
            byte[] mmm = Encoding.UTF8.GetBytes(aaa);
            ClientSocket.Instance.sendMessageToGame(mmm);
            print("send data now");
        }
        if (GUILayout.Button("close"))
        {
            ClientSocket.Instance.stop();
        }
    }
    #endregion
    */
}
