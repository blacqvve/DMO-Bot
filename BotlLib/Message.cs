// Decompiled with JetBrains decompiler
// Type: DMOReaper.Message
// Assembly: DMO Reaper, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 572F02F5-7920-4E84-A8BE-03324AAC1898
// Assembly location: C:\Users\yigit\Desktop\update\DMO Reaper.exe

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BotLib
{
  public class Message
  {
    [BsonElement("message")]
    [BsonRequired]
    public string message { get; set; }

    [BsonId]
    public ObjectId Id { get; set; }

    public Message(string message)
    {
      this.message = message;
    }
  }
}
