using UnityEngine;
using System.Collections;
using System;
using System.Net.Sockets;
using System.Net;
using System.Threading;

public class GameLobby : MonoBehaviour
{
    Socket m_socket;
    const string IP = "192.168.0.103";
    const int PORT = 8830;
    // Use this for initialization
    void Start()
    {
        //ClientSocket.Instance.start();
        // StartCoroutine(test_handle());
        ThreadPool.QueueUserWorkItem(new WaitCallback(test_handle));
    }



    void receive()
    {

    }

    void send()
    {

    }
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
            if(i > 100)
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
            mr.Set();
            //mr.Reset();
        }
    }
    #endregion
}
