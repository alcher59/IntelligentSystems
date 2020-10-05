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

namespace SpecialEquipment
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const int propertyCount = 9;
        int[,] tmatr;
        public MainWindow()
        {
            InitializeComponent();

            var list = Filllist();
            var matr = CreateMatrix(list);
            tmatr = TeachMatrix(list, matr);

            List<Obj> objects = new List<Obj>();
            for (int i = 0; i < propertyCount; i++)
            {
                objects.Add(new Obj
                {
                    Title1 = tmatr[i, 0].ToString(),
                    Title2 = tmatr[i, 1].ToString(),
                    Title3 = tmatr[i, 2].ToString(),
                    Title4 = tmatr[i, 3].ToString(),
                    Title5 = tmatr[i, 4].ToString(),
                    Title6 = tmatr[i, 5].ToString()
                });
            }
            dataGrid.ItemsSource = objects;
            //dataGrid.Columns[0].Header = "Самосвал";
            //dataGrid.Columns[1].Header = "Бензовоз";
            //dataGrid.Columns[2].Header = "Кран-борт";
            //dataGrid.Columns[3].Header = "Бульдозер";
            //dataGrid.Columns[4].Header = "Экскаватор";
            //dataGrid.Columns[5].Header = "Бурмашина";
        }



        public List<int[]> Filllist()
        {
            List<int[]> list = new List<int[]>();
            //двс, колеса, гусеницы, ковш, бур, кузов, цистерна, стрела, выносные опоры
            list.Add(new int[propertyCount] { 1, 1, 0, 0, 0, 1, 0, 0, 0 }); //"Самосвал"
            list.Add(new int[propertyCount] { 1, 1, 0, 0, 0, 0, 1, 0, 0 }); //"Бензовоз"
            list.Add(new int[propertyCount] { 1, 0, 0, 0, 0, 1, 0, 1, 1 }); //"Кран-борт" 
            list.Add(new int[propertyCount] { 1, 0, 1, 1, 0, 0, 0, 0, 0 }); //"Бульдозер"
            list.Add(new int[propertyCount] { 1, 0, 1, 1, 0, 0, 0, 1, 0 }); //"Экскаватор"
            list.Add(new int[propertyCount] { 1, 1, 0, 0, 1, 0, 0, 0, 1 }); //"Бурильная машина"
            return list;
        }

        public int[,] CreateMatrix(List<int[]> list)
        {
            int[,] matrix = new int[propertyCount, list.Count];
            for(int i = 0; i < propertyCount; i++)
                for (int j = 0; j < list.Count; j++)
                {
                    matrix[i, j] = 0;
                }
            return matrix;
        }

        public int[,] TeachMatrix(List<int[]> list, int[,] matrix)
        {
            for (int key = 0; key < list.Count; key++)
                for (int i = 0; i < propertyCount; i++)
                    for (int j = 0; j < list.Count; j++)
                    {
                        if (j == key)
                        {
                            matrix[i, j] += list[j][i];
                            if (matrix[i, j] < -1)
                                matrix[i, j] = -1;
                            if (matrix[i, j] > 1)
                                matrix[i, j] = 1;
                        }
                        else
                        {
                            matrix[i, j] -= list[j][i];
                            if (matrix[i, j] < -1)
                                matrix[i, j] = -1;
                            if (matrix[i, j] > 1)
                                matrix[i, j] = 1;
                        }
                    }
            return matrix;
        }
        public string CompareObjects(int[] obj)
        {
            int[] arr = new int[6];
            List<int> maxNumbers = new List<int>();
            string result = "";
            int max = 0;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    arr[i] += tmatr[j, i] * obj[i];
                }
                max = arr.Max();  //максимальное число для определения объекта
                
                //int num = 0;
               
                for (int num = 0; num < 6; num++)
                {
                    if (arr[num] == max)
                        maxNumbers.Append(num);  //номера объектов, на которые похож новый объект
                }
            }
            
            //int n = 1;
            //int resNum = 0;
            List<int> resNums = new List<int>();
            int[] countsObj = new int[6];

            for(int c = 0; c < countsObj.Length; c++)
            {
                countsObj[c] = maxNumbers.Select(x => x.Equals(c+1)).ToList().Count; //кол-во объектов каждого вида, на которые похож новый объект
            }
            int maxCounts = countsObj.Max();
            for (int c = 0; c < countsObj.Length; c++)
            {
                if (countsObj[c] == maxCounts)
                {
                    if (result != "")
                        result += "/";
                    switch (countsObj[c])
                    {
                        case 1:
                            result += "Самосвал";
                            break;
                        case 2:
                            result += "Бензовоз";
                            break;
                        case 3:
                            result += "Кран-борт";
                            break;
                        case 4:
                            result += "Бульдозер";
                            break;
                        case 5:
                            result += "Экскаватор";
                            break;
                        case 6:
                            result += "Бурильная машина";
                            break;
                    }
                    
                }
            }

            return result;
        }
        public class Obj
        {
            public string Title1 { get; set; }
            public string Title2 { get; set; }
            public string Title3 { get; set; }
            public string Title4 { get; set; }
            public string Title5 { get; set; }
            public string Title6 { get; set; }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int[] obj = {
                engine_check.IsChecked == true ? 1 : 0,
                wheels_check.IsChecked == true ? 1 : 0,
                caterpillars_check.IsChecked == true ? 1 : 0,
                bucket_check.IsChecked == true ? 1 : 0,
                boer_check.IsChecked == true ? 1 : 0,
                body_check.IsChecked == true ? 1 : 0,
                tank_check.IsChecked == true ? 1 : 0,
                jib_check.IsChecked == true ? 1 : 0,
                outriggers_check.IsChecked == true ? 1 : 0
            };
            result_TextBlock.Text = CompareObjects(obj);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
