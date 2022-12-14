using System.Globalization;
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
                    if (Result.Text.Length < 12)
                    {
                        Result.AppendText(button.Content.ToString());
                    }
                }
            }
            else
            {
                if (Result.Text.Length < 12)
                {
                    Result.AppendText(button.Content.ToString());
                }
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
            double resultado = 0;

            switch (operationPerformed)
            {
                case "+": // soma
                    resultado = resultValue + double.Parse(Result.Text, CultureInfo.InvariantCulture);

                    if (GetDoubleLength(resultado) > 7) // verifica se a quantidade de números após casa decimal é maior que 7
                    {
                        Result.FontSize = 30;
                        Result.Text = resultado.ToString("F10", CultureInfo.InvariantCulture);
                        break;
                    }

                    if (resultado.ToString().Length > 10) // verifica se a quantidade de números totais é maior que 10
                    {
                        Result.FontSize = 28; // caso seja, diminui tamanho da fonte para 28 para caber todos
                        Result.Text = resultado.ToString(CultureInfo.InvariantCulture);
                        break;
                    }

                    else
                    {
                        Result.FontSize = 40;
                        Result.Text = resultado.ToString(CultureInfo.InvariantCulture);
                    }

                    Result.SelectionStart = Result.Text.Length;
                    break;

                case "-":
                    resultado = resultValue - double.Parse(Result.Text, CultureInfo.InvariantCulture);

                    if (GetDoubleLength(resultado) > 7)
                    {
                        Result.Text = resultado.ToString("F10", CultureInfo.InvariantCulture);
                        break;
                    }

                    if (resultado.ToString().Length > 10) // verifica se a quantidade de números totais é maior que 10
                    {
                        Result.FontSize = 28; // caso seja, diminui tamanho da fonte para 28 para caber todos
                        Result.Text = resultado.ToString(CultureInfo.InvariantCulture);
                        break;
                    }

                    else
                    {
                        Result.FontSize = 40;
                        Result.Text = resultado.ToString(CultureInfo.InvariantCulture);
                    }

                    Result.Text = resultado.ToString(CultureInfo.InvariantCulture);
                    Result.SelectionStart = Result.Text.Length;
                    break;

                case "×": // multiplicação
                    resultado = resultValue * double.Parse(Result.Text, CultureInfo.InvariantCulture);

                    if (GetDoubleLength(resultado) > 7) // verifica se a quantidade de números após casa decimal é maior que 7
                    {
                        if (resultado.ToString().Length > 12) // verifica se a quantidade de números totais é maior que 12
                        {
                            Result.FontSize = 30; // caso seja, diminui a fonte para 30 para caber todos
                            Result.Text = resultado.ToString(CultureInfo.InvariantCulture);
                            break;
                        }
                        Result.FontSize = 40;
                        Result.Text = resultado.ToString("F10", CultureInfo.InvariantCulture);
                        break;
                    }

                    Result.Text = resultado.ToString(CultureInfo.InvariantCulture);
                    Result.SelectionStart = Result.Text.Length;
                    break;

                case "÷": // divisão
                    resultado = resultValue / double.Parse(Result.Text, CultureInfo.InvariantCulture);

                    if (GetDoubleLength(resultado) > 7) // verifica se a quantidade de números após casa decimal é maior que 7
                    {
                        Result.Text = resultado.ToString("F10", CultureInfo.InvariantCulture);
                        break;
                    }

                    Result.Text = resultado.ToString(CultureInfo.InvariantCulture);
                    Result.SelectionStart = Result.Text.Length;
                    break;

                default:
                    break;
            }

            resultValue = double.Parse(Result.Text, CultureInfo.InvariantCulture);
            CurrentEntry.Content = "";
            isOperationPerformed = true;
        }

        private void ClearEntryButton(object sender, RoutedEventArgs e) // limpa entrada
        {
            Result.Text = "0";
        }

        private void ClearAllButton(object sender, RoutedEventArgs e) // limpa entrada e memória
        {
            Result.Text = "0";
            CurrentEntry.Content = 0;
            resultValue = 0;
            operationPerformed = "";
        }

        private void CloseButton(object sender, RoutedEventArgs e) // fecha programa
        {
            Close();
        }

        private void MinimizeButton(object sender, RoutedEventArgs e) // minimiza
        {
            this.WindowState = WindowState.Minimized;
        }

        private void DeleteText(object sender, RoutedEventArgs e) // apagar último digito do texto
        {
            if (Result.Text == "0")
            {
                Result.Text = "0";
            }
            if (Result.Text.Length == 1)
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

        private static int GetDoubleLength(double n) //verificar o quantidade de números após casa decimal
        {
            string[] number = n.ToString().Split(',');
            int size;

            if (number.Length == 1)
            {
                return 0;
            }

            else
            {
                size = number[1].Length;
            }

            return size;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.NumPad0:
                    NumericButtonClick(_Zero, null);
                    break;
                case Key.NumPad1:
                    NumericButtonClick(One, null);
                    break;
                case Key.NumPad2:
                    NumericButtonClick(Two, null);
                    break;
                case Key.NumPad3:
                    NumericButtonClick(_Three, null);
                    break;
                case Key.NumPad4:
                    NumericButtonClick(Four, null);
                    break;
                case Key.NumPad5:
                    NumericButtonClick(Five, null);
                    break;
                case Key.NumPad6:
                    NumericButtonClick(Six, null);
                    break;
                case Key.NumPad7:
                    NumericButtonClick(Seven, null);
                    break;
                case Key.NumPad8:
                    NumericButtonClick(Eight, null);
                    break;
                case Key.NumPad9:
                    NumericButtonClick(Nine, null);
                    break;

                case Key.Enter:
                    ResultButton(Equals, null);
                    break;

                case Key.Multiply:
                    OperatorButton(Multiplier, null);
                    break;

                case Key.Subtract:
                    OperatorButton(Minus, null);
                    break;

                case Key.Divide:
                    OperatorButton(Divisor, null);
                    break;

                case Key.Add:
                    OperatorButton(Plus, null);
                    break;

                case Key.Back:
                    DeleteText(Delete, null);
                    break;

                case Key.Escape:
                    ClearEntryButton(ClearEntry, null);
                    break;

                case Key.Delete:
                    ClearAllButton(Clear, null);
                    break;

                case Key.OemPeriod:
                    NumericButtonClick(Dot, null);
                    break;

                case Key.OemComma:
                    NumericButtonClick(Dot, null);
                    break;

                case Key.Decimal:
                    NumericButtonClick(Dot, null);
                    break;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
