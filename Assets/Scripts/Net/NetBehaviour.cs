using UnityEngine;
using System.Collections;

public class NetBehaviour : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % 10 == 0)
        {
            GameNetLogic.Instance.update();
        }
    }
}
