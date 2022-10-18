using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CalculatorWPF
{
    public partial class MainWindow : Window
    {
        double resultValue = 0;
        string operationPerformed = "";
        bool isOperationPerformed = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void NumericButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            if (Result.Text == "0" && button.Content.ToString() == ".")
            {
                Result.AppendText(button.Content.ToString());
            }

            if (Result.Text.ToString() == "0" || isOperationPerformed)
            {
                Result.Text = "";
            }

            isOperationPerformed = false;

            if (button.Content.ToString() == ".")
            {
                if (!Result.Text.ToString().Contains("."))
                {
                    Result.AppendText(button.Content.ToString());
                }
            }
            else
            {
                Result.AppendText(button.Content.ToString());
            }
        }

        private void OperatorButton(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            operationPerformed = button.Content.ToString();
            resultValue = double.Parse(Result.Text, CultureInfo.InvariantCulture);
            isOperationPerformed = true;
            CurrentEntry.Content = resultValue + " " + operationPerformed;
        }

        private void ResultButton(object sender, RoutedEventArgs e)
        {
            switch (operationPerformed)
            {
                case "+":
                    Result.Text = (resultValue + double.Parse(Result.Text, CultureInfo.InvariantCulture)).ToString(CultureInfo.InvariantCulture);
                    Result.SelectionStart = Result.Text.Length;
                    break;

                case "-":
                    Result.Text = (resultValue - double.Parse(Result.Text, CultureInfo.InvariantCulture)).ToString(CultureInfo.InvariantCulture);
                    Result.SelectionStart = Result.Text.Length;
                    break;

                case "x":
                    Result.Text = (resultValue * double.Parse(Result.Text, CultureInfo.InvariantCulture)).ToString(CultureInfo.InvariantCulture);
                    Result.SelectionStart = Result.Text.Length;
                    break;

                case "/":
                    Result.Text = (resultValue / double.Parse(Result.Text, CultureInfo.InvariantCulture)).ToString(CultureInfo.InvariantCulture);
                    Result.SelectionStart = Result.Text.Length;
                    break;

                default:
                    break;
            }

            resultValue = double.Parse(Result.Text, CultureInfo.InvariantCulture);
            CurrentEntry.Content = "";
            isOperationPerformed = true;
        }

        private void ClearEntryButton(object sender, RoutedEventArgs e)
        {
            Result.Text = "0";
        }

        private void ClearAllButton(object sender, RoutedEventArgs e)
        {
            Result.Text = "0";
            CurrentEntry.Content = 0;
        }

        private void CloseButton(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MinimizeButton(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void RowDefinition_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void DeleteText(object sender, RoutedEventArgs e)
        {
            if(Result.Text == "0")
            {
                Result.Text = "0";
            }
            if(Result.Text.Length == 1)
            {
                Result.Text = "0";
            }
            else
            {
                Result.Text = Result.Text.Substring(0, Result.Text.Length - 1);
            }
        }

        private void Result_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Result.SelectionStart = Result.Text.Length;
        }
    }
}
