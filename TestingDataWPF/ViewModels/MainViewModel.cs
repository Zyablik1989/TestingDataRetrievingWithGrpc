using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using OxyPlot;
using TestingDataWPF.Models;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

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
        //Title of window
        string title = "Testing data receiver";
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        //Name of user
        string name = "Name";
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
    }
}
