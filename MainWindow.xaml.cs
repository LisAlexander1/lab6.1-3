using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace lab6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ArrayList list = new ArrayList();

        public MainWindow()
        {
            InitializeComponent();

        }

        private void TextBox_KeyDown(object sender, TextCompositionEventArgs e)
        {
            int val;
            if (!int.TryParse(e.Text,out val) && !(e.Text.Equals("-") && TextBoxInput.Text.Length == 0) && e.Text[0] !=  13 && e.Text[0] != 44)
            {
                e.Handled = true;
            }
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Add(sender, e);
            }
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (OutputList.SelectedIndex >= 0)
            {
                list.RemoveAt(OutputList.SelectedIndex);

                Update();
            }
        }

        private void Update()
        {
            OutputList.Items.Clear();
            foreach (var item in list)
            {
                var listItem = new ListBoxItem();
                listItem.Content = item;
                OutputList.Items.Add(listItem);
            }
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBoxInput.Text))
            {
                int input;
                bool success = int.TryParse(TextBoxInput.Text, out input);
                if (success && input % 10 != 0)
                {
                    ErrorOut.Content = "";
                    list.Add(input);
                    TextBoxInput.Clear();
                    Update();
                } else if (success && input % 10 == 0)
                {
                    ErrorOut.Content = "Число не должно быть кратно 10";
                }
                else if (!success)
                {
                    ErrorOut.Content = "Невозможно получить число";

                }
            } else
            {
                ErrorOut.Content = "Введите число";
            }
        }

        private void Clear(object sender, RoutedEventArgs e)
        {
            list.Clear();
            Update();
        }
    }
}
