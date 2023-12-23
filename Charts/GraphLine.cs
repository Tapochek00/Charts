using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Charts
{
    class GraphLine : IGraphLine
    {
        private const double _maxY = 1000;

        private Polyline Line { get; set; }
        private int? X { get; set; }

        public GraphLine(SolidColorBrush stroke)
        {
            Line = new Polyline();
            Line.Stroke = stroke;
            Line.StrokeThickness = 2;
        }

        /// <summary>
        /// Добавление точки из Point
        /// </summary>
        /// <param name="point">Добавляемая точка</param>
        public void AddPoint(Point point)
        {
            if (point.Y < 0) point.Y = 0;
            if (point.Y > 1000) point.Y = 1000;
            point.Y = _maxY - point.Y;
            if (X == null) X = 0;
            else if (point.X != X) X += 50;
            point.X = (double)X;
            Line.Points.Add(point);
        }

        /// <summary>
        /// Добавление точки из чисел
        /// </summary>
        /// <param name="x">Координата точки X</param>
        /// <param name="y">Координата точки Y</param>
        public void AddPoint(double y)
        {
            if (y < 0) y = 0;
            if (y > 1000) y = 1000;
            y = _maxY - y;
            if (X == null) X = 0;
            else X += 50;
            Line.Points.Add(new Point((double)X, y));
        }

        /// <summary>
        /// Генерация случайной точки с ограничением 
        /// отклонения от предыдущего значения
        /// </summary>
        /// <returns>Новая точка</returns>
        public Point GetRandomPoint()
        {
            Random rnd = new Random();
            if (X == null) X = 0;
            else X += 50;
            double newY;
            int yMin;
            int yMax;
            try
            {
                int prevY = (int)(_maxY - GetLastPoint().Y);
                yMin = prevY - 100 > 0 ? prevY - 100 : 0;
                yMax = prevY + 100 < _maxY ? prevY + 100 : (int)_maxY;
                newY = rnd.Next(yMin, yMax);
            }
            catch
            {
                yMin = 0;
                yMax = (int)_maxY;
            }
            newY = rnd.Next(yMin, yMax);
            return new Point(0, newY);
        }

        /// <summary>
        /// Добавление линии на график
        /// </summary>
        public void AddLineToCanvas(Canvas canv)
        {
            canv.Children.Add(Line);
        }

        /// <summary>
        /// Получение последней точки линии
        /// </summary>
        /// <returns>Последняя точка линии, если точки есть. Точка (-1;-1), если точек нет</returns>
        public Point GetLastPoint()
        {
            if (Line.Points.Count != 0)
                return Line.Points.Last();
            else return new Point(-1, -1);
        }
    }
}
