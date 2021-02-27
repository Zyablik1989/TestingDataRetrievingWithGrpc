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
            InitializeComponent();
            Init();
        }

        private readonly MainViewModel viewModel = new MainViewModel();

        private void Init()
        {
            DataContext = viewModel;
        }

        //private void RetrieveData(object sender, RoutedEventArgs e)
        //{
        //    //Tightly connected test of basic logic
        //    //var testingdata = DataRetriever.CollectTestingData(tbName.Text, cbPosition.SelectedIndex);

        //    ////Drawing plot
        //    //plotView.Model = PlotModelDefine.GridLinesHorizontal(testingdata.Data);

        //    ////Filling fields
        //    //tbLambda.Text = testingdata.Lambda.ToString();
        //    //tbFrequency.Text = testingdata.Frequency.ToString();
        //    //tbSignalType.Text = testingdata.SignalType.ToString();
        //    //tbComment.Text = testingdata.Comment;
        //}
    }
}
