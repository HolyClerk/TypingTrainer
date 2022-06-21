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

using TypingTrainer.Classes;

namespace TypingTrainer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int INITIAL_START_POS = 0;
        private const int INITIAL_LENGTH = 1;

        private bool _isTypingStarted = false;

        public MainWindow()
        {
            InitializeComponent();
            FillInputTextBox();

            SelectedStart = INITIAL_START_POS;
        }

        public int SelectedStart { get; set; }

        // Движение окна
        private void HeaderGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TrainerWindow.DragMove();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            inputTextBox.Focus();
            inputTextBox.Select(INITIAL_START_POS, INITIAL_LENGTH);

            SelectedStart = INITIAL_START_POS;

            _isTypingStarted = true;
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            _isTypingStarted = false;
        }

        // "Перехват" нажатых кнопок, иначе действует вложенная логика, сбивающая фокус.
        private void TrainerWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (!_isTypingStarted)
            {
                e.Handled = true;
                return;
            }

            inputTextBox.Focus();
        }

        private void inputTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!_isTypingStarted)
            {
                e.Handled = true;
                return;
            }

            if (e.Text == inputTextBox.SelectedText)
            {
                inputTextBox.Select(SelectedStart++, INITIAL_LENGTH);
            }
        }

        private void inputTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!_isTypingStarted)
            {
                e.Handled = true;
                return;
            }

            if ((e.Key == Key.Space) && inputTextBox.SelectedText == " ")
            {
                inputTextBox.Select(SelectedStart++, INITIAL_LENGTH);
            }
        }

        private void NewTextButton_Click(object sender, RoutedEventArgs e)
        {
            FillInputTextBox();
        }

        private void FillInputTextBox()
        {
            var text = new Text();
            text = new GeneratedText().GenerateNewText();
            inputTextBox.Text = text.Value;
        }
    }
}
