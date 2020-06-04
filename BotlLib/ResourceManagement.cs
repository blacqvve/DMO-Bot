// Decompiled with JetBrains decompiler
// Type: DMOReaper.ResourceManagement
// Assembly: DMO Reaper, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 572F02F5-7920-4E84-A8BE-03324AAC1898
// Assembly location: C:\Users\yigit\Desktop\update\DMO Reaper.exe

using System.Drawing;

namespace BotLib
{
  public class ResourceManagement
  {
    public static bool isCalibrated = false;

    public PixelColorSlot current { get; set; }

    public PixelColorSlot target { get; set; }

    public Point p { get; set; }

    public int type { get; set; }

    public bool verify()
    {
      return this.current.Equals((object) this.target);
    }
  }
}
