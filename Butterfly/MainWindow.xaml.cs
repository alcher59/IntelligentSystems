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

namespace Butterfly
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        int x1 = 0, x2 = 0, x3 = 0, x4 = 0;  //входные сигналы
        int y1 = 0, y2 = 0, y3 = 0; //вых сигналы
        int p1 = 2, p2 = 2, p3 = 4; //пороги

        
        int s1 = 0, s2 = 0, s3 = 0; //сумматоры
        public void clear()
        {
            x1 = x2 = x3 = x4 = 0; 
            y1 = y2 = y3 = 0; 
            p1 = p2 = 2; p3 = 4; 
            s1 = s2 = s3 = 0;
            n1.Fill = new SolidColorBrush(Colors.White);
            n2.Fill = new SolidColorBrush(Colors.White);
            n3.Fill = new SolidColorBrush(Colors.White);
            temp.IsChecked = false;
            sun.IsChecked = false;
            res.IsChecked = false;
        }
        


        public void SetSum()
        {
            s1 = x2*2-x1;
            if (s1 >= p1)
            {
                n1.Fill = new SolidColorBrush(Colors.Red);
                y1 = 1;
            }
            else
            {
                n1.Fill = new SolidColorBrush(Colors.White);
                y1 = 0;
            }
            s2 = x4*2-x3;
            if (s2 >= p2)
            {
                n2.Fill = new SolidColorBrush(Colors.Red);
                y2 = 1;
            }
            else
            {
                n2.Fill = new SolidColorBrush(Colors.White);
                y2 = 0;
            }
            s3 = 2*y1 + 2*y2;
            if (s3 >= p3)
            {
                res.IsChecked = true;
                n3.Fill = new SolidColorBrush(Colors.Red);
                y3 = 1;
            }
            else
            {
                res.IsChecked = false;
                n3.Fill = new SolidColorBrush(Colors.White);
                y3 = 0;
            }
        }
        private void sun_Click(object sender, RoutedEventArgs e)
        {
            //if(e0.IsChecked == true)
            //{

            //}
            //if (e1.IsChecked == true)
            //{

            //}
            //if (e2.IsChecked == true)
            //{

            //}
            if (sun.IsChecked == true)
            {
                x2 = 1;
                x4 = 1;
            }
            else
            {
                x2 = 0;
                x4 = 0;
            }
            SetSum();
        }

        private void temp_Click(object sender, RoutedEventArgs e)
        {
            if (e0.IsChecked == true)
            {
                if (temp.IsChecked == true)
                {
                    x1 = 1;
                    x3 = 1;
                }
                else
                {
                    x1 = 0;
                    x3 = 0;
                }
            }
            if (e1.IsChecked == true)
            {
                if (temp.IsChecked == true)
                {
                    x3 = 1;
                }
                else
                {
                    x3 = 0;
                }
            }
            if (e2.IsChecked == true)
            {
                if (temp.IsChecked == true)
                {
                    x1 = 0;
                    x3 = 1;
                }
                else
                {
                    x1 = 1;
                    x3 = 0;
                }
            }
            SetSum();
        }
        private void e2_Click(object sender, RoutedEventArgs e)
        {
            clear();
        }

        private void e1_Click(object sender, RoutedEventArgs e)
        {
            clear();
        }

        private void e0_Click(object sender, RoutedEventArgs e)
        {
            clear();
        }

    }
}
