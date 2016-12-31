using UnityEngine;
using System.Collections;
using ProtoBuf;
using qi;
using System.IO;

//逻辑层
public class GameNetLogic
{
    void OnLoginAck(byte[] data)
    {
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
        MemoryStream ms = new MemoryStream(data);
        LoginAck ack = Serializer.Deserialize<LoginAck>(ms);
        

    }
}
