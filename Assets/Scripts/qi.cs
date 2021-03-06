//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: qi.proto
namespace qi
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"RoomUser")]
  public partial class RoomUser : global::ProtoBuf.IExtensible
  {
    public RoomUser() {}
    
    private string _name;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"name", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string name
    {
      get { return _name; }
      set { _name = value; }
    }
    private int _state;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"state", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int state
    {
      get { return _state; }
      set { _state = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"LoginReq")]
  public partial class LoginReq : global::ProtoBuf.IExtensible
  {
    public LoginReq() {}
    
    private int _proID = (int)10000;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"proID", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)10000)]
    public int proID
    {
      get { return _proID; }
      set { _proID = value; }
    }
    private string _name;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"name", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string name
    {
      get { return _name; }
      set { _name = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"LoginAck")]
  public partial class LoginAck : global::ProtoBuf.IExtensible
  {
    public LoginAck() {}
    
    private int _proID = (int)10001;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"proID", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)10001)]
    public int proID
    {
      get { return _proID; }
      set { _proID = value; }
    }
    private Result _ret;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"ret", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public Result ret
    {
      get { return _ret; }
      set { _ret = value; }
    }
    private int _id;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int id
    {
      get { return _id; }
      set { _id = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"Room")]
  public partial class Room : global::ProtoBuf.IExtensible
  {
    public Room() {}
    
    private int _roomID;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"roomID", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int roomID
    {
      get { return _roomID; }
      set { _roomID = value; }
    }
    private int _count;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"count", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int count
    {
      get { return _count; }
      set { _count = value; }
    }
    private int _max;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"max", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int max
    {
      get { return _max; }
      set { _max = value; }
    }
    private string _roomName;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"roomName", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string roomName
    {
      get { return _roomName; }
      set { _roomName = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"GetRoomReq")]
  public partial class GetRoomReq : global::ProtoBuf.IExtensible
  {
    public GetRoomReq() {}
    
    private int _proID = (int)10002;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"proID", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)10002)]
    public int proID
    {
      get { return _proID; }
      set { _proID = value; }
    }
    private int _userID;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"userID", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int userID
    {
      get { return _userID; }
      set { _userID = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"GetRoomAck")]
  public partial class GetRoomAck : global::ProtoBuf.IExtensible
  {
    public GetRoomAck() {}
    
    private int _proID = (int)10003;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"proID", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)10003)]
    public int proID
    {
      get { return _proID; }
      set { _proID = value; }
    }
    private Result _ret;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"ret", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public Result ret
    {
      get { return _ret; }
      set { _ret = value; }
    }
    private readonly global::System.Collections.Generic.List<Room> _rooms = new global::System.Collections.Generic.List<Room>();
    [global::ProtoBuf.ProtoMember(3, Name=@"rooms", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Room> rooms
    {
      get { return _rooms; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"EnterRoomReq")]
  public partial class EnterRoomReq : global::ProtoBuf.IExtensible
  {
    public EnterRoomReq() {}
    
    private int _proID = (int)10004;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"proID", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)10004)]
    public int proID
    {
      get { return _proID; }
      set { _proID = value; }
    }
    private int _userID;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"userID", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int userID
    {
      get { return _userID; }
      set { _userID = value; }
    }
    private int _roomID;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"roomID", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int roomID
    {
      get { return _roomID; }
      set { _roomID = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"EnterRoomAck")]
  public partial class EnterRoomAck : global::ProtoBuf.IExtensible
  {
    public EnterRoomAck() {}
    
    private int _proID = (int)10005;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"proID", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)10005)]
    public int proID
    {
      get { return _proID; }
      set { _proID = value; }
    }
    private Result _ret;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"ret", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public Result ret
    {
      get { return _ret; }
      set { _ret = value; }
    }
    private readonly global::System.Collections.Generic.List<RoomUser> _users = new global::System.Collections.Generic.List<RoomUser>();
    [global::ProtoBuf.ProtoMember(3, Name=@"users", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<RoomUser> users
    {
      get { return _users; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"PrepareGameReq")]
  public partial class PrepareGameReq : global::ProtoBuf.IExtensible
  {
    public PrepareGameReq() {}
    
    private int _proID = (int)10006;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"proID", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)10006)]
    public int proID
    {
      get { return _proID; }
      set { _proID = value; }
    }
    private int _userID;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"userID", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int userID
    {
      get { return _userID; }
      set { _userID = value; }
    }
    private int _roomID;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"roomID", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int roomID
    {
      get { return _roomID; }
      set { _roomID = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"PrepareGameAck")]
  public partial class PrepareGameAck : global::ProtoBuf.IExtensible
  {
    public PrepareGameAck() {}
    
    private int _proID = (int)10007;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"proID", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)10007)]
    public int proID
    {
      get { return _proID; }
      set { _proID = value; }
    }
    private Result _ret;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"ret", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public Result ret
    {
      get { return _ret; }
      set { _ret = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ChessReq")]
  public partial class ChessReq : global::ProtoBuf.IExtensible
  {
    public ChessReq() {}
    
    private int _proID = (int)10008;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"proID", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)10008)]
    public int proID
    {
      get { return _proID; }
      set { _proID = value; }
    }
    private int _userID;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"userID", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int userID
    {
      get { return _userID; }
      set { _userID = value; }
    }
    private int _x;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"x", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int x
    {
      get { return _x; }
      set { _x = value; }
    }
    private int _y;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"y", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int y
    {
      get { return _y; }
      set { _y = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ChessAck")]
  public partial class ChessAck : global::ProtoBuf.IExtensible
  {
    public ChessAck() {}
    
    private int _proID = (int)10009;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"proID", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)10009)]
    public int proID
    {
      get { return _proID; }
      set { _proID = value; }
    }
    private Result _ret;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"ret", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public Result ret
    {
      get { return _ret; }
      set { _ret = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"GameStartAck")]
  public partial class GameStartAck : global::ProtoBuf.IExtensible
  {
    public GameStartAck() {}
    
    private int _proID = (int)10010;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"proID", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)10010)]
    public int proID
    {
      get { return _proID; }
      set { _proID = value; }
    }
    private int _userID;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"userID", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int userID
    {
      get { return _userID; }
      set { _userID = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"GameOverAck")]
  public partial class GameOverAck : global::ProtoBuf.IExtensible
  {
    public GameOverAck() {}
    
    private int _proID = (int)10011;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"proID", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)10011)]
    public int proID
    {
      get { return _proID; }
      set { _proID = value; }
    }
    private int _userID;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"userID", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int userID
    {
      get { return _userID; }
      set { _userID = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
    [global::ProtoBuf.ProtoContract(Name=@"Result")]
    public enum Result
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"Success", Value=1)]
      Success = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"Error", Value=2)]
      Error = 2
    }
  
    [global::ProtoBuf.ProtoContract(Name=@"PROTOCOL")]
    public enum PROTOCOL
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"__LoginReq", Value=10000)]
      __LoginReq = 10000,
            
      [global::ProtoBuf.ProtoEnum(Name=@"__LoginAck", Value=10001)]
      __LoginAck = 10001,
            
      [global::ProtoBuf.ProtoEnum(Name=@"__GetRoomReq", Value=10002)]
      __GetRoomReq = 10002,
            
      [global::ProtoBuf.ProtoEnum(Name=@"__GetRoomAck", Value=10003)]
      __GetRoomAck = 10003,
            
      [global::ProtoBuf.ProtoEnum(Name=@"__EnterRoomReq", Value=10004)]
      __EnterRoomReq = 10004,
            
      [global::ProtoBuf.ProtoEnum(Name=@"__EnterRoomAck", Value=10005)]
      __EnterRoomAck = 10005,
            
      [global::ProtoBuf.ProtoEnum(Name=@"__PrepareGameReq", Value=10006)]
      __PrepareGameReq = 10006,
            
      [global::ProtoBuf.ProtoEnum(Name=@"__PrepareGameAck", Value=10007)]
      __PrepareGameAck = 10007,
            
      [global::ProtoBuf.ProtoEnum(Name=@"__ChessReq", Value=10008)]
      __ChessReq = 10008,
            
      [global::ProtoBuf.ProtoEnum(Name=@"__ChessAck", Value=10009)]
      __ChessAck = 10009,
            
      [global::ProtoBuf.ProtoEnum(Name=@"__GameStartAck", Value=10010)]
      __GameStartAck = 10010,
            
      [global::ProtoBuf.ProtoEnum(Name=@"__GameOverAck", Value=10011)]
      __GameOverAck = 10011
    }
  
}