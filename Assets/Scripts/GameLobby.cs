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
using PureMVC.Core;
using PureMVC.Interfaces;
using System.Collections.Generic;

public class GameLobby : MonoBehaviour, IMediator
{
    public UILabel lbl_userName;
    public UIButton btn_login;
    public GameObject go_login;
    public UIGrid grid_rooms;
    // Use this for initialization
    void Start()
    {
        GameFacade.Instance.RegisterMediator(this);
        GameSocket.Instance.start();
        EventDelegate.Add(btn_login.onClick, onLogin);
    }

    void onLogin()
    {
        if (string.IsNullOrEmpty(lbl_userName.text) == false)
        {
            LoginReq req = new LoginReq() { name = lbl_userName.text };
            GameSocket.Instance.send(req.proID, req);
        }
    }

    void OnDestroy()
    {
        print("on gamelobby destroy");
        GameFacade.Instance.RemoveMediator(MediatorName);
        GameSocket.Instance.stop();
    }

    #region MVC
    public string MediatorName
    {
        get
        {
            return "GameLobby";
        }
    }

    public object ViewComponent
    {
        get
        {
            return this;
        }
        set { }
    }
    public IList<string> ListNotificationInterests()
    {
        IList<string> l = new List<string>();
        l.Add(GameFacade.ON_LOGIN);
        l.Add(GameFacade.ON_GET_ROOM);
        return l;
    }

    public void HandleNotification(INotification notification)
    {
        if (notification.Name == GameFacade.ON_LOGIN)
        {
            LoginAck ack = (LoginAck)(notification.Body);
            print("ack is " + ack.id + "," + ack.ret + ",");
        }
        else if (notification.Name == GameFacade.ON_GET_ROOM)
        {
            GetRoomAck ack = (GetRoomAck)(notification.Body);
            GameObject prefab = Resources.Load("PanelRoom") as GameObject;

            int length = ack.rooms.Count;
            for (int i = 0; i < length; i++)
            {
                GameObject go = Instantiate(prefab) as GameObject;
                go.transform.parent = grid_rooms.transform;
                go.transform.localScale = Vector3.one;
                go.transform.localPosition = Vector3.zero;
                PanelRoom pr = go.GetComponent<PanelRoom>();
                pr.set(ack.rooms[i]);
            }
            grid_rooms.repositionNow = true;

        }
    }

    public void OnRegister()
    {
    }

    public void OnRemove()
    {
    }
    #endregion
}
