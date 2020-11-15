using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dog
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetSum();
        }

        const int p1 = 1, p2 = 2, p3 = 0; //пороги

        int s1 = 0, s2 = 0, s3 = 0; //сумматоры

        int x1 = 0, x2 = 0, x3 = 0; //дискретные входные сигналы
        
        int y1 = 0, y2 = 0, y3 = 0; //дискретные выходные сигналы

        public void SetSum()
        {
            s1 = x1;
            if (s1 >= p1)
            {
                res1.Background = new SolidColorBrush(Colors.LightGreen);
                n1.Fill = new SolidColorBrush(Colors.Red);
                y1 = 1;
            }
            else
            {
                res1.Background = new SolidColorBrush(Colors.White);
                n1.Fill = new SolidColorBrush(Colors.White);
                y1 = 0;
            }
            s2 = 2 * x2 - 1 * x3;
            if (s2 >= p2)
            {
                res2.Background = new SolidColorBrush(Colors.LightGreen);
                n2.Fill = new SolidColorBrush(Colors.Red);
                y2 = 1;
            }
            else
            {
                res2.Background = new SolidColorBrush(Colors.White);
                n2.Fill = new SolidColorBrush(Colors.White);
                y2 = 0;
            }
            s3 = -1 * y1 - 1 * y2;
            if (s3 >= p3)
            {
                res3.Background = new SolidColorBrush(Colors.LightGreen);
                n3.Fill = new SolidColorBrush(Colors.Red);
                y3 = 1;
            }
            else
            {
                res3.Background = new SolidColorBrush(Colors.White);
                n3.Fill = new SolidColorBrush(Colors.White);
                y3 = 0;
            }
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            if (x1 == 0) 
            { 
                btn1.Background = new SolidColorBrush(Colors.LightGreen); 
                x1 = 1;
            } 
            else 
            { 
                btn1.Background = new SolidColorBrush(Colors.White);
                x1 = 0;
            }
            SetSum();
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            if (x2 == 0)
            {
                btn2.Background = new SolidColorBrush(Colors.LightGreen);
                x2 = 1;
            }
            else
            {
                btn2.Background = new SolidColorBrush(Colors.White);
                x2 = 0;
            }
            SetSum();
        }

        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            if (x3 == 0)
            {
                btn3.Background = new SolidColorBrush(Colors.LightGreen);
                x3 = 1;
            }
            else
            {
                btn3.Background = new SolidColorBrush(Colors.White);
                x3 = 0;
            }
            SetSum();
        }

        
    }
}
