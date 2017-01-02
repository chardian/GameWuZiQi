using UnityEngine;
using System.Collections;
using PureMVC.Core;
using PureMVC.Interfaces;
using PureMVC.Patterns;

public class GameFacade:Facade
{
    public const string ON_LOGIN = "on login success";
    public const string ON_GET_ROOM = "on get room";

    protected override void InitializeModel()
    {
        base.InitializeModel();
    }
    protected override void InitializeView()
    {
        base.InitializeView();
    }
    protected override void InitializeController()
    {
        base.InitializeController();
        //command
    }
    protected override void InitializeFacade()
    {
        base.InitializeFacade();
    }
    public new static IFacade Instance
    {
        get
        {
            if(m_instance == null)
            {
                lock(m_staticSyncRoot)
                {
                    if(m_instance == null)
                    {
                        m_instance = new GameFacade();
                    }
                }
            }
            return m_instance;
        }
    }
}
