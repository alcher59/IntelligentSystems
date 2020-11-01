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
        List<ListObjects> listObjects = new List<ListObjects>();
        List<ListViewObjects> listViewObjects = new List<ListViewObjects>();
        const int propertyCount = 10;
        const int objectsCount = 5;
        int[,] tmatr;

        public MainWindow()
        {
            InitializeComponent();
           
            txtBox_name.Text = "Введите название..";
            InitObjects();
            var props = listObjects.Select(x => x.Properties).ToList();
            var matr = CreateMatrix(props);
            tmatr = TeachMatrix(props, matr);
            PrintMatr(tmatr);
        }

        public void PrintMatr(int[,] matr)
        {
            dataGrid.Items.Clear();
            List<Obj> objects = new List<Obj>();
            for (int i = 0; i < propertyCount; i++)
            {
                objects.Add(new Obj
                {
                    Title1 = matr[i, 0].ToString(),
                    Title2 = matr[i, 1].ToString(),
                    Title3 = matr[i, 2].ToString(),
                    Title4 = matr[i, 3].ToString(),
                    Title5 = matr[i, 4].ToString(),
                    //Title6 = matr[i, 5].ToString(),
                });
                dataGrid.Items.Add(objects[i]);
            }

            var headers = listObjects.Select(x => x.Name).ToList();
            for(int i = 0; i < headers.Count; i++)
            {
                dataGrid.Columns[i].Header = headers[i];
            }
        }
        public void InitObjects()
        {
            //электромотор, двс, колеса, гусеницы, ковш, бур, кузов, цистерна, стрела, выносные опоры, 
            listObjects.Add(new ListObjects(){ Name = "Самосвал", Properties = new int[propertyCount] { 0, 1, 1, 0, 0, 0, 1, 0, 0, 0 } });
            listObjects.Add(new ListObjects(){ Name = "Бензовоз", Properties = new int[propertyCount] { 0, 1, 1, 0, 0, 0, 0, 1, 0, 0 } });
            listObjects.Add(new ListObjects(){ Name = "Бульдозер", Properties = new int[propertyCount] { 0, 1, 0, 1, 1, 0, 0, 0, 0, 0 } });
            listObjects.Add(new ListObjects(){ Name = "Кран", Properties = new int[propertyCount] { 1, 0, 0, 0, 0, 0, 0, 0, 1, 1 } });
            listObjects.Add(new ListObjects(){ Name = "Бурильная машина", Properties = new int[propertyCount] { 0, 1, 0, 1, 0, 1, 0, 0, 0, 1 } });

            listViewObjects.Add(new ListViewObjects(){ Name = listObjects[0].Name, Properties = "0-1-1-0-0-0-1-0-0-0" });
            listViewObjects.Add(new ListViewObjects(){ Name = listObjects[1].Name, Properties = "0-1-1-0-0-0-0-1-0-0" });
            listViewObjects.Add(new ListViewObjects(){ Name = listObjects[2].Name, Properties = "0-1-0-1-1-0-0-0-0-0" });
            listViewObjects.Add(new ListViewObjects(){ Name = listObjects[3].Name, Properties = "1-0-0-0-0-0-0-0-1-1" });
            listViewObjects.Add(new ListViewObjects(){ Name = listObjects[4].Name, Properties = "0-1-0-1-0-1-0-0-0-1" });
            foreach (var obj in listViewObjects)
                listView.Items.Add(obj);
        }

        public int[,] CreateMatrix(List<int[]> list)
        {
            int[,] matrix = new int[propertyCount, objectsCount];
            for(int i = 0; i < propertyCount; i++)
                for (int j = 0; j < objectsCount; j++)
                {
                    matrix[i, j] = 0;
                }
            return matrix;
        }

        public int[,] TeachMatrix(List<int[]> objects, int[,] matr)
        {
            for (int key = 0; key < objectsCount; key++)
                for (int i = 0; i < propertyCount; i++)
                    for (int j = 0; j < objectsCount; j++)
                    {
                        if (j == key)
                        {
                            matr[i, j] += objects[key][i];
                        }
                        else
                        {
                            matr[i, j] -= objects[key][i];
                            if (matr[i, j] == -2)
                                matr[i, j] = -1;
                        }
                    }
            return matr;
        }

        public string CompareObjects(int[] y)
        {
            int[] demons = new int[objectsCount];
            List<int> maxNumbers = new List<int>();
            string result = "";
            string text = "";
            for (int i = 0; i < objectsCount; i++)
            {
                for (int j = 0; j < propertyCount; j++)
                {
                    demons[i] += tmatr[j, i] * y[j];
                }
                text += $"{demons[i]}, ";
            }
            demons_TextBlock.Text = text.Substring(0, text.Length - 2);
            for (int num = 0; num < objectsCount; num++)
            {
                if (demons[num] == demons.Max())
                    maxNumbers.Add(num);  //номера объектов, на которые похож новый объект
            }
            
            for (int c = 0; c < maxNumbers.Count; c++)
            {
                switch (maxNumbers[c])
                {
                    case 0:
                        if (!result.Contains(listObjects[0].Name))
                            result += "/" + listObjects[0].Name;
                        break;
                    case 1:
                        if (!result.Contains(listObjects[1].Name))
                            result += "/" + listObjects[1].Name;
                        break;
                    case 2:
                        if (!result.Contains(listObjects[2].Name))
                            result += "/" + listObjects[2].Name;
                        break;
                    case 3:
                        if (!result.Contains(listObjects[3].Name))
                            result += "/" + listObjects[3].Name;
                        break;
                    case 4:
                        if (!result.Contains(listObjects[4].Name))
                            result += "/" + listObjects[4].Name;
                        break;
                }
            }
            result = result.Remove(0, 1);
            return result;
        }

        public int[] GetObjectProperties()
        {
            int[] obj = {
                electro_check.IsChecked == true ? 1 : 0,
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
        }

        public class ListObjects
        {
            public string Name { get; set; }
            public int[] Properties { get; set; }
        }

        public class ListViewObjects
        {
            public string Name { get; set; }
            public string Properties { get; set; }
        }

        private void btn_teach_Click(object sender, RoutedEventArgs e)
        {

            var props = listObjects.Select(x => x.Properties).ToList();
            var newMatr = CreateMatrix(props);
            tmatr = TeachMatrix(props, newMatr);
            PrintMatr(tmatr);
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            if (listObjects.Count < objectsCount)
            {
                if (listObjects.FirstOrDefault(x => x.Name == txtBox_name.Text) == null)
                {
                    var newProps = GetObjectProperties();

                    string properties = "";

                    foreach (int i in newProps)
                    {
                        properties += i.ToString() + "-";
                    }

                    properties = properties.Remove(17, 1);

                    if (listViewObjects.FirstOrDefault(x => x.Properties == properties) == null)
                    {
                        var newObj = new ListObjects()
                        {
                            Name = txtBox_name.Text,
                            Properties = newProps
                        };

                        var newViewObj = new ListViewObjects()
                        {
                            Name = txtBox_name.Text,
                            Properties = properties
                        };

                        listObjects.Add(newObj);
                        listViewObjects.Add(newViewObj);
                        listView.Items.Add(newViewObj);

                        if (listObjects.Count == objectsCount)
                            btn_teach.IsEnabled = true;
                    }
                    else
                        MessageBox.Show("Введите дргуие параметры!", "Ошибка");
                }
                else
                    MessageBox.Show("Введите другое название!", "Ошибка");
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
                ListViewObjects item = (ListViewObjects)listView.SelectedItem;
                foreach (var obj in listObjects)
                {
                    if (obj.Name == item.Name)
                    {
                        listObjects.Remove(obj);
                        break;
                    }
                }
                ListViewObjects itemView = (ListViewObjects)listView.SelectedItem;
                foreach (var obj in listViewObjects)
                {
                    if (obj.Name == itemView.Name)
                    {
                        listViewObjects.Remove(obj);
                        break;
                    }
                }
                listView.Items.Remove(itemView);
                if (listObjects.Count != objectsCount)
                    btn_teach.IsEnabled = false;
            }
        }

        private void btn_clear_Click(object sender, RoutedEventArgs e)
        {
            listObjects.Clear();
            listViewObjects.Clear();
            listView.Items.Clear();
            if (listObjects.Count != objectsCount)
                btn_teach.IsEnabled = false;
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
    }
}
