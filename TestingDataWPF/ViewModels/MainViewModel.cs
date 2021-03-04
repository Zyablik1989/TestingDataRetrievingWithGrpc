using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using TestingDataWPF.Models;
using System.Globalization;
using System.Threading;
using TestingDataWPF.Localization;

namespace TestingDataWPF.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        #region INotify... realisation

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var changed = PropertyChanged;

            changed?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #endregion

        #region UI fields

        string title = ResourceHandler.GetResource("TestingdatareceiverString");
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        string juniorResearcher = ResourceHandler.GetResource("JuniorResearcher");
        public string JuniorResearcher
        {
            get { return juniorResearcher; }
            set { SetProperty(ref juniorResearcher, value); }
        }

        string researcher = ResourceHandler.GetResource("Researcher");
        public string Researcher
        {
            get { return researcher; }
            set { SetProperty(ref researcher, value); }
        }

        string seniorResearcher = ResourceHandler.GetResource("SeniorResearcher");
        public string SeniorResearcher
        {
            get { return seniorResearcher; }
            set { SetProperty(ref seniorResearcher, value); }
        }

        string retrieve = ResourceHandler.GetResource("Retrieve");
        public string Retrieve
        {
            get { return retrieve; }
            set { SetProperty(ref retrieve, value); }
        }

        string clear = ResourceHandler.GetResource("Clear");
        public string Clear
        {
            get { return clear; }
            set { SetProperty(ref clear, value); }
        }

        string languageLabel = ResourceHandler.GetResource("LanguageLabel");
        public string LanguageLabel
        {
            get { return languageLabel; }
            set { SetProperty(ref languageLabel, value); }
        }

        string frequencyLabel = ResourceHandler.GetResource("FrequencyLabel");
        public string FrequencyLabel
        {
            get { return frequencyLabel; }
            set { SetProperty(ref frequencyLabel, value); }
        }

        string lambdaLabel = ResourceHandler.GetResource("LambdaLabel");
        public string LambdaLabel
        {
            get { return lambdaLabel; }
            set { SetProperty(ref lambdaLabel, value); }
        }

        string signalTypeLabel = ResourceHandler.GetResource("SignalTypeLabel");
        public string SignalTypeLabel
        {
            get { return signalTypeLabel; }
            set { SetProperty(ref signalTypeLabel, value); }
        }

        string commentLabel = ResourceHandler.GetResource("CommentLabel");
        public string CommentLabel
        {
            get { return commentLabel; }
            set { SetProperty(ref commentLabel, value); }
        }
        #endregion

        #region InputFields

        //Name of user
        string name = ResourceHandler.GetResource("NameString");
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        //Position of user
        int position = 0;
        public int Position
        {
            get { return position; }
            set { SetProperty(ref position, value); }
        }

        //Position of user
        int language = 0;
        public int Language
        {
            get { return language; }
            set
            {
                SetProperty(ref language, value);
                SetLanguage();
            }
        }
        #endregion

        //Is view busy right now
        bool notReceiving = true;
        public bool NotReceiving
        {
            get { return notReceiving; }
            set { SetProperty(ref notReceiving, value); }
        }

        #region Tesing data fields

        //Lambda
        string lambda = string.Empty;
        public string Lambda
        {
            get { return lambda; }
            set { SetProperty(ref lambda, value); }
        }

        //Frequency
        string frequency = string.Empty;
        public string Frequency
        {
            get { return frequency; }
            set { SetProperty(ref frequency, value); }
        }

        //Signal Type
        string signalType = string.Empty;
        public string SignalType
        {
            get { return signalType; }
            set { SetProperty(ref signalType, value); }
        }

        //Plot with calculated graph
        PlotModel plotModel = new PlotModel();
        public PlotModel PlotModel
        {
            get { return plotModel; }
            set { SetProperty(ref plotModel, value); }
        }

        //Comment
        string comment = string.Empty;
        public string Comment
        {
            get { return comment; }
            set { SetProperty(ref comment, value); }
        }
        #endregion

        
        //Retrieve data from server and fill all fields
        public async Task ReceiveData()
        {
            NotReceiving = false;

            try
            {
                //Firstly trying to receive data. Errors will be thrown further
                var data = await DataReceiver.CollectTestingData(Name,Position);

                if (string.IsNullOrEmpty(data.Data))
                {
                    Comment = "No data received";
                    return;
                }

                //Trying to draw a graph for plot from data numbers
                PlotModel = PlotModelDefine.GridLinesHorizontal(data.Data);

                Lambda = data.Lambda.ToString();
                Frequency = data.Frequency.ToString();
                SignalType = data.SignalType.ToString();
                Comment = data.Comment;
            }
            catch (Exception ex)
            {
                if (ex.Data.Contains("UserMessage"))
                {
                    Comment = ex.Data["UserMessage"].ToString();
                }
                else
                {
                    Comment = ex.Message;
                }
            }
            finally
            {
                NotReceiving = true;
            }
        }

        //clean all fields
        public void ClearData()
        {
            NotReceiving = false;

            PlotModel = new PlotModel();
            Lambda = string.Empty;
            Frequency = string.Empty;
            SignalType = string.Empty;
            Comment = string.Empty;

            NotReceiving = true;
        }

        public MainViewModel()
        {
            Language = Properties.Settings.Default.Language;
        }

        public void SetLanguage()
        {
            Properties.Settings.Default.Language = Language;
            Properties.Settings.Default.Save();
            switch (Language)
            {
                default:
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
                    
                    break;
                case 1:
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("ru-RU");
                    break;
            }

            Title = ResourceHandler.GetResource("Testingdatareceiver");
            Name = ResourceHandler.GetResource("NameString");
            JuniorResearcher = ResourceHandler.GetResource("JuniorResearcher");
            Researcher = ResourceHandler.GetResource("Researcher");
            SeniorResearcher = ResourceHandler.GetResource("SeniorResearcher");
            Retrieve = ResourceHandler.GetResource("Retrieve");
            Clear = ResourceHandler.GetResource("Clear");
            LanguageLabel = ResourceHandler.GetResource("LanguageLabel");
            FrequencyLabel = ResourceHandler.GetResource("FrequencyLabel");
            LambdaLabel = ResourceHandler.GetResource("LambdaLabel");
            SignalTypeLabel = ResourceHandler.GetResource("SignalTypeLabel");
            CommentLabel = ResourceHandler.GetResource("CommentLabel");
        }
    }
}
