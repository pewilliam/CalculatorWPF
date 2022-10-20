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

                    Result.Text = resultado.ToString(CultureInfo.InvariantCulture);
                    Result.SelectionStart = Result.Text.Length;
                    break;

                case "-":
                    resultado = resultValue - double.Parse(Result.Text, CultureInfo.InvariantCulture);

                    if (GetDoubleLength(resultado) > 7)
                    {
                        Result.Text = resultado.ToString("F10", CultureInfo.InvariantCulture);
                        break;
                    }

                    Result.Text = resultado.ToString(CultureInfo.InvariantCulture);
                    Result.SelectionStart = Result.Text.Length;
                    break;

                case "x": // multiplicação
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

                case "/": // divisão
                    resultado = resultValue / double.Parse(Result.Text, CultureInfo.InvariantCulture);

                    if(GetDoubleLength(resultado) > 7) // verifica se a quantidade de números após casa decimal é maior que 7
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

        private void RowDefinition_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
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

        private int GetDoubleLength(double n) //verificar o quantidade de números após casa decimal
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
    }
}
