using UnityEngine;
using System.Collections;

public class PlayerInformation
{
    static PlayerInformation _instance;
    public static PlayerInformation Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new PlayerInformation();
            }
            return _instance;
        }
    }
    public int m_userID;
}