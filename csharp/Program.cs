using System;
using System.Numerics;
using System.Runtime.CompilerServices;

using System.Linq;
 
public class ClientRect
{
  public int Left { get; set; }
  public int Top { get; set; }
  public int Right { get; set; }
  public int Bottom { get; set; }

  public int Width => Right - Left;
  public int Height => Bottom - Top;

  public int X => Left;
  public int Y => Top;
}


public class HelloWorld
{

  // index 回滚: 6 -> 5 -> 4 -> 3 -> 2 -> 1
  static void SubdivideX(double x, double divisionInPixels, double division, int index, double y, double height, int tickLevel, int[] subdivs)
  {
    double[] tickHeights = new double[6] { 1.0, 0.6, 0.35, 0.25, 0.1, 0.075 };
    double[] integerSubDivisors = new double[3] { 2.0, 5.0, 2.0 };


    string str = "";


    Console.WriteLine($"{string.Join("", Enumerable.Repeat("-", 3- tickLevel))} {string.Join("", Enumerable.Repeat("  ", 3+ tickLevel))} x:{x} , divisionInPixels: {divisionInPixels}, division: {division}, index: {index}, tickLevel: {tickLevel}");


    double heightP = height * tickHeights[tickLevel];
    // dc.DrawLine(x + 0.5, y, x + 0.5, y - heightP, tickBrush, 1.0);
    if (index > 10 || tickLevel >= 5)
    {
      return;
    }
    double div;
    if (subdivs != null && index >= 0)
    {
      div = subdivs[index % subdivs.Length];
    }
    else
    {
      if (index >= 0)
      {
        return;
      }
      // divLookupIndex 获取 index 前面一个 刻度
      int divLookupIndex = (-index - 1) % integerSubDivisors.Length;
      // Console.WriteLine($"tickLevel:{tickLevel} , divLookupIndex: {divLookupIndex}, divisionInPixels: {divisionInPixels}");
      if (tickLevel == 0 && divLookupIndex != 0 && divisionInPixels <= 80.0)
      {
        divLookupIndex = 1; // 5
        height *= 0.6;
      }
      if (tickLevel == 1 && divisionInPixels >= 40.0)
      {
        divLookupIndex = 1;
      }
      // { 2.0, 5.0, 2.0 }
      div = integerSubDivisors[divLookupIndex];

      // Console.WriteLine($"div:{div}");
    }
    double deltaDiv = divisionInPixels / div;
    double divisionP = division / div;

    // Console.WriteLine($"div:{div},deltaDiv:{deltaDiv},division:{division},divisionP:{divisionP},");
    if (!(subdivs == null && divisionP != (double)(int)divisionP) && deltaDiv > 6.5)
    {
      for (int i = 0; (double)i < div; i++)
      {
        double xP = x + divisionInPixels * (double)i / div;
        SubdivideX(xP, deltaDiv, divisionP, index + 1, y, height, tickLevel + 1, subdivs);
      }
    }
  }

  public static void Main()
  {

    double offsetPixels = 0;
    ClientRect clientRect = new ClientRect();
    clientRect.Left = 0;
    clientRect.Top = 0;
    clientRect.Right = 500;
    clientRect.Bottom = 50;

    int[] subdivs = null;
    int endMajor = 500;
    int startMajor = 0;
    double majorSkip = 1.0;
    int majorSkipPower = 0;
    // dpu === 1 px
    double majorDivisionLength = 1;
    double majorDivisionPixels = 1;
    // 如果 majorDivisionPixels = 0.1 , majorSkip = 1000 , majorSkipPower = 9
    double[] majorDivisors = new double[3] { 2.0, 2.5, 2.0 };

    while (majorDivisionPixels * majorSkip < 60.0)
    {
      // majorDivisors = new double[3] { 2.0, 2.5, 2.0 };
      majorSkip *= majorDivisors[majorSkipPower % majorDivisors.Length];
      majorSkipPower++;

      Console.WriteLine($"majorSkip:{majorSkip} , majorSkipPower: {majorSkipPower} , ds: {majorDivisors[majorSkipPower % majorDivisors.Length]}");
    }

    //将 startMajor 取整到 majorSkip的倍数
    startMajor = (int)(majorSkip * Math.Floor((double)startMajor / majorSkip));

    for (int major = startMajor; major <= endMajor; major += (int)majorSkip)
    {
      double majorMarkPos = (double)major * majorDivisionPixels + offsetPixels;
      string majorText = major.ToString();
      double maxHeight;

      SubdivideX(
        (double)clientRect.Left + majorMarkPos,
      majorDivisionPixels * majorSkip,
      majorSkip,
       -majorSkipPower,
       clientRect.Bottom - 1,
        clientRect.Height - 1,
         0, subdivs);
      maxHeight = clientRect.Height;
      double textX2 = 2.0 + (double)clientRect.X + majorMarkPos;
      double textY = -1.0;
      // Console.WriteLine($"majorText:{majorText} , textX2: {textX2}");

    }
  }


}

