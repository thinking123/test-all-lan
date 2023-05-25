using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

public class HelloWorld
{


  public static byte ConvertToByte(float toleranceSettingValue)
  {
    byte num = checked((byte)Math.Round((double)toleranceSettingValue * 255.0, MidpointRounding.AwayFromZero));
    return FastScale(num, num);
  }

  public static byte FastScale(byte x, byte frac)
  {
    int r1 = x * frac + 128;
    return (byte)((r1 >> 8) + r1 >> 8);
  }


  class Bs
  {
    byte b;
    int i;

    Bs(byte b, int i)
    {
      this.b = b;
      this.i = i;
    }

    public override string ToString()
    {
      return $"{b},{i})";
    }
  }

  public static void Main1()
  {
    List<string> myList = new List<string>();
    for (int i = 0; i <= 100; i++)
    {
      // 在这里执行循环体中的代码

      Bs bs = new Bs(ConvertToByte(i / 100.0f), i);

      myList.Add(bs.ToString());
    }

  }

}
