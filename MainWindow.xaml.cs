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
using lab6.Stack;
using lab6.TList;

namespace lab6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ArrayList list = new();
        private SinglyStack<string> stack = new();
        private TList<string> doublyList = new();
        Random r = new();


        public MainWindow()
        {
            InitializeComponent();
            OutputList.ItemsSource = list;
            stack.Push("Students");
            stack.Push("of");
            stack.Push("the");
            stack.Push("group");
            stack.Push("TE");
            stack.Push("3");
            UpdateStack();

        }

        private void TextBox_KeyDown(object sender, TextCompositionEventArgs e)
        {
            int val;
            if (!int.TryParse(e.Text,out val) && !(e.Text.Equals("-")) && e.Text[0] !=  13 && e.Text[0] != 44)
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
                OutputList.Items.Refresh();
                RefreshCounter();
            }
        }

        private void RefreshCounter()
        {
            int counter = 0;
            foreach (var item in list)
            {
                counter += Math.Abs((int)item % 2);
            }
            Counter.Content = counter;
        }

        private void UpdateStack()
        {
            OutputStack.Items.Clear();
            foreach (var item in stack)
            {
                var listItem = new ListBoxItem();
                listItem.Content = item;
                OutputStack.Items.Add(listItem);
            }
        }

        private void UpdateTList()
        {
            OutputTList.Items.Clear();
            foreach (var item in doublyList)
            {
                var listItem = new ListBoxItem();
                listItem.Content = item;
                OutputTList.Items.Add(listItem);
            }
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            string error = "";
            if (!string.IsNullOrEmpty(TextBoxInput.Text))
            {
                string[] inputSringArray = TextBoxInput.Text.Split(',');
                for (int i = 0; i < inputSringArray.Length; i++)
                {
                    int value;
                    bool success = int.TryParse(inputSringArray[i],out value);
                    if (success && value % 10 != 0)
                    {
                        list.Add(value);
                        
                    } else if (!success)
                    {
                        error += $"Не получилось получить значение: {inputSringArray[i]}.\n";
                    }
                }
                ErrorOut.Text = error;
                TextBoxInput.Clear();
                OutputList.Items.Refresh();
                RefreshCounter();
            } else
            {
                ErrorOut.Text = "Введите числа";
            }
        }

        private void Clear(object sender, RoutedEventArgs e)
        {
            list.Clear();
            OutputList.Items.Refresh();
            RefreshCounter();
        }

        private void PushToStack(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBoxStackInput.Text))
            {
                stack.Push(TextBoxStackInput.Text);
                UpdateStack();
                TextBoxStackInput.Clear();
            }
        }

        private void PopToStack(object sender, RoutedEventArgs e)
        {
            stack.Pop();
            UpdateStack();
        }

        private void InsertToTList(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBoxTListInput.Text))
            {
                doublyList.InsertBefore(TextBoxTListInput.Text);
                UpdateTList();
                TextBoxStackInput.Clear();
            }
        }

        private void RemoveFromTList(object sender, RoutedEventArgs e)
        {
            if (OutputTList.SelectedIndex >= 0)
            {
                var listItem = OutputTList.SelectedItem as ListBoxItem;
                
                doublyList.Remove(listItem.Content.ToString());
                UpdateTList();
            }
        }

        private void FillList(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < r.Next(1,10); i++)
            {
                var value = r.Next(0, 100);
                list.Add(value % 10 != 0 ? value : value-1);
            }
            OutputList.Items.Refresh();
        }
    }
}
