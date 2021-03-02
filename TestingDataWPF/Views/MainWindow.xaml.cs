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
using TestingDataWPF.ViewModels;


namespace TestingDataWPF.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Init();
            InitializeComponent();
        }

        private readonly MainViewModel viewModel = new MainViewModel();

        private void Init()
        {
            DataContext = viewModel;
        }

        private void RetrieveData(object sender, RoutedEventArgs e)
        {
            viewModel.ClearData();
            viewModel.ReceiveData();
        }

        private void ClearData(object sender, RoutedEventArgs e)
        {
            viewModel.ClearData();
        }
    }
}
