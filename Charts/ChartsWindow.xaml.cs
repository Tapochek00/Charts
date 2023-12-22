using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Charts
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ChartsWindow : Window
    {
        GraphLine barometerGraph;
        GraphLine anemometerGraph;
        DispatcherTimer timer;
        List<double> baraYs = new List<double>();
        List<double> anemoYs = new List<double>();
        DateTime date;
        SolidColorBrush anemoBtnColor = new SolidColorBrush(Colors.RoyalBlue);
        SolidColorBrush baraBtnColor = new SolidColorBrush(Colors.MediumVioletRed);
        public ChartsWindow()
        {
            InitializeComponent();

            // Расставление значений Y
            for (int i = 0; i <= 1000 / 50; i++)
            {
                TextBlock tb = new TextBlock
                {
                    Text = (i * 50).ToString(),
                    Margin = new Thickness(5, 1000 - i * 50, 0, 0)
                };
                canv.Children.Add(tb);
            }

            // Создание графиков
            barometerGraph = new GraphLine(new SolidColorBrush(Colors.Red));
            anemometerGraph = new GraphLine(new SolidColorBrush(Colors.Blue));
            barometerGraph.AddLineToCanvas(canv);
            anemometerGraph.AddLineToCanvas(canv);

            // Начальная дата (взят период за последние полгода)
            date = DateTime.Now.AddMonths(-6);

            // Создание таймера
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            // Прекращение построения графиков при достижении текущего числа
            if (date > DateTime.Now) timer.Stop();

            // Добавление случайных точек на графики
            barometerGraph.AddPoint(barometerGraph.GetRandomPoint());
            anemometerGraph.AddPoint(anemometerGraph.GetRandomPoint());

            // Добавление последней точки графика барометра в коллекцию
            double baraY = barometerGraph.GetLastPoint().Y;
            baraYs.Add(baraY);

            // Создание точки со значением
            CreateValueButton(baraBtnColor, barometerGraph.GetLastPoint().X, baraY);

            // Добавление даты
            int dateX = (int)barometerGraph.GetLastPoint().X;

            TextBlock dateText = new TextBlock
            {
                Text = date.ToString("dd.MM.yy"),
                Margin = new Thickness(dateX, 980, 0, 0)
            };
            date = date.AddDays(1);

            canv.Children.Add(dateText);

            // Добавление последней точки графика анемометра в коллекцию
            double anemoY = anemometerGraph.GetLastPoint().Y;
            anemoYs.Add(anemoY);

            // Создание точки со значением
            CreateValueButton(anemoBtnColor, anemometerGraph.GetLastPoint().X, anemoY);

            // Вычисление средних показателей
            baraAvg.Text = Math.Round(baraYs.Average(), 2).ToString();
            anemoAvg.Text = Math.Round(anemoYs.Average(), 2).ToString();
        }

        // Создание точки со значением
        private void CreateValueButton(SolidColorBrush color, double x, double y)
        {
            Button btn = new Button
            {
                Background = color,
                Width = 4,
                Height = 4,
                BorderThickness = new Thickness(0),
                ToolTip = (1000 - y).ToString()
            };
            btn.MouseEnter += Btn_MouseEnter;
            btn.MouseLeave += Btn_MouseLeave;
            canv.Children.Add(btn);
            Canvas.SetTop(btn, y);
            Canvas.SetLeft(btn, x - 2);
        }

        // Прекращение наведения на точку со значением
        private void Btn_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {   
            barabarabara.Text = "";
            anemo.Text = "";
        }

        // Наведение на точку со значением
        private void Btn_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Background == baraBtnColor)
                barabarabara.Text = (1000 - Canvas.GetTop(btn)).ToString();
            else
                anemo.Text = (1000 - Canvas.GetTop(btn)).ToString();
        }
    }
}