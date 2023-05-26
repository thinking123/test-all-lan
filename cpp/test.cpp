#include <iostream>
#include <array>
#include <string>
#include <cmath>

class ScaleFactor {
private:
    int denominator;
    int numerator;

    static std::string percentageFormat;
    

public:
    static const ScaleFactor OneToOne;
    static const ScaleFactor MinValue;
    static const ScaleFactor MaxValue;
    // static std::array<ScaleFactor, 39> scales;
    static const ScaleFactor scales[39];

    ScaleFactor(int num, int den, bool clamp)
        : numerator(num), denominator(den) {}

    int GetDenominator() const { return denominator; }
    int GetNumerator() const { return numerator; }

    friend std::ostream& operator<<(std::ostream& os, const ScaleFactor& scaleFactor) {
        os << "Numerator: " << scaleFactor.GetNumerator() << ", Denominator: " << scaleFactor.GetDenominator();
        return os;
    }

     float Unscale(float value){
        return value * denominator;
    }

     double Scale( double x)
    {
        return x * static_cast<double>(numerator) / static_cast<double>(denominator);
    }

     double  Unscale(double x)
    {
        return x * static_cast<double>(denominator) / static_cast<double>(numerator);
    }
};

const ScaleFactor ScaleFactor::OneToOne = ScaleFactor(1, 1, false);
const ScaleFactor ScaleFactor::MinValue = ScaleFactor(1, 100, false);
const ScaleFactor ScaleFactor::MaxValue = ScaleFactor(64, 1, false);
const ScaleFactor ScaleFactor::scales[39] = {
        ScaleFactor(1, 64, false),
        ScaleFactor(1, 56, false),
        ScaleFactor(1, 48, false),
        ScaleFactor(1, 40, false),
        ScaleFactor(1, 32, false),
        ScaleFactor(1, 28, false),
        ScaleFactor(1, 24, false),
        ScaleFactor(1, 20, false),
        ScaleFactor(1, 16, false),
        ScaleFactor(1, 14, false),
        ScaleFactor(1, 12, false),
        ScaleFactor(1, 10, false),
        ScaleFactor(1, 8, false),
        ScaleFactor(1, 6, false),
        ScaleFactor(1, 5, false),
        ScaleFactor(1, 4, false),
        ScaleFactor(1, 3, false),
        ScaleFactor(1, 2, false),
        ScaleFactor(2, 3, false),
        ScaleFactor(1, 1, false),
        ScaleFactor(3, 2, false),
        ScaleFactor(2, 1, false),
        ScaleFactor(3, 1, false),
        ScaleFactor(4, 1, false),
        ScaleFactor(5, 1, false),
        ScaleFactor(6, 1, false),
        ScaleFactor(8, 1, false),
        ScaleFactor(10, 1, false),
        ScaleFactor(12, 1, false),
        ScaleFactor(14, 1, false),
        ScaleFactor(16, 1, false),
        ScaleFactor(20, 1, false),
        ScaleFactor(24, 1, false),
        ScaleFactor(28, 1, false),
        ScaleFactor(32, 1, false),
        ScaleFactor(40, 1, false),
        ScaleFactor(48, 1, false),
        ScaleFactor(56, 1, false),
        ScaleFactor(64, 1, false)
    };


 
struct ClientRect
{
    float Height;
    float Width;
    float Left;
    float Top;
    float Right;
    float Bottom;
    float X;
    float Y;
};

