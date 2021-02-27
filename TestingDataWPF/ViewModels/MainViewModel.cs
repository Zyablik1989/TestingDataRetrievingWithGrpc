using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using OxyPlot;

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

        public ICommand RetrieveDataCommand { get; set; }

        //Title of window
        string title = "Testing data retriever";
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
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

        //Frequency
        string signalType = string.Empty;
        public string SignalType
        {
            get { return signalType; }
            set { SetProperty(ref signalType, value); }
        }

        PlotModel plotModel = new PlotModel();
        public PlotModel PlotModel
        {
            get { return plotModel; }
            set { SetProperty(ref plotModel, value); }
        }

        //Frequency
        string comment = string.Empty;
        public string Comment
        {
            get { return comment; }
            set { SetProperty(ref comment, value); }
        }
        #endregion
    }
}
