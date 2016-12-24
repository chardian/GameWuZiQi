using UnityEngine;
using System.Collections;

public class GlobalLog
{
    public static void v(string str)
    {
        Debug.Log(System.DateTime.Now.ToString("dd hh:mm:ss:fff") +  str);
    }
}