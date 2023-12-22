using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Charts
{
    interface IGraphLine
    {
        void AddPoint(Point point);
        Point GetRandomPoint();
        void AddLineToCanvas(Canvas canv);
        Point GetLastPoint();
    }
}
