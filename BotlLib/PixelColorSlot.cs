// Decompiled with JetBrains decompiler
// Type: DMOReaper.PixelColorSlot
// Assembly: DMO Reaper, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 572F02F5-7920-4E84-A8BE-03324AAC1898
// Assembly location: C:\Users\yigit\Desktop\update\DMO Reaper.exe

using System;

namespace BotLib
{
  public class PixelColorSlot
  {
    public int r { get; set; }

    public int g { get; set; }

    public int b { get; set; }

    public string desc { get; set; }

    public PixelColorSlot(int r, int g, int b)
    {
      this.r = r;
      this.g = g;
      this.b = b;
    }

    public override bool Equals(object obj)
    {
      return Math.Abs(this.r - ((PixelColorSlot) obj).r) < 20 && Math.Abs(this.g - ((PixelColorSlot) obj).g) < 20 && Math.Abs(this.b - ((PixelColorSlot) obj).b) < 20;
    }
  }
}
