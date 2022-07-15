using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using Newtonsoft.Json;
using WpfAppDemo1.ViewModel;

namespace WpfAppDemo1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel ViewModel { get; }

        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new MainViewModel();
            DataContext = ViewModel;
            ItemsControl.ItemsSource = ViewModel.ButtonInfoList;

            Task.Run(() =>
            {
                while (true)
                {
                    Dispatcher.Invoke(() => { TitleText.Text = "思政题目练习  作者：WHX    " + DateTime.Now; });
                    Thread.Sleep(1000);

                }
            });

        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (int.TryParse((sender as Button)?.Content.ToString(),out var numResult))
            {
                ViewModel.Index = numResult - 1;
            }
        }

        private void MainWindow_OnClosed(object sender, EventArgs e)
        {
            try
            {
                File.WriteAllText("AnswerRecord.txt", JsonConvert.SerializeObject(ViewModel.AnswerRecord));
                File.WriteAllText("Index.txt",ViewModel.Index.ToString());
                //File.WriteAllText("Questions.txt",JsonConvert.SerializeObject(ViewModel.AllQuestions));
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }


        private void MainWindow_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void ButtonClose_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void ButtonMinWindow_OnClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void ButtonMaxWindow_OnClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
        }
    }
    
}
