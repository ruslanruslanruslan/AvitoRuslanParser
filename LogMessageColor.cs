using System.Drawing;

namespace AvitoRuslanParser
{
  class LogMessageColor
  {
    public static Color Information()
    {
      return Color.Black;
    }
    public static Color Warning()
    {
      return Color.Gold;
    }
    public static Color Error()
    {
      return Color.Red;
    }
    public static Color Success()
    {
      return Color.LimeGreen;
    }
  }
}
