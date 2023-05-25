using System;
using System.Numerics;
using System.Runtime.CompilerServices;

public class HelloWorld
{
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
  public static void Main1()
  {
    ColorRgba128Float c = new ColorRgba128Float(1f,2f,3f,4f);
    Vector4 v = (Vector4)c;
  }

}
