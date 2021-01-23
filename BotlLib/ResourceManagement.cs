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
