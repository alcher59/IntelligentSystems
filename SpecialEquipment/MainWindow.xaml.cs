using System;
using System.Collections.Generic;
using System.Data;
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
        public List<ListObjects> listObjects = new List<ListObjects>();
        const int propertyCount = 9;
        int[,] tmatr;
        public MainWindow()
        {
            InitializeComponent();
           
            txtBox_name.Text = "Введите название..";

            var propList = FillProperties(); 
            var nameList = FillNames();
            var matr = CreateMatrix(propList);
            tmatr = TeachMatrix(propList, matr);

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
                    Title6 = tmatr[i, 5].ToString(),
                });
                dataGrid.Items.Add(objects[i]);
            }

            dataGrid.Columns[0].Header = "Самосвал";
            dataGrid.Columns[1].Header = "Бензовоз";
            dataGrid.Columns[2].Header = "Кран-борт";
            dataGrid.Columns[3].Header = "Бульдозер";
            dataGrid.Columns[4].Header = "Экскаватор";
            dataGrid.Columns[5].Header = "Бурмашина";
        }


        public List<int[]> FillProperties(/*List<int[]> newList*/)
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

        public List<string> FillNames(/*List<string> newList*/)
        {
            List<string> list = new List<string>();
            list.Add("Самосвал");
            list.Add("Бензовоз");
            list.Add("Кран-борт");
            list.Add("Бульдозер");
            list.Add("Экскаватор");
            list.Add("Бурильная машина");
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
                        maxNumbers.Add(num);  //номера объектов, на которые похож новый объект
                }
            }
            
            //int n = 1;
            //int resNum = 0;
            List<int> resNums = new List<int>();
            int[] countsObj = new int[6];
            int no = 0;
            foreach (int val in maxNumbers.Distinct())
            {
                countsObj[no] = maxNumbers.Where(x => x == val).Count();
                no++;
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

        public int[] GetObjectProperties()
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
            return obj;
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

        public class ListObjects
        {
            public string Name { get; set; }
            public string Properties { get; set; }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

        }


        
        private void txtBox_name_MouseEnter(object sender, MouseEventArgs e)
        {
            if (txtBox_name.Text == "Введите название..")
            {
                txtBox_name.Clear();
            }
        }

        private void txtBox_name_MouseLeave(object sender, MouseEventArgs e)
        {
            TextBox textBox = Keyboard.FocusedElement as TextBox;
            if (textBox == null)
            {
                if (string.IsNullOrWhiteSpace(txtBox_name.Text))
                {
                    txtBox_name.Text = "Введите название..";
                }
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBox textBox = Keyboard.FocusedElement as TextBox;
            if (textBox != null)
            {
                TraversalRequest tRequest = new TraversalRequest(FocusNavigationDirection.Next);
                textBox.MoveFocus(tRequest);
            }
            if (string.IsNullOrWhiteSpace(txtBox_name.Text))
            {
                txtBox_name.Text = "Введите название..";
            }
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            if (listObjects.Count < 6)
            {
                var newProp = GetObjectProperties();

                string properties = "";

                foreach (int i in newProp)
                {
                    properties += i.ToString() + "-";
                }

                var newObj = new ListObjects()
                {
                    Name = txtBox_name.Text,
                    Properties = properties.Remove(17, 1)
                };

                listObjects.Add(newObj);
                listView.Items.Add(newObj);

                if (listObjects.Count == 6)
                    btn_teach.IsEnabled = true;
            }
            else
                MessageBox.Show("Достигнуто максимальное кол-во объектов!\n\nОчистите список или обучите матрицу.", "Ошибка");
        }

        private void btn_comp_Click(object sender, RoutedEventArgs e)
        {
            var obj = GetObjectProperties();
            result_TextBlock.Text = CompareObjects(obj);
        }

        private void btn_remove_Click(object sender, RoutedEventArgs e)
        {
            if(listView.SelectedItem != null)
            {
                ListObjects item = (ListObjects)listView.SelectedItem;
                foreach (var obj in listObjects)
                {
                    if (obj.Name == item.Name)
                    {
                        listObjects.Remove(obj);
                        break;
                    }
                }
                listView.Items.Remove(item);
            }
        }

        private void btn_clear_Click(object sender, RoutedEventArgs e)
        {
            listObjects.Clear();
            listView.Items.Clear();
        }
    }
}
