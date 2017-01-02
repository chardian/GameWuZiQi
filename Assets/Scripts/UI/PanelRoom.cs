using UnityEngine;
using System.Collections;
using ProtoBuf;

public class PanelRoom : MonoBehaviour {
    public UILabel lbl_roomName;
    public UILabel lbl_roomCount;
    public UIButton btn_enter;
    private qi.Room m_room;
	// Use this for initialization
	void Start () {
	}
	void OnEnter()
    {
        qi.EnterRoomReq req = new qi.EnterRoomReq();
        req.roomID = m_room.roomID;
        //req.userID = 
        GameSocket.Instance.send(req.proID, req);
    }
	public void set(qi.Room r)
    {
        m_room = r;
        lbl_roomCount.text = m_room.count + "/" + m_room.max;
        lbl_roomName.text = m_room.count.ToString();
        EventDelegate.Add(btn_enter.onClick, OnEnter);
    }
    public void OnDestroy()
    {

    }
}