class Ruler{
public:
    void RenderTicksAndLabels(){
          ClientRect clientRect;
            clientRect.Height = 50.0f;
            clientRect.Width = 1000.0f;
            clientRect.Left = 0.0f;
            clientRect.Top = 0.0f;
            clientRect.Right = clientRect.Left + clientRect.Width;
            clientRect.Bottom = clientRect.Top + clientRect.Height;
            clientRect.X = clientRect.Left;
            clientRect.Y = clientRect.Top;
        ScaleFactor scaleFactor(10, 1, false);
        double majorDivisors[] = { 2.0, 2.5, 2.0 };
        //   double AdjustedOffset = -100.0;
        //   double AdjustedOffset = 0.0;

//canvasX= -300
// Offset = 0 - canvasX
// AdjustedOffset = Offset + ScaleFactor.Unscale(base.ClientRectangle.Height)
           double canvasX = -300;
           double Offset = 0 - canvasX;
        //    double AdjustedOffset = Offset + scaleFactor.Unscale(clientRect.Height);

double AdjustedOffset = 0.0;
        // ScaleFactor scaleFactor(64, 1, false);
        // ScaleFactor scaleFactor(10, 1, false);
      

        int orientation = 0;
        float dpu = 1.0;

        int maxPixel = (orientation != 0) ? static_cast<int>(scaleFactor.Unscale(clientRect.Height)) : static_cast<int>(scaleFactor.Unscale(clientRect.Width));
        
        double adjustedOffset = AdjustedOffset;
        double majorSkip = 1.0;
        int majorSkipPower = 0;

        double majorDivisionLength = dpu;
        double majorDivisionPixels = scaleFactor.Scale(majorDivisionLength);

        // int* subdivs = GetSubdivisions(measurementUnit);
        int* subdivs = nullptr;

        double offsetPixels = scaleFactor.Scale(adjustedOffset) - 1.0;
        int startMajor = static_cast<int>((0.0 - adjustedOffset) / majorDivisionLength) - 1;
        int endMajor = static_cast<int>(((static_cast<double>(maxPixel) - adjustedOffset) / majorDivisionLength)) + 1;

        while (majorDivisionPixels * majorSkip < 60.0)
        {
            majorSkip *= majorDivisors[majorSkipPower % 3];
            majorSkipPower++;
        }

        startMajor = static_cast<int>(majorSkip * std::floor(static_cast<double>(startMajor) / majorSkip));


      for (int major = startMajor; major <= endMajor; major += static_cast<int>(majorSkip)) {
            double majorMarkPos = static_cast<double>(major) * majorDivisionPixels + offsetPixels;
            std::string majorText = std::to_string(major);
            double maxHeight;

            if (orientation == 0 /* Assuming orientation is an integer variable */) {
                SubdivideX(static_cast<double>(clientRect.Left) + majorMarkPos,
                        majorDivisionPixels * majorSkip, majorSkip, -majorSkipPower,
                        clientRect.Bottom - 1, clientRect.Height - 1, 0, subdivs);

                // CachingUITextFactory cachingUITextFactory = uiTextFactory;
                // SizedFontProperties value = tickLabelFontLazy.Value;
                // maxHeight = clientRect.Height;

                // TextLayout textLayout2 = cachingUITextFactory.CreateLayout(dc, majorText, value, nullptr,
                //                                                         HotkeyRenderMode::Ignore, 65535.0, maxHeight);
                double textX2 = 2.0 + static_cast<double>(clientRect.X) + majorMarkPos;
                double textY = -1.0;

       std::cout << "textX2: " << textX2   << std::endl;
                // dc.DrawTextLayout(textX2, textY, textLayout2, labelBrush);
                continue;
            }
        }

    }

    void SubdivideX(double x, double divisionInPixels, double division, int index, double y, double height, int tickLevel, int* subdivs) {
        double tickHeights[6] = { 1.0, 0.6, 0.35, 0.25, 0.1, 0.075 };
        double integerSubDivisors[3] =  { 2.0, 5.0, 2.0 };
        double heightP = height * tickHeights[tickLevel];
        
        // 使用 IDrawingContext 对象 dc 进行绘图操作
        // dc.DrawLine(x + 0.5, y, x + 0.5, y - heightP, tickBrush, 1.0);
         std::cout << "tickLevel：" << tickLevel << " x：" << x << " y：" << y << " h：" << (y - heightP) << std::endl;
        
        if (index > 10 || tickLevel >= 5) {
            return;
        }
        
        double div;
        
        if (subdivs != nullptr && index >= 0) {
            // div = subdivs[index % (sizeof(subdivs) / sizeof(subdivs[0]))];
            div = subdivs[index % 3];
        } else {
            if (index >= 0) {
                return;
            }
            
            int divLookupIndex = (-index - 1) % 3;  // 这里假设 integerSubDivisors 的长度为 3
            if (tickLevel == 0 && divLookupIndex != 0 && divisionInPixels <= 80.0) {
                divLookupIndex = 1;
                height *= 0.6;
            }
            if (tickLevel == 1 && divisionInPixels >= 40.0) {
                divLookupIndex = 1;
            }
            
            // double integerSubDivisors[3] = { /* 这里填写 integerSubDivisors 的值 */ };
            div = integerSubDivisors[divLookupIndex];
        }
        
        double deltaDiv = divisionInPixels / div;
        double divisionP = division / div;
        
        if (!(subdivs == nullptr && divisionP != (double)(int)divisionP) && deltaDiv > 6.5) {
            for (int i = 0; (double)i < div; i++) {
                double xP = x + divisionInPixels * (double)i / div;
                
                // 递归调用 SubdivideX 方法
                SubdivideX( xP, deltaDiv, divisionP, index + 1, y, height, tickLevel + 1, subdivs);
            }
        }
    }


};

int main() {

    Ruler ruler;
    ruler.RenderTicksAndLabels();


    // for (int i = 0; i < 39; i++) {
    //   std::cout << ScaleFactor::scales[i] << std::endl;
    // }

    return 0;
}

