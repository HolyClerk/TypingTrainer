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
using System.Windows.Threading;
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

        private const int BRUSH_LEFT = 114;
        private const int BRUSH_TOP = 93;
        private const int STEPSIZE = 10;

        private int _ticks = 0;
        private int _hits = 0;
        private int _misses = 0;    

        private DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();
            FillInputTextBox();

            SelectedStart = INITIAL_START_POS;

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += (object? sender, EventArgs e) =>
            {
                _ticks++;

                float result = (_hits + _misses) / _ticks;

                spdtypBlock.Text = $"{result * 60} KPM";
            };
        }

        public int SelectedStart { get; set; }

        // Движение окна
        private void HeaderGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TrainerWindow.DragMove();
        }

        //
        private void TrainerWindow_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!timer.IsEnabled)
            {
                e.Handled = true;
                return;
            }

            if (e.Text == inputTextBox.SelectedText)
            {
                OnUserSuccess();
            }
            else
            {
                OnUserMisstake();
            }
        }

        private void TrainerWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!timer.IsEnabled)
            {
                e.Handled = true;
                return;
            }

            if ((e.Key == Key.Space) && inputTextBox.SelectedText == " ")
            {
                OnUserSuccess();
            }
        }
        //

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            inputTextBox.Focus();
            inputTextBox.Select(INITIAL_START_POS, INITIAL_LENGTH);

            SelectedStart = INITIAL_START_POS;

            timer.Start();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
        }

        private void NewTextButton_Click(object sender, RoutedEventArgs e)
        {
            FillInputTextBox();
        }

        private void FillInputTextBox()
        {
            Text text = new GeneratedText().GetNewGeneratedText(Difficulty.Insane);
            inputTextBox.Text = text.Value;
        }

        private void OnUserMisstake()
        {
            inputTextBox.SelectionBrush = new SolidColorBrush(Color.FromRgb(250, 76, 61));
            _misses++;
        }

        private void OnUserSuccess()
        {
            inputTextBox.Select(++SelectedStart, INITIAL_LENGTH);
            inputTextBox.SelectionBrush = new SolidColorBrush(Color.FromRgb(66, 214, 45));

            _hits++;
        }
    }
}
