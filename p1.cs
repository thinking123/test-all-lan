using System;
using System.Numerics;
using System.Runtime.CompilerServices;



public class HelloWorld
{

  class ColorBgra
  {
    public byte R;
    public byte G;
    public byte B;
    public byte A;
    public ColorBgra(byte R, byte G, byte B, byte A)
    {
      this.R = R;
      this.G = G;
      this.B = B;
      this.A = A;
    }

    public byte Bgra
    {
      get
      {
        return A;
      }
    }

    public static implicit operator ColorRgba128Float(ColorBgra c)
    {
      return new ColorRgba128Float(ToScalingFloat(c.R), ToScalingFloat(c.G), ToScalingFloat(c.B), ToScalingFloat(c.A));
    }
  }

  class ColorRgba128Float
  {
    public float r;

    public float g;

    public float b;

    public float a;
    public ColorRgba128Float(float r, float g, float b, float a)
    {
      this.r = r;
      this.g = g;
      this.b = b;
      this.a = a;
    }

    public static explicit operator Vector4(ColorRgba128Float color)
    {
      return Unsafe.As<ColorRgba128Float, Vector4>(ref color);
    }

  }
  private static byte GetDistancePremultiplied(ColorBgra x, ColorBgra y)
  {
    if (x.Bgra == y.Bgra || (x.A == 0 && y.A == 0))
    {
      return 0;
    }
    Vector4 xF = new Vector4((int)x.R, (int)x.G, (int)x.B, (int)x.A) * new Vector4(0.003921569f);
    Vector4 yF = new Vector4((int)y.R, (int)y.G, (int)y.B, (int)y.A) * new Vector4(0.003921569f);
    Vector4 xFPM = new Vector4(xF.W, xF.W, xF.W, 1f);
    Vector4 yFPM = new Vector4(yF.W, yF.W, yF.W, 1f);
    Vector4 xFP = xF * xFPM;
    Vector4 yFP = yF * yFPM;
    Vector4 xOffsetVec = (new Vector4(1f) - new Vector4(xFP.W)) * new Vector4(0.5f, 0.5f, 0.5f, 0f);
    Vector4 yOffsetVec = (new Vector4(1f) - new Vector4(yFP.W)) * new Vector4(0.5f, 0.5f, 0.5f, 0f);
    byte distance = GetDistanceEuclidean(xFP + xOffsetVec, yFP + yOffsetVec);
    return Math.Max((byte)1, distance);
  }
  private static byte GetDistanceEuclidean(Vector4 color1, Vector4 color2)
  {
    return (byte)(int)FastRoundAwayFromZero(Vector4.Distance(color1, color2) * 0.5f * 255f);
  }

  static float FastRoundAwayFromZero(float x)
  {
    if (x > 0f)
    {
      return (float)Math.Floor((double)x + 0.5);
    }
    if (x < -0f)
    {
      return (float)Math.Ceiling((double)x - 0.5);
    }
    return x;
  }

  static byte GetDistanceStraight(ColorBgra _x, ColorBgra _y)
  {
    if (_x.Bgra == _y.Bgra)
    {
      return 0;
    }
    ColorRgba128Float x = _x;
    ColorRgba128Float y = _y;
    Vector4 Vector1 = new Vector4(x.r, x.g, x.b, x.a);
    Vector4 Vector2 = new Vector4(y.r, y.g, y.b, y.a);

    byte distance = GetDistanceEuclidean(color2: Vector2, color1: Vector1);
    return Math.Max((byte)1, distance);
  }


  static float ToScalingFloat(byte x)
  {
    return (float)(double)(int)x * (1.0 / 255.0);
  }


  public static void Main1()
  {
    ColorBgra x = new ColorBgra((byte)1, (byte)2, (byte)3, (byte)4);
    ColorBgra y = new ColorBgra((byte)10, (byte)20, (byte)30, (byte)40);
    byte res = GetDistancePremultiplied(x, y);
    Console.WriteLine("res");
    Console.WriteLine(res);
    //22
    byte res1 = GetDistanceStraight(x, y);
    Console.WriteLine("res1");
    Console.WriteLine(res1);
    //1
  }
}